using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public BuildingControler building;
    public int CurrentNodeIndex;

    void Start()
    {
        // Assume you already know starting room
        var room = building.graph.rooms[CurrentNodeIndex];
        room.enemiesInRoom.Add(gameObject);
    }
}
