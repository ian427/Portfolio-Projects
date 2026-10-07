using UnityEngine;

public class PickUpcontroler : MonoBehaviour
{

    [SerializeField] private int HealthAddedOnpickup = 5;
    private Health health;
    private AbilityManager abilityManager;
    //incresse current health
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = this.GetComponent<Health>();
        abilityManager = this.GetComponent<AbilityManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("HealthPickUp"))
        {
            if (health.currentHealth < health.maxHealth)
            {
               health.AddHealth(HealthAddedOnpickup);
            }
            
           // Debug.Log("Health");
        }
        if (collision.CompareTag("AbilityPickup"))
        {
            abilityManager.AddAbility(collision.gameObject.GetComponent<PickupData>().abilityObj);
           // Debug.Log(collision.gameObject.GetComponent<PickupData>().abilityObj);

        }
       
        Destroy(collision.gameObject);
    }
}
