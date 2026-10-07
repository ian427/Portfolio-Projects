using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Scrollbar PlayerHealthBar;
    [SerializeField] private Scrollbar EnemyHealthBar;
    [SerializeField]private GameObject player;
    [SerializeField] private GameObject enemy;
    private Health playerHealth;
    private Health enemyHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.GetComponent<Health>();
        enemy.GetComponent<Health>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
