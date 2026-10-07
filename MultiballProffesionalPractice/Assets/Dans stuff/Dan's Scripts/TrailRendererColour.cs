using UnityEngine;

public class TrailRendererColour : MonoBehaviour
{
    //The trail renderer and colour components
    private TrailRenderer tr;
    private float randomNumber;
    private string colorName;

    //The gameObject and script to check ball colours
    private GameObject colorCheckOBJ;
    private BallColorChecker bcc;

    //
    void Start()
    {
        colorCheckOBJ = GameObject.Find("Manager");
        bcc = colorCheckOBJ.GetComponent<BallColorChecker>();

        tr = GetComponent<TrailRenderer>();
        SetStartColour();
    }

    //Randomizes the number between 1 and 5 and sets the trail renderer's colour depending on that number
    //The bcc has the colour bool set to true as to prevent 2 trail renderers of the same colour
    private void SetStartColour()
    {
        for (int i = 0; i < 4; i++)
        {
            randomNumber = Random.Range(1, 5);

            if(randomNumber == 1 && bcc.blueColor == false)
            {
                tr.startColor = Color.blue;
                colorName = "Blue";
                bcc.blueColor = true;
                return;
            }

            if (randomNumber == 2 && bcc.yellowColor == false)
            {
                tr.startColor = Color.yellow;
                colorName = "Yellow";
                bcc.yellowColor = true;
                return;
            }

            if (randomNumber == 3 && bcc.redColor == false)
            {
                tr.startColor = Color.red;
                colorName = "Red";
                bcc.redColor = true;
                return;
            }

            if (randomNumber == 4 && bcc.greenColor == false)
            {
                tr.startColor = Color.green;
                colorName = "Green";
                bcc.greenColor = true;
                return;
            }
        }

        bcc.allColoursPicked = true;
        ReturnData();

    }

    //A spare function to be called if all colours have been assigned and to prevent crashing
    private void ReturnData()
    {
        if(bcc.allColoursPicked == true)
        {
            Debug.Log("No Colours Left");
        }
    }

    //The use of AI (ChatGPT) has been used to help in the adjustments of the SetStartColour to prevent the game from breaking
}
