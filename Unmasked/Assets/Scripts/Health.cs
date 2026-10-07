using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    
    [SerializeField]public int currentHealth;
    [SerializeField]
    public int maxHealth = 100;
    private int shield = 0;
    [SerializeField] private Slider HealthBar;
    [SerializeField] private float duration;//in seconds

    public bool isPlayer = false;

    void Start()
    {

        currentHealth = maxHealth;


        Slider[] HealthBars = FindObjectsByType<Slider>(FindObjectsSortMode.None);


        if (isPlayer)
        {
            foreach (Slider healthBar in HealthBars)
            {
                if (healthBar.tag == "PlayerBar")
                {
                    Debug.Log("Found Player Health Bar");
                    HealthBar = healthBar;
                }
            }
        }
        else
        {
            foreach (Slider healthBar in HealthBars)
            {
                if (healthBar.tag == "EnemyBar")
                {
                    Debug.Log("Found Enemy Health Bar");
                    HealthBar = healthBar;
                }
            }

        }


            StartCoroutine(SmoothScroll(currentHealth / (float)maxHealth));
    }

    public void AddShield(int amount)
    {
        shield += amount;
    }

    public int GetShield()
    {
        return shield;
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        if ((currentHealth > maxHealth))
        {
            currentHealth = maxHealth;
        }

        float adjustment = currentHealth / (float)maxHealth;
        StartCoroutine(SmoothScroll(adjustment));

    }

    public void TakeDamage(int amount)
    {
        
        
        currentHealth -= amount;
        //HealthBar.value = currentHealth/maxHealth;

        if(shield > 0)
        {
            shield = 0;
        }

        float adjustment = currentHealth / (float)maxHealth;
        StartCoroutine(SmoothScroll(adjustment));

        if(currentHealth <= 0)
        {
            Death();
        }

        //Debug.Log("Scrolling");

    }

    public void DamageShield()
    {
        float s = shield;
        s /= 1.5f;
        shield = Mathf.FloorToInt(s);
    }

    private void Death()
    {
        if(!isPlayer)
        {
            //Player wins
            Gamemode gm = FindFirstObjectByType<Gamemode>();
            StartCoroutine(gm.PlayerWin());
        }
        else
        {
            //Player loses
            Gamemode gm = FindFirstObjectByType<Gamemode>();
            gm.PlayerDeath();
            StartCoroutine(gm.PlayerDeath());
        }
    }


    public IEnumerator SmoothScroll( float target)
    {
        float startValue = HealthBar.value;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            HealthBar.value = Mathf.Lerp(startValue, target, t);

            yield return null; // wait for next frame
        }

        HealthBar.value = target; // snap exactly at the end
    }

}
