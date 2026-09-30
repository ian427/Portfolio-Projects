using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private GameObject Tempbullet;
    public GameObject[] Ships;
    public int Collum = 5;
    public int file = 5;
    public float spacing = 2f;
    private Vector2 MyPos;//spawner position
    void Start()
    {
       
        MyPos = transform.position;
        for (int i = 0; i < Collum; i++)//will move and create a collum
        {

            for (int f = 0; f < file; f++)// moves acreates a file
            {
                
                Vector2 NextSpawn = MyPos;//nextposition to spawn ship at
                NextSpawn = new Vector2(NextSpawn.x + spacing, MyPos.y); //move spawn
                transform.position = NextSpawn; // MOVES OBJECT
                int Index = UnityEngine.Random.Range(0, Ships.Length);
                Instantiate(Ships[Index], transform.position, transform.rotation); //SPAWNS
                                                                                   Debug.Log("spawned");
            }
        }
    }
}
