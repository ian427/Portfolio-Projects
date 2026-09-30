using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWindow : MonoBehaviour
{
    public PlayerMovment PMove;
    public bool CanSpawn = true;
    public GameObject[] Bullet;
     public GameObject Tempbullet;
    // Start is called before the first frame update
    void Start()
    {
        PMove = GameObject.Find("PlayerCannon").GetComponent<PlayerMovment>();
        int Index = Random.Range(0, Bullet.Length);
        Tempbullet = GameObject.Instantiate(Bullet[Index]);
        Tempbullet.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSpawn == true)
        {
            StartCoroutine(NewBullet());


        }
    }
    private IEnumerator NewBullet()
    { 
            CanSpawn = false;
            PMove.FireControl = false;
            Destroy(Tempbullet.gameObject);
            int Index = Random.Range(0, Bullet.Length);
            Tempbullet = GameObject.Instantiate(Bullet[Index]);
            Tempbullet.transform.position = this.transform.position;//spawns bullet at position of sets by
            yield return new WaitForSeconds(1.0f);
            PMove.FireControl = true;


    }
}
