using UnityEngine;

public class TogglePrefab : MonoBehaviour
{
    [SerializeField] private GameObject prefabInstance;

    public void Open()
    {
        prefabInstance.SetActive(true);
    }

    public void Close()
    {
        prefabInstance.SetActive(false);
    }
}