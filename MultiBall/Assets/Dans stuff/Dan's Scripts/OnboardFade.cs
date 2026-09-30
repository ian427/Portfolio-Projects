using UnityEngine;

public class OnboardFade : MonoBehaviour
{
    [SerializeField] private float countdown;
    [SerializeField] private float startTime = 5f;

    [SerializeField] private GameObject onboardOBJ;
    [SerializeField] private Animator onboardAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdown = startTime;
        onboardAnim = onboardOBJ.GetComponent<Animator>();
        onboardAnim.SetBool("Fading", false);
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        StopOnboarding();
    }

    private void StopOnboarding()
    {
        if(countdown <= 0)
        {
            onboardAnim.SetBool("Fading", true);
        }
    }
}
