using UnityEngine;

public class OnboardFade : MonoBehaviour
{
    //A countdown and starting time value
    [SerializeField] private float countdown;
    [SerializeField] private float startTime = 5f;

    //The gameObject and animator
    [SerializeField] private GameObject onboardOBJ;
    [SerializeField] private Animator onboardAnim;

    //Starts by setting the countdown time as the start time and plays the right animation
    void Start()
    {
        countdown = startTime;
        onboardAnim = onboardOBJ.GetComponent<Animator>();
        onboardAnim.SetBool("Fading", false);
    }

    //Counts down from the startTime while calling for StopOnboarding
    void Update()
    {
        countdown -= Time.deltaTime;
        StopOnboarding();
    }

    //If the time is 0 or less, then the onboarding animation and gameObject will stop and disappear
    private void StopOnboarding()
    {
        if(countdown <= 0)
        {
            onboardAnim.SetBool("Fading", true);
        }
    }
}
