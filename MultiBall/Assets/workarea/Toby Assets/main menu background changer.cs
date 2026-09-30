using System.Collections;
using UnityEngine;

public class SpriteCrossFade : MonoBehaviour
{
    public SpriteRenderer mainRenderer;  
    public Sprite[] sprites;             

    public float timeBetweenChanges = 5f;
    public float fadeDuration = 1f;

    private SpriteRenderer fadeRenderer;
    private int currentIndex = 0;

    void Start()
    {
        if (sprites == null || sprites.Length == 0)
            return;

        // Create fade renderer
        fadeRenderer = new GameObject("FadeRenderer").AddComponent<SpriteRenderer>();

        fadeRenderer.transform.SetParent(mainRenderer.transform, false);
        fadeRenderer.transform.localPosition = Vector3.zero;

        // IMPORTANT FIX: use separate material so alpha changes work reliably
        fadeRenderer.material = new Material(mainRenderer.material);

        fadeRenderer.sortingLayerID = mainRenderer.sortingLayerID;
        fadeRenderer.sortingOrder = mainRenderer.sortingOrder + 1;
        fadeRenderer.color = new Color(1, 1, 1, 0);

        mainRenderer.sprite = sprites[currentIndex];

        StartCoroutine(CycleSprites());
    }

    IEnumerator CycleSprites()
    {
        while (true)
        {
            int nextIndex = (currentIndex + 1) % sprites.Length;
            Sprite nextSprite = sprites[nextIndex];

            // Set next sprite on fadeRenderer
            fadeRenderer.sprite = nextSprite;
            fadeRenderer.color = new Color(1, 1, 1, 0);

            float t = 0f;

            // Crossfade
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                float a = Mathf.Clamp01(t / fadeDuration);

                fadeRenderer.color = new Color(1, 1, 1, a);      // Fade in next
                mainRenderer.color = new Color(1, 1, 1, 1 - a);  // Fade out current

                yield return null;
            }

            // Swap complete
            mainRenderer.sprite = nextSprite;
            mainRenderer.color = Color.white;
            fadeRenderer.color = new Color(1, 1, 1, 0);

            currentIndex = nextIndex;

            yield return new WaitForSeconds(timeBetweenChanges);
        }
    }
}
