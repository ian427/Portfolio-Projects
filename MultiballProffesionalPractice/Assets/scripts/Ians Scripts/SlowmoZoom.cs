using UnityEngine;
using System.Collections;
public class SlowmoZoom : MonoBehaviour
{ 
    [Header("Slow Motion Settings")]
    [Range(0.01f, 1f)] public float slowFactor = 0.2f;
    public float slowInDuration = 0.3f;
    public float slowOutDuration = 0.3f;

    [Header("Camera Zoom Settings")]
    public Camera cam;                   // Assign your orthographic camera
    public Transform focusTarget;        // The spot or object to zoom towards
    public Vector3 focusOffset = new Vector3(0, 0, 0); // Optional offset
    public float zoomSize = 3f;          // Orthographic size when zoomed-in
    public float normalSize = 5f;        // Default orthographic size
    public float zoomDuration = 0.35f;   // How fast to zoom/move
    public float moveDistance = 4f;      // How close the camera moves toward target
    
    private Vector3 originalCamPos;
    bool isBulletTime;
    float defaultFixedDeltaTime;
    Coroutine routine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Awake()
    {
        originalCamPos = cam.transform.position;
        if (cam == null) cam = Camera.main;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        if (!cam.orthographic)
            Debug.LogWarning("Camera is not orthographic! Switch projection mode.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleBulletTime(Transform target)
    {
        Time.timeScale = 0;
         focusTarget = target;
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(isBulletTime ? ExitBulletTime() : EnterBulletTime());
    }

    IEnumerator EnterBulletTime()
    {
        isBulletTime = true;

        // Smoothly change timeScale and fixedDeltaTime
        yield return LerpTimeScale(1f, slowFactor, slowInDuration);

        // Camera: compute target position
        Vector3 targetPos = focusTarget ? focusTarget.position + focusOffset : cam.transform.position;
        Vector3 desiredCamPos = new Vector3(targetPos.x, targetPos.y, cam.transform.position.z);

        // Smoothly move & zoom
        yield return LerpCamera(cam.transform.position, desiredCamPos, cam.orthographicSize, zoomSize, zoomDuration);
    }


    IEnumerator ExitBulletTime()
    {
        // Restore camera position and zoom
        yield return LerpCamera(cam.transform.position, originalCamPos, cam.orthographicSize, normalSize, zoomDuration);

        // Restore time
        yield return LerpTimeScale(Time.timeScale, 1f, slowOutDuration);

        Time.fixedDeltaTime = defaultFixedDeltaTime;
        isBulletTime = false;
    }


    IEnumerator LerpTimeScale(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime; // Use unscaled time for consistent interpolation
            float k = Mathf.Clamp01(t / duration);
            Time.timeScale = Mathf.Lerp(from, to, k);
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
            yield return null;
        }
        Time.timeScale = to;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
    }

    IEnumerator LerpCamera(Vector3 posFrom, Vector3 posTo, float sizeFrom, float sizeTo, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float k = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / duration));

            cam.transform.position = Vector3.Lerp(posFrom, posTo, k);
            cam.orthographicSize = Mathf.Lerp(sizeFrom, sizeTo, k);

            yield return null;
        }
        cam.transform.position = posTo;
        cam.orthographicSize = sizeTo;
    }
}
