using System.Collections;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private CameraAnimTest test;
    private bool isShaking;

    private void Start()
    {
        test = cameraObject.GetComponent<CameraAnimTest>();
        isShaking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            if(isShaking == false)
            {
                isShaking = true;
                test.ShakyCam();
                StartCoroutine(resetShaking());
            }
        }
    }

    private IEnumerator resetShaking()
    {
        yield return new WaitForSeconds(0.5f);
        isShaking = false;
        test.NormalCam();
    }
}
