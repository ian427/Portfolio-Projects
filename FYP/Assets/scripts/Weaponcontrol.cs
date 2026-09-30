using System;
using System.Collections;
using UnityEngine;


public class Weaponcontrol : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float FireRate;
    [SerializeField] private int burst;//shots ton fire
    [SerializeField] private int bulletsPerShot;//bullets per shot
    [SerializeField] private float bulletrange;// s=d/t
    [SerializeField] private float offset;// s=d/t
    [SerializeField] private int bulletspeed;
    [SerializeField] private float inacuracy;
    private bool Cycle = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Attack()
    { 
        Fire();
    }
    private IEnumerator Fire()
    {
        for (int i = 0; i < burst; i++)
        {
            yield return new WaitForSeconds(FireRate);
            for(int s = 0;s < bulletsPerShot; s++)
            {

                float randomYaw = UnityEngine.Random.Range(-inacuracy, inacuracy);
                float randomPitch = UnityEngine.Random.Range(-inacuracy, inacuracy);

                Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0);
                Quaternion finalRotation = transform.rotation * spreadRotation;

                GameObject Temp = Instantiate(bullet, transform.position + transform.forward * offset, finalRotation);
                Temp.GetComponent<BulletControl>().bulletspeed = bulletspeed;
                Temp.GetComponent<BulletControl>().bulletrange = bulletrange;

            }
           
        }
        
    }
}
