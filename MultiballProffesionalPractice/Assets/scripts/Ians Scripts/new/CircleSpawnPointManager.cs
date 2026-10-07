
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawnPointManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private List<Obsticalspawner> obspawn = new List<Obsticalspawner>();

    private bool CanSpawn = true;

    [SerializeField] float StartSize = 22f;
    [SerializeField] float TargetSize = 11f;   // final scale (uniform)
    [SerializeField] private float duration = 0.75f;
    public float shrinkSpeed = 2f;

    public bool CanShrink = false;

    private Vector3 startScale;
    private Vector3 targetScale;
    private float t = 0f;

    void Start()
    {
        foreach (var sp in SpawnPoints)
            obspawn.Add(sp.GetComponent<Obsticalspawner>());

        Restart();
    }

    private void OnEnable()
    {
        // Set the start scale based on whatever the current scale is
        startScale = transform.localScale;

        // Set target scale (uniform)
        targetScale = new Vector3(TargetSize, TargetSize, transform.localScale.z);

        // Make sure we only shrink (never grow)
        float current = startScale.x;
        float final = Mathf.Min(TargetSize, current);
        targetScale = new Vector3(final, final, transform.localScale.z);

        t = 0f;
    }

    private void Update()
    {
        if (!CanShrink) return;       // ← ONLY shrink when requested
        if (t >= 1f) return;

        t += Time.deltaTime / duration;
        if (t > 1f) t = 1f;

        float eased = t * t * (3f - 2f * t);
        transform.localScale = Vector3.Lerp(startScale, targetScale, eased);
    }

    private void Restart()
    {
        if (CanSpawn)
            StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        foreach (var s in obspawn)
            s.SpawnObstacle();

        yield break;
    }

    public void ResetShrink()
    {
        CanShrink = false;
        transform.localScale = new Vector3(StartSize, StartSize, transform.localScale.z);

        StartCoroutine(Spawn());
    }

    public void StartShrink()
    {
        CanShrink = true;
        t = 0f; // restart animation
        startScale = transform.localScale;
    }
}

