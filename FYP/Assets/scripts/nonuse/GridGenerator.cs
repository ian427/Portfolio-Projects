using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour
{
    public GameObject UnitSightBlock;

    public int Collum = 5;
    public int file = 5;
    public float spacing = 2f;
    private Vector3 MyPos;//spawner position
    void Start()
    {

        MyPos = transform.position;
        for (int i = 0; i < Collum; i++)//will move and create a collum
        {

            for (int f = 0; f < file; f++)// moves acreates a file
            {

                Vector3 NextSpawn = MyPos;//nextposition to spawn ship at
                NextSpawn = new Vector3(MyPos.x + i, 1,MyPos.z+ f + spacing); //move spawn
                transform.position = NextSpawn; // MOVES OBJECT
                Instantiate(UnitSightBlock, transform.position, transform.rotation); //SPAWNS
                Debug.Log("spawned");
            }
        }
    }
}
