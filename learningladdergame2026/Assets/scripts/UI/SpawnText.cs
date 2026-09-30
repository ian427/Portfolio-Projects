using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SpawnText : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("UI")]
    [SerializeField] private Canvas canvas;                   // Screen Space - Overlay (or Camera)
    [SerializeField] private TextMeshProUGUI textPrefab;      // TMP UGUI prefab
    [SerializeField] private float lifetime = 1.25f;          // auto-destroy delay
    private Vector3 SpawnPoint = new Vector3(0f, 0f, 0f); // spawn point
    [Header("position the empty were you want the text to spawn")]
    [SerializeField] private GameObject EmptySpawnPoint;

    


    Camera cam;

    void Awake()
    {
        cam = Camera.main; // cache for performance; replace if you use a different camera
          SpawnPoint = EmptySpawnPoint.transform.position;
    }

    

    public void SpawnTxt(string score)
    {
        RectTransform canvasRect = canvas.transform as RectTransform;

        // World -> Screen
        Vector2 screenPos = Camera.main.WorldToScreenPoint(SpawnPoint);

        // Screen -> Canvas Local
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.worldCamera,
            out localPos);

        TextMeshProUGUI t = Instantiate(textPrefab, canvasRect);

        t.rectTransform.anchoredPosition = localPos ;
        t.text = score;

        if (lifetime > 0f)
            Destroy(t.gameObject, lifetime);
    }
}
