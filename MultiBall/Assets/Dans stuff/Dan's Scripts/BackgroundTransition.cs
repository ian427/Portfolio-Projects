using UnityEngine;
using System.Collections;

public class BackgroundTransition : MonoBehaviour
{
    public GameObject defaultBG;
    public GameObject desertBG;
    public GameObject arcticBG;
    public GameObject jungleBG;
    public GameObject volcanoBG;
    [SerializeField] private GameObject Corner;
    [SerializeField] private GameObject Corner1;
    [SerializeField] private Animator anim;
    [SerializeField] private int number;
    private string name;
    [SerializeField]
    private Spawnpointhandler Spawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultBG.SetActive(true);
        desertBG.SetActive(false);
        arcticBG.SetActive(false);
        jungleBG.SetActive(false);
        volcanoBG.SetActive(false);
        Spawn = GameObject.Find("Manager").GetComponent<Spawnpointhandler>();
        anim.SetBool("Switching", false);
        name = "Default";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomNumber()
    {
        number = Random.Range(1, 5);
    }

    public void SwitchBackground()
    {
        anim.SetBool("Switching", true);
        RandomNumber();
        Spawn.UpdateBackground(number);
        if(number == 1)
        {
            if(name != "Desert")//check what previous background was
            {
                defaultBG.SetActive(false);
                desertBG.SetActive(true);
                arcticBG.SetActive(false);
                jungleBG.SetActive(false);
                volcanoBG.SetActive(false);
                Corner1.GetComponent<ChangeCorner>().SetColorDesert();
                Corner.GetComponent<ChangeCorner>().SetColorDesert();
                name = "Desert";
            }

            else
            {
                SwitchBackground();
            }
            
        }

        if(number == 2)
        {
            if (name != "Arctic")
            {
                defaultBG.SetActive(false);
                desertBG.SetActive(false);
                arcticBG.SetActive(true);
                jungleBG.SetActive(false);
                volcanoBG.SetActive(false);
                Corner1.GetComponent<ChangeCorner>().SetColorArctic();
                Corner.GetComponent<ChangeCorner>().SetColorArctic();
                name = "Arctic";
            }

            else
            {
                SwitchBackground();
            }

        }

        if (number == 3)
        {
            if (name != "Jungle")
            {
                defaultBG.SetActive(false);
                desertBG.SetActive(false);
                arcticBG.SetActive(false);
                jungleBG.SetActive(true);
                volcanoBG.SetActive(false);
                Corner1.GetComponent<ChangeCorner>().SetColorJungle();
                Corner.GetComponent<ChangeCorner>().SetColorJungle();
                name = "Jungle";
            }

            else
            {
                SwitchBackground();
            }
        }

        if(number == 4)
        {
            if (name != "Volcano")
            {
                defaultBG.SetActive(false);
                desertBG.SetActive(false);
                arcticBG.SetActive(false);
                jungleBG.SetActive(false);
                volcanoBG.SetActive(true);
                Corner1.GetComponent<ChangeCorner>().SetColorVolcano();
                Corner.GetComponent<ChangeCorner>().SetColorVolcano();
                name = "Volcano";
            }

            else
            {
                SwitchBackground();
            }
        }

        StartCoroutine(resetBool());
    }

    private IEnumerator resetBool()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Switching", false);
    }
    
}
