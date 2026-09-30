using UnityEngine;
using TMPro;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;

    private int[,] gridArray;
    private TextMeshPro[,] debugTextArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Transform debugGridObjectPrefab)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMeshPro[width, height];

        // Spawn debug objects
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 worldPos = GetWorldPosition(x, z) + new Vector3(1, 0, 1) * cellSize * 0.5f;

                Transform debugTransform = GameObject.Instantiate(debugGridObjectPrefab, worldPos, Quaternion.identity);
                TextMeshPro textMesh = debugTransform.GetComponent<TextMeshPro>();
                textMesh.text = gridArray[x, z].ToString();
                debugTextArray[x, z] = textMesh;

                // Draw grid lines in XZ plane
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        // Test: set one value
       // SetValue(2, 1, 56);
    }

    private Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize + originPosition;
    }

    private void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        worldPosition -= originPosition;
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }

    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            if (gridArray[x,z]< 100 || gridArray[x,z]>-100)
            {
              gridArray[x, z] += value;
              debugTextArray[x, z].text = value.ToString();

            }
            
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        GetXZ(worldPosition, out int x, out int z);
        SetValue(x, z, value);
    }
    public void GetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = value;
            debugTextArray[x, z].text = value.ToString();
        }
    }
}

