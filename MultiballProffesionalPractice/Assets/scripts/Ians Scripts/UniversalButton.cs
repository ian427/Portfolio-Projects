using UnityEngine;
using UnityEngine.SceneManagement; 

public class UniversalButton : MonoBehaviour
{

    public AudioSource click;
    private bool canSwitch = false;
    [SerializeField]
    private string SceneToGoTO;
    public void OnButtonPress()
    {
        SceneManager.LoadScene(SceneToGoTO);//remove for sound
        //click.Play();

        //canSwitch = true;

       // Debug.Log("click");
    }
    /*
    private void Update()
    {

        if (!click.isPlaying && canSwitch)//defults true
        {
            Debug.Log("switch");
            SceneManager.LoadScene(SceneToGoTO);//remember to add scene to Build
        }
    }
    */
}