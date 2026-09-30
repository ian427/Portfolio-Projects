using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class squadLeader2 : MonoBehaviour
{
    /*
 

    private bool usingcordinatedplanning = false;
    public List<GameObject> squadMembers = new List<GameObject>();
    public List<Vector3> enemypositions = new List<Vector3>();
    public bool enemiesclose;
    private List<GameObject> enemies = new List<GameObject>();
    private int CurrentUnitManuvering;
    private List<Squadmember> squadLists = new List<Squadmember>();
    public LayerMask coverLayer;
    private enum squadstates//individual unit doing
    {
        takingcover,
        moving,
        Attack,

        healing,
        idle
    }

    private enum mission// squad doing
    {
        HoldPosition,
        Manuvering,
        HoldFire,
        Attack
    }
    public enum Ordertype
    {
        move,
        attack
    }

    public class SquadOrder
    {

        public Ordertype ordertype;
        public Vector3 TargetPosition; // Only used for Move orders
        public bool WaitForOtherSquads; // Whether to wait before executing

    }
    Vector3 CalculateCentroid()//calculates cetter position
    {
        List<Vector3> positions = squadMembers.Select(m => m.transform.position).ToList();
        Vector3 sum = Vector3.zero;

        foreach (Vector3 pos in positions)
        {
            sum += pos;
        }

        return sum / positions.Count;
    }

    public int SquadId;
    public bool inPosition = false;
    public bool waitingforotherSquads = false;
    public int placeinSquadPlan;
    public List<SquadOrder> Orders = new List<SquadOrder>();


    private Vector3 FindNearestVisiblePosition(Vector3 enemyPosition, float searchRadius, LayerMask coverLayer)
    {
        Vector3 bestPosition = Vector3.zero;
        float shortestDistance = Mathf.Infinity;

        // Find all potential positions within the search radius
        Collider[] positions = Physics.OverlapSphere(transform.position, searchRadius, coverLayer);

        foreach (var pos in positions)
        {
            Vector3 dirToEnemy = (enemyPosition - pos.transform.position).normalized;
            float distanceToEnemy = Vector3.Distance(pos.transform.position, enemyPosition);

            // Check if this position has a clear line of sight to the enemy
            if (Physics.Raycast(pos.transform.position, dirToEnemy, out RaycastHit hit, distanceToEnemy))
            {
                if (hit.collider.CompareTag("Enemy")) // Ensure the ray hits the enemy
                {
                    float distanceToSelf = Vector3.Distance(transform.position, pos.transform.position);
                    if (distanceToSelf < shortestDistance)
                    {
                        shortestDistance = distanceToSelf;
                        bestPosition = pos.transform.position;
                    }
                }
            }
        }

        return bestPosition;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coverLayer = LayerMask.GetMask("Cover");
        for (int i = 0; i < squadMembers.Count; i++)
        {
            //Squadmember temp = squadMembers[i].GetComponent<Squadmember>();
            squadLists.Add(squadMembers[i].GetComponent<Squadmember>());
        }

    }
    private SquadOrder GetCurrentOrder()
    {
        return Orders[placeinSquadPlan];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updateSqudSituation()//squads situation
    {
        // Debug.Log("Updateing");
        float searchRadius = 15f;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= searchRadius)
            {
                enemies.Add(enemy);
            }
        }
        Vector3 currentPosition = CalculateCentroid();
        float Radius = 15f;
        float shortestEnemyDistance = Mathf.Infinity;
        Vector3 enemyPosition = Vector3.zero;

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        foreach (GameObject obj in enemies)
        {
            if (obj == null) continue;

            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < Radius)
            {
                Vector3 directionToEnemy = (obj.transform.position - currentPosition).normalized;

                if (!Physics.Raycast(currentPosition, directionToEnemy, distance, coverLayer))
                {
                    if (distance < shortestEnemyDistance)
                    {
                        shortestEnemyDistance = distance;
                        enemyPosition = obj.transform.position;

                    }
                }
            }


            // If no enemy found, exit early
            if (shortestEnemyDistance == Mathf.Infinity)
            {
                enemiesclose = false;
            }
            if (shortestEnemyDistance != Mathf.Infinity)
            {
                enemiesclose = true;
            }

        }
        ///
        //decide if using cordinated plan

        /*plans
         * bounding fall back
         * leapfrog advance
         * turned shooting
         * spred ot advancinf formation
       
        if (squadMembers.Count > 4)
        {
            usingcordinatedplanning = true;
        }
        //Calculate situation for plan
        //if plan not complete
        if ((Orders.Count > 0) && (usingcordinatedplanning))// valid oreders
        {
            SquadOrder order = GetCurrentOrder();
            if (!enemiesclose)
            {


                if (order.ordertype == Ordertype.move)
                {
                    //Debug.Log("moving1");
                    if (Vector3.Distance(order.TargetPosition, CalculateCentroid()) < 3f)// if squad pos is with in 3 units
                    {
                        // Debug.Log("attarget");
                        //order complete 
                        if (placeinSquadPlan != Orders.Count)
                        {
                            placeinSquadPlan++;
                            // replan unit actions
                        }
                        else
                        {
                            Orders.Clear();
                            //Ordescompletegoidle
                        }
                    }
                    else
                    {
                        for (int unitID = 0; unitID < squadMembers.Count; unitID++)//move each unit
                        {
                            //Debug.Log("moving2");
                            squadLists[unitID].destination = order.TargetPosition;
                            //Debug.Log(squadMembers[unitID].GetComponent<Squadmember>().destination);
                            squadLists[unitID].Plan.Add(Squadmember.UnitManuvers.move);
                            ExecutePlan(unitID);

                        }

                        //continuemovement
                    }
                }
                else if (order.ordertype == Ordertype.attack)// attack sucess full
                {
                    //plan complete
                    if (placeinSquadPlan != Orders.Count)
                    {
                        placeinSquadPlan++;
                        // replan unit actions
                    }
                    else
                    {
                        //Ordescompletegoidle
                    }
                }
                else
                {
                    //  Debug.Log("invalidOrder");
                    //error non valid order
                }

                //move tonextwaypoint
                //if no next waypoint plan complete

            }
            else// enemies are close
            {

                if (order.ordertype == Ordertype.move)
                {
                    //behaviour/scatter advance by cover
                    //for every unity find cover
                    //find cover on the way to waypoint 

                    //check if at destination
                    if (Vector3.Distance(order.TargetPosition, CalculateCentroid()) < 3f)// if squad pos is with in 3 units
                    {
                        //order complete 
                        if (placeinSquadPlan != Orders.Count)
                        {
                            placeinSquadPlan++;
                            // replan unit actions
                        }
                        else
                        {
                            Orders.Clear();
                            //Ordescompletegoidle
                        }
                    }

                    else//replan
                    {
                        //need to move to position
                        //while in cover
                        int unitplanscomplete = 0;
                        for (int i = 0; i < squadMembers.Count; i++)
                        {
                            if (squadLists[i].placeinPlan >= squadLists[i].Plan.Count())
                            {
                                unitplanscomplete++;
                            }
                        }
                        if (unitplanscomplete > squadMembers.Count())//allplans complete replan
                        {
                            Vector3 lastcoverposition = squadMembers[0].transform.position;
                            for (int i = 0; squadMembers.Count() > 0; i++)
                            {
                                squadLists[i].takecover(enemyPosition);
                            }
                            // all units but one manuvering needs to wait
                            for (int i = 0; i < squadMembers.Count; i++)
                            {
                                int lastsqudmembermoved = 0;
                                for (int j = 0; j < squadMembers.Count; j++)
                                {
                                    if (j != i)
                                    {
                                        //skip
                                        squadLists[j].Plan.Add(Squadmember.UnitManuvers.idle);
                                    }
                                    else
                                    {
                                        //find next cover position and save as destination
                                        //calculate line of travel
                                        Vector3 travelDirection = (order.TargetPosition - lastcoverposition).normalized;
                                        float travelDistance = Vector3.Distance(lastcoverposition, order.TargetPosition);

                                        float stepSize = 1f;
                                        float minCoverDistanceFromEnemy = 3f;
                                        Vector3 bestCoverPoint = Vector3.zero;
                                        float closestDistanceToStart = Mathf.Infinity;

                                        for (float d = 0; d <= travelDistance; d += stepSize)
                                        {
                                            Vector3 samplePoint = lastcoverposition + travelDirection * d;

                                            // Find nearby cover objects at this sample point
                                            Collider[] nearbyCovers = Physics.OverlapSphere(samplePoint, 2f, coverLayer); // Adjust radius as needed
                                            foreach (Collider cover in nearbyCovers)
                                            {
                                                Vector3 coverPos = cover.transform.position;
                                                float distanceToEnemy = Vector3.Distance(coverPos, enemyPosition);

                                                if (distanceToEnemy >= minCoverDistanceFromEnemy)
                                                {
                                                    // Check if the cover blocks line of sight from the enemy
                                                    Vector3 dirFromEnemy = (coverPos - enemyPosition).normalized;
                                                    float distFromEnemy = Vector3.Distance(enemyPosition, coverPos);

                                                    if (Physics.Raycast(enemyPosition, dirFromEnemy, distFromEnemy, coverLayer))
                                                    {
                                                        float distanceToStart = Vector3.Distance(lastcoverposition, coverPos);
                                                        if (distanceToStart < closestDistanceToStart)
                                                        {
                                                            closestDistanceToStart = distanceToStart;
                                                            bestCoverPoint = coverPos;
                                                            squadLists[j].destination = bestCoverPoint;
                                                            squadLists[j].Plan.Add(Squadmember.UnitManuvers.move);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                                //current unit add move rest idle
                            }
                        }
                    }
                }
                else if (order.ordertype == Ordertype.attack)
                {
                    //find los attack
                    // find cover alternate attacks
                    for (int i = 0; i < squadMembers.Count; i++)
                    {
                        squadLists[i].Plan.Add(Squadmember.UnitManuvers.takecover);
                    }
                    for (int activePairStart = 0; activePairStart < squadMembers.Count; activePairStart += 2)
                    {
                        for (int j = 0; j < squadMembers.Count; j++)
                        {
                            if (j == activePairStart || j == activePairStart + 1)
                            {
                                squadLists[j].Plan.Add(Squadmember.UnitManuvers.attack); // Current pair attacks
                                squadLists[j].Plan.Add(Squadmember.UnitManuvers.takecover);
                            }
                            else
                            {
                                squadLists[j].Plan.Add(Squadmember.UnitManuvers.idle); // Everyone else idle
                                squadLists[j].Plan.Add(Squadmember.UnitManuvers.idle);
                            }
                        }
                    }

                }
                else
                {
                    usingcordinatedplanning = false;
                }
            }
        }
        else// plan needed
        {
            // Debug.Log("novalidorder");
            //ask for orders from captain
            usingcordinatedplanning = false;
        }
        //if squad injured
        // advancing
        // spread out and attack if little cover
        //turned attacking
        //no plan found defaulft Behaviour
        // Debug.Log("attempting default behaviour");
        for (int ID = 0; ID < squadMembers.Count; ID++)
        {
            //Debug.Log("forcheckpassed");
            // Debug.Log(usingcordinatedplanning);
            //(SquadList[ID].Plan.Count == SquadList[ID].placeinPlan
            if ((!usingcordinatedplanning))
            {
                squadLists[ID].placeinPlan = squadLists[ID].Plan.Count;
                calculateSituation(ID);
                //Debug.Log("calculatedsituation");
            }

            ExecutePlan(ID);
        }
    }
    private void calculateSituation(int ID)// each units situation
    {
        //Debug.Log("calculating");
        //calculate move location


        if ((squadLists[ID].wasshot > 0.4) && (squadLists[ID].health < (squadLists[ID].Maxhealth / 2)))
        {
            squadLists[ID].Plan.Clear();
            squadLists[ID].Plan.Add(Squadmember.UnitManuvers.takecover);
            squadLists[ID].Plan.Add(Squadmember.UnitManuvers.heal);
        }
        else if (enemiesclose && (squadLists[ID].Attacked < 0.4))
        {
            squadLists[ID].Plan.Clear();
            squadLists[ID].Plan.Add(Squadmember.UnitManuvers.move);

            //calculating LOS to enemy

            ///calculating closest
            GameObject closest = null;
            float shortestDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            Vector3 enemyPosition = Vector3.zero;
            float shortestEnemyDistance = Mathf.Infinity;

            // Find nearest enemy
            foreach (GameObject obj in enemies)
            {
                if (obj == null) continue;

                float distance = Vector3.Distance(currentPosition, obj.transform.position);
                if (distance < shortestEnemyDistance)
                {
                    shortestEnemyDistance = distance;
                    enemyPosition = obj.transform.position;
                }
            }

            // If no enemy found, exit early
            if (shortestEnemyDistance == Mathf.Infinity)
                return;

            float searchRadius = 15f;
            Collider[] covers = Physics.OverlapSphere(transform.position, searchRadius, coverLayer);
            Vector3 bestAttackPosition = Vector3.zero;
            float shortestAttackDistance = Mathf.Infinity;

            foreach (var cover in covers)
            {
                Vector3 dirToEnemy = (enemyPosition - cover.transform.position).normalized;
                float distanceToEnemy = Vector3.Distance(cover.transform.position, enemyPosition);

                // Check if there's a clear line of sight from cover to enemy
                if (!Physics.Raycast(cover.transform.position, dirToEnemy, distanceToEnemy))
                {
                    float distanceToLOS = Vector3.Distance(transform.position, cover.transform.position);
                    if (distanceToLOS < shortestAttackDistance)
                    {
                        shortestAttackDistance = distanceToLOS;
                        bestAttackPosition = cover.transform.position;
                    }
                }
            }

            // Assign destination if a valid cover was found
            if (shortestAttackDistance < Mathf.Infinity)
            {
                squadLists[ID].destination = bestAttackPosition;
            }
            squadLists[ID].Plan.Add(Squadmember.UnitManuvers.attack);

        }
        else if ((squadLists[ID].wasshot > 0.6) || (squadLists[ID].Attacked > 0.6))
        {
            squadLists[ID].Plan.Clear();
            squadLists[ID].Plan.Add(Squadmember.UnitManuvers.takecover);

        }
        else if ((squadLists[ID].health < squadLists[ID].Maxhealth) && !enemiesclose)
        {
            squadLists[ID].Plan.Clear();
            squadLists[ID].Plan.Add(Squadmember.UnitManuvers.heal);
        }
        else if (enemiesclose)
        {
            squadLists[ID].Plan.Clear();
            squadLists[ID].Plan.Add(Squadmember.UnitManuvers.takecover);
        }
        else
        { squadLists[ID].Plan.Clear(); }//go idle
    }
    // if simple solution not working begin cordinated behaviour
    private squadstates ExecutePlan(int ID)
    {
        // Debug.Log("executing");
        squadstates status;
        Squadmember.UnitManuvers Temp = Squadmember.UnitManuvers.idle;
        if (squadLists[ID].placeinPlan < squadLists[ID].Plan.Count)//to avoid null error
        {
            Temp = squadLists[ID].Plan[squadLists[ID].placeinPlan];
        }
        if (Temp == Squadmember.UnitManuvers.move)//change to case and switch
        {
            //Debug.Log("moving4");
            status = squadstates.moving;
            // squadMembers[ID].GetComponent<Squadmember>().destination = Orders.
            squadLists[ID].move(squadLists[ID].destination);
            if (squadMembers[ID].GetComponent<Squadmember>().message == Squadmember.Messages.atposition)
            {
                squadLists[ID].placeinPlan++;
            }

        }
        else if (Temp == Squadmember.UnitManuvers.attack)
        {
            // Debug.Log("attacking");
            status = squadstates.Attack;
            GameObject enemy = new GameObject();
            float shortestEnemyDistance = Mathf.Infinity;
            foreach (GameObject obj in enemies)
            {
                if (obj == null) continue;

                float distance = Vector3.Distance(squadLists[ID].GetPosition(), obj.transform.position);
                if (distance < shortestEnemyDistance)
                {
                    shortestEnemyDistance = distance;
                    enemy = obj;
                }
            }
            Vector3 bestPosition = FindNearestVisiblePosition(enemy.transform.position, 15f, coverLayer);

            if (bestPosition != Vector3.zero)
            {
                squadLists[ID].destination = bestPosition;
            }

            squadLists[ID].attack(enemy);
            squadLists[ID].placeinPlan++;
        }
        else if (Temp == Squadmember.UnitManuvers.takecover)
        {
            // Debug.Log("takingcover");
            status = squadstates.takingcover;
            Vector3 enemyPosition = Vector3.zero;
            float shortestEnemyDistance = Mathf.Infinity;
            foreach (GameObject obj in enemies)
            {
                if (obj == null) continue;

                float distance = Vector3.Distance(squadLists[ID].GetPosition(), obj.transform.position);
                if (distance < shortestEnemyDistance)
                {
                    shortestEnemyDistance = distance;
                    squadLists[ID].takecover(enemyPosition);

                }
            }

            squadLists[ID].placeinPlan++;
        }
        else if (Temp == Squadmember.UnitManuvers.heal)
        {
            //  Debug.Log("healing");
            status = squadstates.healing;
            squadLists[ID].heal();
            squadLists[ID].placeinPlan++;
        }
        else
        {
            // Debug.Log("idle");
            status = squadstates.idle;
        }
        //   SquadList[ID].placeinPlan++;
        return status;
    } */
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

