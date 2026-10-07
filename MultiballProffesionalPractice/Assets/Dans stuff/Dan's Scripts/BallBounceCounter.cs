using System.Collections;
using UnityEngine;
using TMPro;

public class BallBounceCounter : MonoBehaviour
{
    //Variables for the ball counting when it hits the platform
    private float ballCountMax;
    [SerializeField] private float ballCount;

    //Variables relating to when the ball reaches 5 and displays a random message
    [SerializeField] private float randomNumber;
    [SerializeField] private GameObject rewardObject;
    [SerializeField] private TMP_Text rewardText;

    //Starts by setting the max as 5 and disabling the reward text
    void Start()
    {
        ballCountMax = 5f;
        ballCount = 0f;
        rewardObject.SetActive(false);
    }

    //Whenever the platform collides with a ball, the count is increased by 1 and then it checks to see if the count is 5
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            ballCount += 1;
            CountChecker();
        }
    }

    //The random number is set as either 1, 2, 3, and 4
    //Depending what the number is, the text will display one of these options
    //The count then resets
    private void CountChecker()
    {
        if(ballCount == 5)
        {
            randomNumber = Random.Range(1, 5);

            if(randomNumber == 1)
            {
                rewardText.text = "Great job!";
            }

            if (randomNumber == 2)
            {
                rewardText.text = "You rock!";
            }

            if (randomNumber == 3)
            {
                rewardText.text = "Nice one!";
            }

            if (randomNumber == 4)
            {
                rewardText.text = "Well done!";
            }

            rewardObject.SetActive(true);
            StartCoroutine(turnTextOff());
        }

        if(ballCount > ballCountMax)
        {
            ballCount = 1;
        }
    }

    //Simple countdown to turn the reward text back off
    private IEnumerator turnTextOff()
    {
        yield return new WaitForSeconds(0.7f);
        rewardObject.SetActive(false);
    }
}
