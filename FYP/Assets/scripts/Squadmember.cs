using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class Squadmember : MonoBehaviour
{
    //tuyrn into fuzzy logic
    public float wasshot = 0;//
    public bool incover = false ;
    public bool ismoving = false;
    public float healed = 0;
    public float Attacked = 0;
    public bool idle = false ; 
    public int health;
    [SerializeField]public int Maxhealth;
    [SerializeField] private GameObject Weapon;
    [SerializeField] private float ticdown = 0.1f;
    public LayerMask coverLayer;// objects that can be used as cover need to be in this layer mask
    private NavMeshAgent agent;
    public Vector3 destination;
    public enum Messages//unit doing
    {
        
        incover,
        moving,
        Attacking,
        healing,
        idle
    }
  
    public Messages status = Messages.idle;
   
     
       
        public GameObject assignedTarget;

        public void MoveTo(Vector3 pos)
        {
            // Use NavMeshAgent to move
            agent.SetDestination(pos);
        }

        public bool HasReachedDestination()
        {
            return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
        }

        public void UpdateBehavior()
        {
            // Handle moving, attacking, shooting
            if (status == Messages.Attacking && assignedTarget != null)
            {
                ShootAt(assignedTarget);
            }
        }

        void ShootAt(GameObject target)
        {
            // Implement shooting logic
        }
    

    public int placeinPlan;
       //public squadstates currentstate;
       

    
    //move
    //find and take cover
    //Select nearest enemy
    //heal
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = Maxhealth;
        agent = GetComponent<NavMeshAgent>();
        coverLayer = LayerMask.GetMask("Cover");
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet")
        {
            health -= 1;
        }
        wasshot = 1;
   
    }

    // Update is called once per frame
    public void SoldierUpdate()
    {
        //fuzzy logic tic down
        if (wasshot > 0)
        {
            wasshot -= ticdown;
        }
        if(wasshot <= 0)
        {
            wasshot = 0;
        }

        if (healed > 0)
        {
            healed -= ticdown;
        }
        if (healed <= 0)
        {
            healed = 0;
        }

        if (Attacked > 0)
        {
            Attacked -= ticdown;
        }
        if (Attacked <= 0)
        {
            Attacked = 0;
        }
        ////////////////////////
        if (health > Maxhealth)
        {
            health = 5;
        }
        if (health <= 0)
        {
            //destroyunit
            // deleate from squad list
        }
        if (transform.position == destination)
        {
            status = Messages.idle;
        }
    }
    public void heal()
    {
        if (health < Maxhealth )
        { 
            health++;
            status = Messages.healing;
        }
      
       
    }
    public void move(Vector3 newPos)
    {
        agent.SetDestination(newPos);
        status = Messages.moving;
        //Debug.Log("Moving");
    }
 
    public void attack(GameObject enemy)
    {

        Vector3 directionToEnemy = enemy.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        Weapon.transform.rotation = lookRotation;
        Weapon.GetComponent<Weaponcontrol>().Attack();
        Attacked = 1;
        status = Messages.Attacking;
    }
    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
    
}
