using UnityEngine;
using System.Collections;

public class OnboardingHintCoroutine : MonoBehaviour
{
    [Header("Gesture")]
    public Transform gestureIcon;
    public float moveDistance = 1.5f;
    public float moveDuration = 0.8f;

    [Header("Lifetime")]
    public float maxDuration = 3f;

    [Header("Fade")]
    public float fadeDuration = 0.4f;

    [Header("Platform Highlight")]
    public GameObject platformGlow;

    Vector3 startPos;
    bool fading;
    SpriteRenderer[] renderers;

    void Start()
    {
        // Cache start data
        startPos = gestureIcon.position;
        renderers = GetComponentsInChildren<SpriteRenderer>();

        // Enable glow
        if (platformGlow != null)
            platformGlow.SetActive(true);

        // Start behaviour
        StartCoroutine(GestureLoop());
        StartCoroutine(CheckForInput());
        StartCoroutine(AutoRemove());
    }

    // -------------------------
    // Gesture Animation
    // -------------------------
    IEnumerator GestureLoop()
    {
        while (!fading)
        {
            yield return MoveGesture(startPos + Vector3.right * moveDistance);
            yield return MoveGesture(startPos - Vector3.right * moveDistance);
        }
    }

    IEnumerator MoveGesture(Vector3 target)
    {
        float t = 0f;
        Vector3 from = gestureIcon.position;

        while (t < 1f && !fading)
        {
            t += Time.unscaledDeltaTime / moveDuration;
            gestureIcon.position = Vector3.Lerp(from, target, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }
    }

    // -------------------------
    // Input Detection
    // -------------------------
    IEnumerator CheckForInput()
    {
        while (!fading)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
#else
            if (Input.touchCount > 0)
#endif
            {
                StartFadeOut();
                yield break;
            }

            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    // -------------------------
    // Auto Remove Timer
    // -------------------------
    IEnumerator AutoRemove()
    {
        yield return new WaitForSecondsRealtime(maxDuration);
        StartFadeOut();
    }

    // -------------------------
    // Fade Logic
    // -------------------------
    void StartFadeOut()
    {
        if (fading) return;
        fading = true;

        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        // Disable glow immediately
        if (platformGlow != null)
            platformGlow.SetActive(false);

        float t = 0f;
        Color[] startColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            startColors[i] = renderers[i].color;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / fadeDuration;

            for (int i = 0; i < renderers.Length; i++)
            {
                Color c = startColors[i];
                c.a = Mathf.Lerp(startColors[i].a, 0f, t);
                renderers[i].color = c;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
