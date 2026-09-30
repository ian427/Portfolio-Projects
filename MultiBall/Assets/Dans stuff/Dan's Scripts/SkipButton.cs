using UnityEngine;

public class SkipButton : MonoBehaviour
{
    [SerializeField] private StartingCountdown sc;
    [SerializeField] private Introduction intro;
    private bool skipped = false;

    public void SkipIntroSequence()
    {
        if(skipped == false)
        {
            skipped = true;
            intro.SkipIntros();
            sc.StartCountdown();
           
        }
    }
}
