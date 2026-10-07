using UnityEngine;

public class twoSpawnPointHandler : MonoBehaviour
{

 public float respawnDelay = 5f;
    public CircleSpawnPointManager OuterCircle;
    public CircleSpawnPointManager InnerCircle;

    void Start()
    {
        StartCoroutine(Swaptimer());
    }

    public void SwapCircles()
    {
        // Outer shrinks
        OuterCircle.StartShrink();

        // Inner resets size
        InnerCircle.ResetShrink();

        // Swap references
        var temp = InnerCircle;
        InnerCircle = OuterCircle;
        OuterCircle = temp;

        StartCoroutine(Swaptimer());
    }

    System.Collections.IEnumerator Swaptimer()
    {
        yield return new WaitForSeconds(respawnDelay + InnerCircle.shrinkSpeed);
        SwapCircles();
    }

}
