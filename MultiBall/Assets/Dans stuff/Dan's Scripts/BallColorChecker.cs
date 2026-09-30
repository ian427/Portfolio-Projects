using UnityEngine;

public class BallColorChecker : MonoBehaviour
{
    //Color Bools
    public bool blueColor;
    public bool yellowColor;
    public bool redColor;
    public bool greenColor;

    public bool allColoursPicked;

    private void Start()
    {
        allColoursPicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(blueColor == true && yellowColor == true && redColor == true && greenColor == true)
        {
            allColoursPicked = true;
        }
    }
}
