using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class SquadLeader : MonoBehaviour
{
    public List<GameObject> squadMembers = new List<GameObject>();
    private List<Squadmember> squadScripts = new List<Squadmember>();
    public int MembersinSquad = 0;
    public GameObject TargetBuilding;
    public BuildingControler.BuildingGraph buildingnodemap;
    public int entrypointindex;
    public bool ghostsquad = false;
    private bool pathisbuilt = false;
    private int PlaceinPathIndex = 0;
    private int t = 0;
    private float visionDistance = 5;
    private bool stackPositionsAssigned = false;
    private BuildingControler bc;
    public enum Squadstates
    {
        moving,
        attacking,
        destroyed,
        idle
    }
    public Squadstates squadStatus;

    List<GameObject> enemiesInThisRoom = new List<GameObject>();
    private List<BuildingControler.RoomRegion> Path = new List<BuildingControler.RoomRegion>();
    Dictionary<int, BuildingControler.RoomRegion> roomLookup;
  
    public void BuildPathBFS()
    {
        if (buildingnodemap == null || buildingnodemap.rooms == null || buildingnodemap.adjacency == null)
            return;

        Path.Clear();

        Queue<int> queue = new Queue<int>();
        HashSet<int> visited = new HashSet<int>();

        // Start at entry point
        queue.Enqueue(entrypointindex);
        visited.Add(entrypointindex);

        while (queue.Count > 0)
        {
            int currentRoomID = queue.Dequeue();

            // Get the actual room object
            BuildingControler.RoomRegion room = buildingnodemap.rooms[currentRoomID];
            Path.Add(room);

            // Check neighbors
            if (buildingnodemap.adjacency.TryGetValue(currentRoomID, out List<int> neighbors))
            {
                foreach (int neighborID in neighbors)
                {
                    if (!visited.Contains(neighborID))
                    {
                        visited.Add(neighborID);
                        queue.Enqueue(neighborID);
                    }
                }
            }
        }

        pathisbuilt = true;
    }
    public void BuildLookup()// id not garented to match the index
    {
        roomLookup = new Dictionary<int, BuildingControler.RoomRegion>();

        foreach (var room in buildingnodemap.rooms)
        {
            roomLookup[room.id] = room;
        }
    }


    private void Start()
    {
        //buildingnodemap = TargetBuilding.GetComponent<BuildingControler>().graph;
        //bc = TargetBuilding.GetComponent<BuildingControler>();
        foreach (var member in squadMembers)
        {
            squadScripts.Add(member.GetComponent<Squadmember>());
        }
        MembersinSquad = squadMembers.Count;
    }
    private void RunSquadUpdate()
    {
        if (!ghostsquad)
        {

            //do bfs 
            if (!pathisbuilt)
            {
                BuildLookup();
                BuildPathBFS();
                pathisbuilt = true;
                PlaceinPathIndex = 0;
            }
            //move through nodes
            //check if all squad members are in position 
            //check if room cleared 
            // position squad
            // do sight update

            // 0. Check if squad members exist (in case some died)

            squadMembers.RemoveAll(item => item == null);
            squadScripts.RemoveAll(item => item == null);
            if (squadMembers.Count > 0)
            {
                // squad destroyed
            }

            // 2. Clean the enemy list for the current room

            enemiesInThisRoom.RemoveAll(item => item == null);
            if (enemiesInThisRoom.Count <= 0)// check for enemies
            {
                squadStatus = Squadstates.moving;
            }




            //    release reserved cover
            //    order squad to move to next room node


            // -------------------------------------------------------
            // 1. Update sight checks (members do the raycasts)
            // -------------------------------------------------------
            // for each squad member: 
            //    squadMember.CheckForEnemies(...)  // using leader-provided data
            if (enemiesInThisRoom.Count > 0)
            {
                int enemyCount = enemiesInThisRoom.Count;
                int squadCount = squadMembers.Count;

                for (int j = 0; j < enemyCount; j++)
                {
                    // Assign a squad member to this enemy for this pass
                    int squadelementIndex = (j + t) % squadCount;//Rotates the squad members per enemy so each enemy is handled by a different member.

                    // Only run if the squad member exists
                    if (squadelementIndex < squadCount)
                    {
                        int enemyotherIndex = j;

                        // Vision system
                        Vector3 origin = squadMembers[squadelementIndex].transform.position;
                        Vector3 target = enemiesInThisRoom[enemyotherIndex].transform.position;

                        Vector3 directionToTarget = (target - origin).normalized;

                        // Use the squad member's forward
                        Vector3 forward = squadMembers[squadelementIndex].transform.forward;

                        float dot = Vector3.Dot(forward, directionToTarget);
                        float cosHalfFOV = Mathf.Cos(45f * Mathf.Deg2Rad); // 90° vision cone

                        RaycastHit hit;
                        if (dot >= cosHalfFOV && Physics.Raycast(origin, directionToTarget, out hit, visionDistance))
                        {
                            if (hit.transform == enemiesInThisRoom[enemyotherIndex].transform)
                            {
                                // Target is visible
                                // Debug.Log($"{squadMembers[squadelementIndex].name} sees {enemiesInThisRoom[enemyotherIndex].name}");
                                squadStatus = Squadstates.attacking;
                            }
                        }
                    }
                }

                // Advance rotation counter to wrap squad members evenly
                t = (t + enemyCount) % squadCount;
            }

            // 3. Handle squad state

            // ------------------- MOVING STATE -------------------
            if (squadStatus == Squadstates.moving)
            {
                // Check if we have reached current node's stack positions
                bool allInPosition = squadScripts.All(m => m.HasReachedDestination());

                // If enemies are present, switch to attacking
                if (enemiesInThisRoom.Count > 0)
                {
                    squadStatus = Squadstates.attacking;
                    return;
                }

                // Assign stack positions if not already done
                if (!stackPositionsAssigned)
                {
                    Vector3 nodeCenter = Path[PlaceinPathIndex].centerWorld;
                    List<Vector3> stackPositions = CalculateStackPositions(nodeCenter, squadMembers.Count);

                    for (int i = 0; i < squadScripts.Count; i++)
                    {
                        squadScripts[i].move(stackPositions[i]);
                    }

                    stackPositionsAssigned = true;
                }

                // Advance to next node if all reached
                if (allInPosition)
                {
                    PlaceinPathIndex++;
                    stackPositionsAssigned = false;

                    if (PlaceinPathIndex >= Path.Count)
                    {
                        // Reached final node
                        squadStatus = Squadstates.idle;
                    }
                }
            }
            // ------------------- ATTACKING STATE -------------------
            else if (squadStatus == Squadstates.attacking)
            {
                if (enemiesInThisRoom.Count == 0)
                {
                    // Room cleared, return to moving
                    squadStatus = Squadstates.moving;
                    foreach (var m in squadScripts)
                        m.status = Squadmember.Messages.idle;
                    return;
                }

                // Get cover positions in the current room
                List<Vector3> coverPositions = bc.GetCoverPositions(Path[PlaceinPathIndex], bc.CoverMap, bc.min);

                // Scatter squad members to nearest available cover
                foreach (var member in squadScripts)
                {
                    if (member.status != Squadmember.Messages.Attacking && coverPositions.Count > 0)
                    {
                        Vector3 cover = coverPositions
                            .OrderBy(p => Vector3.Distance(member.GetPosition(), p))
                            .First();

                        member.move(cover);
                        member.status = Squadmember.Messages.Attacking;

                        // Remove the cover spot from available list
                        coverPositions.Remove(cover);
                    }
                }

                // Assign nearest visible enemy to each squad member
                foreach (var member in squadScripts)
                {
                    if (member.status != Squadmember.Messages.Attacking) continue;

                    GameObject nearestEnemy = enemiesInThisRoom
                        .OrderBy(e => Vector3.Distance(member.GetPosition(), e.transform.position))
                        .FirstOrDefault();

                    if (nearestEnemy != null)
                    {
                        member.assignedTarget = nearestEnemy;
                        member.attack(nearestEnemy);
                    }
                }
            }

            // ------------------- UPDATE INDIVIDUAL MEMBERS -------------------
            foreach (var member in squadScripts)
            {
                member.SoldierUpdate(); // ticks fuzzy logic and health
            }
        }
    }

    // ------------------- HELPER FUNCTION -------------------
    // Create a simple stacking formation at a node
    private List<Vector3> CalculateStackPositions(Vector3 center, int count)
    {
        List<Vector3> positions = new List<Vector3>();
        float spacing = 1.5f;

        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * spacing;
            positions.Add(center + offset);
        }

        return positions;
    }


}


    


/*
 * squad menber should not be using unity update
 * sqleader call member update from its update function
 * genral clean up and optimation 
 * refactor logic
 * look into planning algorithiums 
 * goap
 * htn hir task networks
 * */