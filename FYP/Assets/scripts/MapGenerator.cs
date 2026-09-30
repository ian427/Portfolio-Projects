using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public Texture2D mapTexture;
    public List<ColorToPrefab> colorMappings;
    public int chunkSize = 32;
    public NavMeshSurface navMeshSurface;
    public Grid2 CoverMap;
    private Dictionary<Color, int> colorIndexMap;
    public Color wallcolour;
    public float fallbackTolerance = 0.1f; // Adjustable in Inspector

    [System.Serializable]
    public class ColorToPrefab
    {
        public Color color;
        public GameObject prefab;
    }

    void Start()
    {
        // Get the grid reference
        CoverMap = GameObject.Find("gridgenerator").GetComponent<Gridtest>().grid;

        // Build dictionary for fast color-to-index lookup
        colorIndexMap = new Dictionary<Color, int>();
        for (int i = 0; i < colorMappings.Count; i++)
        {
            colorIndexMap[colorMappings[i].color] = i;
        }

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
        Debug.Log("Starting map generation...");
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
                pixelColor.a = 1f; // Ignore alpha
                SpawnObjectBasedOnColor(pixelColor, x, y);
            }
        }
    }

    void SpawnObjectBasedOnColor(Color color, int x, int y)
    {
        // Try exact match from dictionary
        if (colorIndexMap.TryGetValue(color, out int index))
        {
            InstantiatePrefab(index, x, y, color);
        }
        else
        {
            // Fallback: tolerance-based match
            for (int i = 0; i < colorMappings.Count; i++)
            {
                if (ColorsMatch(colorMappings[i].color, color, fallbackTolerance))
                {
                    InstantiatePrefab(i, x, y, color);
                    break;
                }
            }
        }
    }

    void InstantiatePrefab(int index, int x, int y, Color color)
    {
        Vector3 position = new Vector3(x, 0, y);

        // Store index in CoverMap
        if (CoverMap != null)
        {
            CoverMap.SetValue(x, y, index);

            // Special handling for wall color
            if (ColorsMatch(wallcolour, color, fallbackTolerance))
            {
                CoverMap.SetValue(x + 1, y, index);
                CoverMap.SetValue(x - 1, y, index);
                CoverMap.SetValue(x, y + 1, index);
                CoverMap.SetValue(x, y - 1, index);
                CoverMap.SetValue(x, y, 100);
            }
        }

        // Instantiate prefab
        if (colorMappings[index].prefab != null)
        {
            Instantiate(colorMappings[index].prefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Prefab for color index {index} is null!");
        }
    }

    bool ColorsMatch(Color a, Color b, float tolerance)
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