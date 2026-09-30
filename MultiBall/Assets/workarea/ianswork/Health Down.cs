using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class HealthDown : MonoBehaviour
{
    [SerializeField] private int LivesLeft = 3;
    [SerializeField] private List<GameObject> HealthObjects = new List<GameObject>();
    public GameManager manager;
    [SerializeField]
    private GameObject Effect;
    private ParticleControler Pcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pcon = Effect.GetComponent<ParticleControler>();
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        // Destroy(obj.gameObject);
        obj.gameObject.SetActive(false);
        Pcon.PlayEffect();
        // HealthObjects.Remove(HealthObjects[LivesLeft])
        Destroy(HealthObjects[(LivesLeft-1)].gameObject);
        LivesLeft--;
        //Debug.Log("triggered");

    }
   
        // Update is called once per frame
        void Update()
        {
          if (LivesLeft < 0 || LivesLeft == 0)
          {
            //Debug.Log("Gameover");
            manager.GameOver();
          }
        
        }
}
