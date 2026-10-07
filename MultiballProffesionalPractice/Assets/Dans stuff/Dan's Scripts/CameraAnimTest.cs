using System.Collections;
using UnityEngine;

public class CameraAnimTest : MonoBehaviour
{
    //Animator and reset time float
    private Animator anim;
    private float resetTime;

    //Starts by getting the animator and setting the reset time
    void Start()
    {
        anim = GetComponent<Animator>();
        resetTime = 0.5f;
    }

    //The camera as it is normally, stationary
    public void NormalCam()
    {
        anim.SetBool("BallHitWall", false);
        anim.SetBool("BallHitMetal", false);
    }

    //The camera when a ball hits a side wall, it shakes a little
    public void ShakyCam()
    {
        anim.SetBool("BallHitWall", true);
        //StartCoroutine(ResetCam());
    }

    public void SuperShakyCam()
    {
        anim.SetBool("BallHitMetal", true);
    }

    //Simple IEnumerator that counts down until the shaking stops
    public IEnumerator ResetCam()
    {
        yield return new WaitForSeconds(resetTime);
        NormalCam();
    }
}
