using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Health health;
    public Slider healthBar;
    public int mainHealth;
    float currentVelocity = 0;

    public int value { get; internal set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float curHealth = Mathf.SmoothDamp(healthBar.value,mainHealth, ref currentVelocity, 100 * Time.deltaTime);
        health = GetComponent<Health>();
        healthBar.value = curHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
