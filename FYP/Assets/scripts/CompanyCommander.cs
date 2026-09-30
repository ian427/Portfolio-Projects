using System;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class CompanyCommander : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // list of waypoints

    public List<GameObject> Squads;//list of emptys created with Squad Leader Script
    private List<GameObject> SquadsToUpdate;
    public GameObject Target;//assigned area
    public int squadsleftToassign = 0;
    public int RemainingForceValue = 0;
    public GameObject NewSquadleaderPrefab;
    public List<SquadLeader> SquadScripts = new List<SquadLeader>();//squad scripts
    public List<GameObject> Areas = new List<GameObject>();//buildings
    public List<BuildingControler> Buildingscripts = new List<BuildingControler>();
    public List<GameObject> BuildingsUnderAssault = new List<GameObject>();//buildings
    //public List<Squadcount> SquadsAvaliable = new();
    public int allowedforceover = 3;

    /*public class Squadcount
    {
        public GameObject SquadRefrence;
        public int Membersinsquad;

        public Squadcount(GameObject obj, int value)
        {
            this.SquadRefrence = obj;
            this.Membersinsquad = value;
        }
        public void UpdatenumberofMembersinsquad(int value)
        {
            Membersinsquad = value;
        }
    }*/
    void Start()
    {


        //foreach (GameObject building in Target.GetComponent<CombatAreaController>().Buildings)
        //{
        //    Buildingscripts.Add(building.GetComponent<BuildingControler>());
        //}


    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void Distributesquadstobuildings()
    {//first count howmany squads remain and how many soldiers total are still alive
     // squadsleftToassign = 0;
     // RemainingForceValue = 0;

        List<GameObject> SquadsAvaliable = new List<GameObject>(this.Squads);


        foreach (SquadLeader obj in SquadScripts)
        {
            squadsleftToassign += SquadScripts.Count;
            RemainingForceValue += obj.MembersinSquad;
            obj.ghostsquad = false;

        }

        //seleting next area to attack creating and assigning squads
        foreach (BuildingControler obj in Buildingscripts)
        {
            if (obj.EnemiesInArea < RemainingForceValue)
            {
                BuildingsUnderAssault.Add(obj.gameObject);
                int forcetoequal = obj.EnemiesInArea;
                int forceassignedtobuilding = 0;
                bool hasloopedthroughlist = false;
                bool forceisassigned = false;
                //V
                while (!forceisassigned)
                {



                    for (int i = 0; i < Squads.Count; i++)//go through eac squad
                    {
                        if (!forceisassigned)//if force assigned goal has not been reached
                        {
                            if (hasloopedthroughlist)// assign squad to building
                            {
                                SquadScripts[i].TargetBuilding = obj.gameObject;
                                RemainingForceValue -= SquadScripts[i].MembersinSquad;
                                forceassignedtobuilding += SquadScripts[i].MembersinSquad;
                                SquadsAvaliable.RemoveAt(i);
                                i--;
                            }
                            else
                            {
                                //check if force will be added over boundrycheck
                                //((soldiers captain has+soldiers that will be assigned)-soldiers need to complete area)
                                if (((forceassignedtobuilding + SquadScripts[i].MembersinSquad) - forcetoequal) <= allowedforceover)
                                {
                                    SquadScripts[i].TargetBuilding = obj.gameObject;
                                    RemainingForceValue -= SquadScripts[i].MembersinSquad;
                                    forceassignedtobuilding += SquadScripts[i].MembersinSquad;
                                    SquadsAvaliable.RemoveAt(i);
                                    i--;
                                }
                            }
                            if (forcetoequal <= forceassignedtobuilding) { forceisassigned = true; }

                        }
                    }
                    hasloopedthroughlist = true;
                }

            }
        }

        foreach (var building in BuildingsUnderAssault)
        {
            int assignedsquads = 0;
            int Entrypointcurrentassigningindex = 0;
            List<SquadLeader> SquadsAssignedToThisBuilding = new List<SquadLeader>();//squads assigned to building
            BuildingControler temporybuilding = building.GetComponent<BuildingControler>();
            foreach (SquadLeader squad in SquadScripts)
            {

                if (building == squad.TargetBuilding)
                {


                    SquadsAssignedToThisBuilding.Add(squad);
                    squad.entrypointindex = Entrypointcurrentassigningindex;
                    Entrypointcurrentassigningindex++;
                    if (Entrypointcurrentassigningindex >= temporybuilding.graph.outsideEntryPoints.Count) { Entrypointcurrentassigningindex = 0; }//checking index is not invalid

                }

            }
            if (SquadsAssignedToThisBuilding.Count > 1)/// assigning entry points 
            {
                for (int i = 0; i < temporybuilding.graph.outsideEntryPoints.Count; i++)
                {
                    GameObject Ghostsquad = Instantiate(NewSquadleaderPrefab);
                    bool entryassigned = false;
                    foreach (SquadLeader squad in SquadsAssignedToThisBuilding)
                    {
                        if (squad.entrypointindex == i)
                        {
                            foreach (GameObject Member in squad.squadMembers)
                            {
                                Ghostsquad.GetComponent<SquadLeader>().squadMembers.Add(Member);
                                Ghostsquad.GetComponent<SquadLeader>().entrypointindex = SquadsAssignedToThisBuilding[0].entrypointindex;//wrong
                            }
                            if (!entryassigned)
                            {
                                Ghostsquad.GetComponent<SquadLeader>().entrypointindex = squad.entrypointindex;
                            }
                        }
                    }
                    Ghostsquad.GetComponent<SquadLeader>().buildingnodemap = SquadsAssignedToThisBuilding[0].buildingnodemap;
                    SquadsToUpdate.Add(Ghostsquad);
                }
                foreach (SquadLeader squad in SquadsAssignedToThisBuilding)//turn off squad leaders
                {
                    squad.ghostsquad = true;
                }
            }

        }
    }
    public void buildingclear(GameObject Squad)
    {
        SquadsToUpdate.Remove(Squad);
    }
    public void UpdateSquads()
    {
        //calculat situation
        //execute plans

    }
}




