using UnityEngine;

public class SkipButton : MonoBehaviour
{
    //The countdown and intro scripts that are to be bypassed
    [SerializeField] private StartingCountdown sc;
    [SerializeField] private Introduction intro;

    //The skipped bool
    private bool skipped = false;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SkipIntroSequence();
        }
    }

    //When called, the value is set to true to prevent repetition. The skipIntros function is called to bypass the text displays and the 3 2 1 begins
    public void SkipIntroSequence()
    {
        if(skipped == false)
        {
            skipped = true;
            intro.SkipIntros();
           
        }
    }
}
