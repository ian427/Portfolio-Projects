using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PadDetector : MonoBehaviour
{
    //private bool OverFlowed = false;
    private SwipDirectionTracker Track;
    [SerializeField] private string Pad;

	// Start is called before the first frame update
	void Start()
    {
        Track = GameObject.Find("MainGameController").GetComponent<SwipDirectionTracker>();

    }
    void Update()
    {

    }

    public void OnTriggerEnter2D()
    {
        // Debug.Log("In");
        switch (Pad)
        {
            case "RightPad":
                Track.RightPadIn = true;
                break;
            case "LeftPad":
                Track.LeftPadIn = true;
                break;
            case "TopPad":
                Track.TopPadIn = true;
                break;
            case "BottomPad":
                Track.BottomPadIn = true;
                break;
        }
    }

    public void OnTriggerExit2D()
    {

        switch (Pad)
        {
            case "StartPad":

                Track.PadOut = true;
                StartCoroutine( nameof( Out ) );
                break;

        }
        //Debug.Log("Out");
    }
    private IEnumerator Out()
    {
        yield return new WaitForSeconds(1.0f);

        Track.RightPadIn = false;
        Track.LeftPadIn = false;
        Track.TopPadIn = false;
        Track.BottomPadIn = false;

        Track.PadOut = false;
    }
}

