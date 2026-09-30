
using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;
using System.Collections.Generic;


public class MapGeneratortest : MonoBehaviour
{

    public Texture2D mapTexture;
    public List<ColorToPrefab> colorMappings;
    public int chunkSize = 32;
    public NavMeshSurface navMeshSurface;
    public Grid2 CoverMap; // Optional grid reference
    public float fallbackTolerance = 0.1f; // Used if exact match fails

    [System.Serializable]
    public class ColorToPrefab
    {
        public Color color;
        public GameObject prefab;
    }

    void Start()
    {
        // Optional: Get grid reference if needed
        // CoverMap = GameObject.Find("gridgenerator").GetComponent<Gridtest>().grid;

        // Check texture readability
        if (!mapTexture.isReadable)
        {
            Debug.LogError("Map texture is not readable! Enable Read/Write in import settings.");
            return;
        }

        StartCoroutine(GenerateMapAsync());
    }

    IEnumerator GenerateMapAsync()
    {
        Debug.Log($"Starting map generation... Texture size: {mapTexture.width}x{mapTexture.height}");

        for (int chunkX = 0; chunkX < mapTexture.width; chunkX += chunkSize)
        {
            for (int chunkY = 0; chunkY < mapTexture.height; chunkY += chunkSize)
            {
                GenerateChunk(chunkX, chunkY, chunkSize);
                yield return null; // Wait one frame after each chunk
            }
        }

        Debug.Log("Map generation complete. Baking NavMesh...");
        BakeNavMesh();
    }

    void GenerateChunk(int startX, int startY, int size)
    {
        for (int x = startX; x < startX + size && x < mapTexture.width; x++)
        {
            for (int y = startY; y < startY + size && y < mapTexture.height; y++)
            {
                Color pixelColor = mapTexture.GetPixel(x, y);
                SpawnObjectBasedOnColor(pixelColor, x, y);
            }
        }
    }

    void SpawnObjectBasedOnColor(Color color, int x, int y)
    {
        // Ignore alpha
        color.a = 1f;

        bool matched = false;

        // First try exact match
        for (int i = 0; i < colorMappings.Count; i++)
        {
            if (ColorsMatchExact(colorMappings[i].color, color))
            {
                InstantiatePrefab(i, x, y);
                matched = true;
                break;
            }
        }

        // Fallback: tolerance-based match
        if (!matched)
        {
            for (int i = 0; i < colorMappings.Count; i++)
            {
                if (ColorsMatchTolerance(colorMappings[i].color, color, fallbackTolerance))
                {
                    InstantiatePrefab(i, x, y);
                    break;
                }
            }
        }
    }

    void InstantiatePrefab(int index, int x, int y)
    {
        Vector3 position = new Vector3(x, 0, y);

        // Optional: Store index in grid
        // if (CoverMap != null) CoverMap.SetValue(x, y, index);

        if (colorMappings[index].prefab != null)
        {
            Instantiate(colorMappings[index].prefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Prefab for color index {index} is null!");
        }
    }

    bool ColorsMatchExact(Color a, Color b)
    {
        return Mathf.RoundToInt(a.r * 255) == Mathf.RoundToInt(b.r * 255) &&
               Mathf.RoundToInt(a.g * 255) == Mathf.RoundToInt(b.g * 255) &&
               Mathf.RoundToInt(a.b * 255) == Mathf.RoundToInt(b.b * 255);
    }

    bool ColorsMatchTolerance(Color a, Color b, float tolerance)
    {
        return Mathf.Abs(a.r - b.r) <= tolerance &&
               Mathf.Abs(a.g - b.g) <= tolerance &&
               Mathf.Abs(a.b - b.b) <= tolerance;
    }

    void BakeNavMesh()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh baked successfully!");
        }
        else
        {
            Debug.LogWarning("NavMeshSurface not assigned.");
        }
    }
}