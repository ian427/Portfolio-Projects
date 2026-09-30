using System;
using System.Collections;
using UnityEngine;
public class BulletControl : MonoBehaviour
{
    public float bulletrange;// s=d/t
    public int bulletspeed;
    private float lifetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifetime = bulletrange/bulletspeed;
        Cull();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletspeed * Time.deltaTime;
    }
    private IEnumerator Cull()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Destroy(this.gameObject); 
        }

    }
}
