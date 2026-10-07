using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class HealthDown : MonoBehaviour
{
    [SerializeField] public int LivesLeft = 3;
    //[SerializeField] private List<GameObject> HealthObjects = new List<GameObject>();
    public GameManager manager;
    public Spawnballs Spawn;
    [SerializeField]
    private GameObject Effect;
    public TextMeshProUGUI LivesLefttxt;
    private ParticleControler Pcon;
    private int RespawnDelay = 5;
    [SerializeField] public GameObject lifelayer1;
    [SerializeField] public GameObject lifelayer2;
    [SerializeField] private GameObject Ship;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pcon = Effect.GetComponent<ParticleControler>();
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        Spawn = GameObject.Find("Manager").GetComponent<Spawnballs>();
        LivesLefttxt.text = $"{LivesLeft} lives remaining";
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        // Destroy(obj.gameObject);
        if (obj.gameObject.tag == "Ball")
        {

            obj.gameObject.SetActive(false);

            // HealthObjects.Remove(HealthObjects[LivesLeft])

            StartCoroutine(PlayAnimationCoroutine(LivesLeft - 1));
            //Destroy(HealthObjects[(LivesLeft-1)].gameObject);
            //LivesLeft--;
            LivesLeft--;
            if (LivesLeft == 2) { lifelayer1.SetActive(true); LivesLefttxt.text = $"{LivesLeft} lives remaining"; }

            if (LivesLeft == 1) { lifelayer2.SetActive(true); lifelayer1.SetActive(true); LivesLefttxt.text = $"{LivesLeft} lives remaining"; }
            if (LivesLeft == 0) { Pcon.PlayEffect(); LivesLefttxt.text = $"{LivesLeft} lives remaining"; }
            if (LivesLeft >= 3) { lifelayer2.SetActive(false); lifelayer1.SetActive(false); LivesLefttxt.text = $"{LivesLeft} lives remaining"; }
            //Spawn.CurrentSpawned--;
            StartCoroutine(StartRespawnCount());
        }
        //Debug.Log("triggered");

    }

    // Update is called once per frame
    void Update()
    {

        if (LivesLeft < 0 || LivesLeft == 0)
        {
            //Debug.Log("Gameover");
            manager.GameOver();

            Ship.SetActive(false);

        }

    }
    System.Collections.IEnumerator PlayAnimationCoroutine(int index)
    {

        if (index >= 0)
        {
            // HealthObjects[(index)].gameObject.GetComponent<TestHeartControler>().PlayAnimation();
            //Animator m_Animator = HealthObjects[(index)].gameObject.GetComponent<Animator>();
            //yield return new WaitUntil(() => m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
            yield return new WaitForSeconds(1.9f);
            // HealthObjects[(index)].gameObject.GetComponent<ParticleControler>().PlayEffect();
            //  HealthObjects[(index)].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    System.Collections.IEnumerator StartRespawnCount()
    {
        yield return new WaitForSeconds(RespawnDelay);
        Spawn.Respawn();

    }
    public void SetHealthcount()
    {
        if (LivesLeft == 2) { lifelayer1.SetActive(true); lifelayer2.SetActive(false); LivesLefttxt.text = $"{LivesLeft} lives remaining"; }

        if (LivesLeft == 1) { lifelayer2.SetActive(true); lifelayer1.SetActive(true); LivesLefttxt.text = $"{LivesLeft} lives remaining"; }
        if (LivesLeft == 0) { Pcon.PlayEffect(); LivesLefttxt.text = $"{LivesLeft} lives remaining"; }
        if (LivesLeft >= 3) { lifelayer2.SetActive(false); lifelayer1.SetActive(false); LivesLefttxt.text = $"{LivesLeft} lives remaining"; }
    }
}
