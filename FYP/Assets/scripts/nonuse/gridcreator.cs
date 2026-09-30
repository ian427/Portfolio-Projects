using UnityEngine;

public class gridcreator : MonoBehaviour
{
    [SerializeField] private Transform debugGridObjectPrefab;

    private Grid grid;

    void Start()
    {
        // width = 20, height = 10, cell size = 5 units
        Grid grid= new Grid(20, 10, 5f, Vector3.zero, debugGridObjectPrefab);
         
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            grid.SetValue(mousePosition, 99);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point; // Raycast against floor
        }
        return Vector3.zero;
    }
}

