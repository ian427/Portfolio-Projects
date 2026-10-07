using UnityEngine;

public class BallColorChecker : MonoBehaviour
{
    //Color Bools
    public bool blueColor;
    public bool yellowColor;
    public bool redColor;
    public bool greenColor;

    //The bool that sees if all colours are picked
    public bool allColoursPicked;

    //The all colours picked bool is set as false at first
    private void Start()
    {
        allColoursPicked = false;
    }

    //Checks to see if all the 4 colours have been picked before setting the bool to true
    void Update()
    {
        if(blueColor == true && yellowColor == true && redColor == true && greenColor == true)
        {
            allColoursPicked = true;
        }
    }
}
