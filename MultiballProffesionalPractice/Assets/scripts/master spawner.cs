using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Prefabs (5 total)")]
    public GameObject[] obstaclePrefabs;

    [Header("Spawn Points (7 total)")]
    public Transform[] spawnPoints;

    [Header("How many obstacles to spawn per cycle")]
    public int spawnAmount = 3;

    [Header("Respawn Delay")]
    public float respawnTime = 5f;

    private List<GameObject> currentObstacles = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnSet();
            yield return new WaitForSeconds(respawnTime);
            ClearSet();
        }
    }

    void SpawnSet()
    {
        List<Transform> points = new List<Transform>(spawnPoints);

        for (int i = 0; i < spawnAmount; i++)
        {
            int pointIndex = Random.Range(0, points.Count);
            Transform chosenPoint = points[pointIndex];
            points.RemoveAt(pointIndex);

            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            GameObject newObj = Instantiate(prefab, chosenPoint.position, Quaternion.identity);

            currentObstacles.Add(newObj);
        }
    }

    void ClearSet()
    {
        foreach (GameObject obj in currentObstacles)
        {
            if (obj != null) Destroy(obj);
        }
        currentObstacles.Clear();
    }
}
