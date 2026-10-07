using System.Collections;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyParticles());
    }

    private IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
