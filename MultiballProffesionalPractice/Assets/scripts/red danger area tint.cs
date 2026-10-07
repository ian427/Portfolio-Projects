using UnityEngine;
using System.Collections;

public class DangerZoneController : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer redOverlay;
    public Transform[] balls;

    [Header("Zone Bounds (World Y)")]
    public float zoneTopY;
    public float zoneBottomY;

    [Header("Visual Tuning")]
    public float maxAlpha = 0.35f;
    public float smoothSpeed = 10f;
    public float resetSpeed = 16f;

    float currentAlpha;

    void Start()
    {
        SetAlpha(0f);
        StartCoroutine(DangerLoop());
    }

    IEnumerator DangerLoop()
    {
        while (true)
        {
            float lowestBallY = float.MaxValue;

            foreach (Transform ball in balls)
            {
                if (ball == null) continue;
                lowestBallY = Mathf.Min(lowestBallY, ball.position.y);
            }

            float targetAlpha = 0f;

            // Only activate danger if a ball is inside the zone
            if (lowestBallY < zoneTopY && lowestBallY > zoneBottomY)
            {
                float dangerAmount = Mathf.InverseLerp(zoneTopY, zoneBottomY, lowestBallY);
                targetAlpha = dangerAmount * maxAlpha;
            }

            // Faster fade-out when safe
            float speed = targetAlpha > currentAlpha ? smoothSpeed : resetSpeed;

            currentAlpha = Mathf.Lerp(
                currentAlpha,
                targetAlpha,
                Time.unscaledDeltaTime * speed
            );

            // HARD CLAMP to zero when safe
            if (targetAlpha == 0f && currentAlpha < 0.01f)
                currentAlpha = 0f;

            SetAlpha(currentAlpha);
            yield return null;
        }
    }

    void SetAlpha(float a)
    {
        Color c = redOverlay.color;
        c.a = a;
        redOverlay.color = c;
    }
}
