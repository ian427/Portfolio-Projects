using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : TriggerStartButton
{
    public TriggerStartButton tsb;
    

    public void OnButtonPress()
    {
        
        StartCoroutine(Pressed());
        tsb.setButtonPressed(true);
        Debug.Log("click");
    }
    private IEnumerator Pressed()
    {
        Debug.Log("ipress");
       
        
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainScene");
    }

}
