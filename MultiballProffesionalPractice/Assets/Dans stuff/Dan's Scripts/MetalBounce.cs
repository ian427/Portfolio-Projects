using System.Collections;
using UnityEngine;

public class MetalBounce : MonoBehaviour
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private CameraAnimTest test;
    private bool isShaking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraObject = GameObject.Find("Main Camera");
        test = cameraObject.GetComponent<CameraAnimTest>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (isShaking == false)
            {
                isShaking = true;
                test.SuperShakyCam();
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
