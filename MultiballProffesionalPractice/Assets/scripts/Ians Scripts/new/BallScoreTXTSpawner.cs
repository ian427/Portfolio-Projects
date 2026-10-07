using TMPro;
using UnityEngine;

using UnityEngine;
using TMPro;

public class BallScoreTXTSpawner : MonoBehaviour
{
    [Header("UI")]
    public Canvas canvas;                   // Screen Space - Overlay (or Camera)
    public TextMeshProUGUI textPrefab;      // TMP UGUI prefab
    public float lifetime = 1.25f;          // auto-destroy delay
    public Vector2 pixelOffset = new Vector2(0f, 20f); // small nudge upward
    [SerializeField] private HealthDown health;
    [Header("Game")]
    public Score score;

    Camera cam;

    void Awake()
    {
        cam = Camera.main; // cache for performance; replace if you use a different camera
        // Optionally: score ??= GameObject.Find("Manager")?.GetComponent<Score>();
        // Optionally: canvas ??= GameObject.Find("Canvas")?.GetComponent<Canvas>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        bool validCollision = false;
        string displayText = "Hit!";

        if (col.gameObject.CompareTag("Class3"))      // 50 pts
        {
            score.AddScore(50);
            displayText = "50";
            validCollision = true;
        }
        else if (col.gameObject.CompareTag("Class2")) // 100 pts
        {
            score.AddScore(100);
            displayText = "100";
            validCollision = true;
        }
        else if (col.gameObject.CompareTag("Class1")) // 1000 pts
        {
            score.AddScore(1000);
            displayText = "1000";
            validCollision = true;
        }
        if (col.gameObject.CompareTag("Heart"))      // 50 pts
        {
            health.LivesLeft++;
            displayText = "Extra Life";
            validCollision = true;
            col.gameObject.SetActive(false);
            health.SetHealthcount();
        }

            if (!validCollision) return;

        // --- Get a collision point in world space ---
        Vector2 worldHit = (col.contactCount > 0) ? col.GetContact(0).point : (Vector2)transform.position;

        // --- Convert world -> screen pixels ---
        Vector2 screenPos = cam != null ? (Vector2)cam.WorldToScreenPoint(worldHit) : worldHit;

        // --- Convert screen -> canvas local position ---
        RectTransform canvasRect = canvas.transform as RectTransform;
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : cam, // null for Overlay
            out localPos
        );

        // --- Spawn and position the UI text ---
        TextMeshProUGUI t = Instantiate(textPrefab, canvasRect);
        t.rectTransform.anchoredPosition = localPos + pixelOffset;
        t.text = displayText;

        // Optional: random slight rotation or scale for variety
        // t.rectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-5f, 5f));

        // Cleanup
        if (lifetime > 0f)
            Destroy(t.gameObject, lifetime);
    }
}
