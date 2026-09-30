using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Commander : MonoBehaviour
{
    public int squadsleftToassign = 0;
    public int RemainingForceValue = 0;
    public GameObject Captain;
    public List<GameObject> Captains = new List<GameObject>();
    public List<CompanyCommander> CaptainScripts = new List<CompanyCommander>();
    public List<GameObject> Areas = new List<GameObject>();//must be added in order that each area can ba acessed
    public List<CombatAreaController> AreaScrpits = new List<CombatAreaController>();
    public List<Squadcount> SquadsAvaliable = new();
    public int allowedforceover = 3;
    public class Squadcount
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
    }
 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CreateCaptain (GameObject obj)
    {
        Captains.Add(obj);
        CaptainScripts.Add(obj.GetComponent<CompanyCommander>());
    }
    public void RemoveCaptains()
    {
        foreach (GameObject obj in Captains)
        {
            Destroy(obj);
        }
    }
    public void evaluateAreas()
    {//first count howmany squads remain and how many soldiers total are still alive
       // squadsleftToassign = 0;
       // RemainingForceValue = 0;
        SquadsAvaliable.Clear();
        foreach (CompanyCommander obj in CaptainScripts)
        {
            squadsleftToassign += obj.SquadScripts.Count;
            
            foreach (SquadLeader n in obj.SquadScripts)
            { //acount remaining squads and soldiers left in that squad
                SquadsAvaliable.Add(new Squadcount(n.gameObject, n.MembersinSquad));
                squadsleftToassign++;
                RemainingForceValue += n.MembersinSquad;
            }
        }
       

        //clear captains
        RemoveCaptains();
            Captains.Clear();
            CaptainScripts.Clear();
        List<Squadcount> Squads = new List<Squadcount>(SquadsAvaliable);
        //seleting next area to attack creating and assigning squads
        foreach (CombatAreaController obj in AreaScrpits)
        {
            if (obj.EnemiesInArea < RemainingForceValue)
            {
                // create captain and assign squads and area as needed
               
                GameObject temp = Instantiate(Captain);
                Captains.Add(temp);
                CaptainScripts.Add(temp.GetComponent<CompanyCommander>());
                CaptainScripts[CaptainScripts.Count - 1].Target = obj.gameObject;
                int forcetoequal = obj.EnemiesInArea;
                int forcegiventocaptain = 0;
                bool hasloopedthroughlist = false;
                bool forceisassigned = false;
                //V
                while (!forceisassigned)
                {
                    // adding squads untul captain has enough force to take area//add* force multiplyir if it doesnt work


                    for (int i = 0; i < Squads.Count; i++)//go through eac squad
                    {
                        if (!forceisassigned)//if force assigned goal has not been reached
                        {
                            if(hasloopedthroughlist)
                            {
                                CaptainScripts[CaptainScripts.Count - 1].Squads.Add(Squads[i].SquadRefrence);
                                RemainingForceValue -= Squads[i].Membersinsquad;
                                forcegiventocaptain += Squads[i].Membersinsquad;
                                Squads.RemoveAt(i);
                                i--;
                            }
                            else 
                            {
                                //check if force will be added over boundrycheck
                                //((soldiers captain has+soldiers that will be assigned)-soldiers need to complete area)
                                if (((forcegiventocaptain + Squads[i].Membersinsquad)- forcetoequal) <= allowedforceover)
                                {
                                    CaptainScripts[CaptainScripts.Count-1].Squads.Add(Squads[i].SquadRefrence);
                                    RemainingForceValue -= Squads[i].Membersinsquad;
                                    forcegiventocaptain += Squads[i].Membersinsquad;
                                    Squads.RemoveAt(i);
                                    i--;
                                }
                            }
                            if (forcetoequal <= forcegiventocaptain)  { forceisassigned = true; }

                        }
                    }
                    hasloopedthroughlist = true;
                }
                
            }
        }

    }
    void Start()
    {

        foreach (GameObject obj in Areas)
        {
            AreaScrpits.Add(obj.GetComponent<CombatAreaController>());
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}