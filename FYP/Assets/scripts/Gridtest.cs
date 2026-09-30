using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Gridtest : MonoBehaviour
{
    public SimulationSettings settings;
    public Grid2 Heatmap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 LastGridUpdatePoint;
    private Vector2 CurrentGridUpdatePoint;
    public Grid2 grid;
    void Awake()
    {
         grid = new Grid2(300, 300 ,1f);
        Heatmap = grid; 
    }
    public void UpdateHeatmap(Vector3 Pos, string Tag)
    {
        int x = (int)Pos.x;
        int z = (int)Pos.z;
        CurrentGridUpdatePoint.y = z;
        CurrentGridUpdatePoint.x = x;
        if(CurrentGridUpdatePoint!=LastGridUpdatePoint)
        {
            if (Tag == "Player1")
            {
                Heatmap.SetValue(x, z, 1);

            }
            else if (Tag == "Player2")
            {
                Heatmap.SetValue(x, z, -1);

            }
            LastGridUpdatePoint.x = x;
            LastGridUpdatePoint.y = z;

        }
        
    }
        // Update is called once per frame
        void Update()
        {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        switch (settings.GameState)
        {
            case 1:


            break;
        }

        }
}

