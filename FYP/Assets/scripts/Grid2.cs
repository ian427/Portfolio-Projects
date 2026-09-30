using CodeMonkey.Utils;
using System.Drawing;
using UnityEngine;
public class Grid2
{
    private int width, height;
    private float Cellsize;
    private int[,] gridArray;
    private TextMesh[,] TextArray;
    /*
    public static TextMesh createWorldText (Transform parent = null, Vector3 localPosition = default(Vector3)int fontSize = 40 ,Color color ,textAnchor)
    {
        if (color == null) color = color.white;
        return createWorldText(parent,createWorldText,localPosition,fontSize,(Color)color,textAnchor,textAlignment,sortingOrder)
    }
    public static TextMesh CreateWorldText(transform)
        //codemonkey utils package
    */
    private Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z) * Cellsize;
    }
    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = value;
            //TextArray[x, z].text = value.ToString();
            Debug.Log(value.ToString());
        }
    }
    public int GetValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return gridArray[x, z];
        }
        return -1; // or some sentinel for invalid
    }

    public Grid2(int width, int height, float Cellsize)
    {
        this.width = width;
        this.height = height;
        gridArray = new int[width, height];
        TextArray = new TextMesh[width, height];
        // Debug.Log(width + "" + height);
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            { 
                /*
                TextMesh temp;
                //Debug.Log(i + "" + y);
               
               temp = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition( x,  y,2), 10, UnityEngine.Color.white,TextAnchor.MiddleCenter);
                temp.transform.position = new Vector3(x, 0, y)*Cellsize;
                TextArray[x, y] = temp;
                */
            }
        }
    }

    
    
}