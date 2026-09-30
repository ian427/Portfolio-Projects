using UnityEngine;

public class TrailRendererColour : MonoBehaviour
{
    private TrailRenderer tr;
    private float randomNumber;
    private string colorName;

    private GameObject colorCheckOBJ;
    private BallColorChecker bcc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorCheckOBJ = GameObject.Find("Manager");
        bcc = colorCheckOBJ.GetComponent<BallColorChecker>();

        tr = GetComponent<TrailRenderer>();
        SetStartColour();
    }

    // Update is called once per frame
    
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

    private void ReturnData()
    {
        if(bcc.allColoursPicked == true)
        {
            Debug.Log("No Colours Left");
        }
    }
}
