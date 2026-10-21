using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonFlash : MonoBehaviour
{
    public bool Canflash = false;
    public bool CanflashRed = false;
    private Image button;
    public randomNumber NumbGen;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Image>();
        NumbGen = GameObject.Find("Input").GetComponent<randomNumber>();

    }
 void Update()
    {     //checks which colour to flash
        if (Canflash == true)
        {
            StartCoroutine(Flash());
           // Debug.Log("trigger"); 
        }
        if (CanflashRed == true)
        {
            StartCoroutine(FlashRed());
           // Debug.Log("trigger");
        }
    }
    private IEnumerator Flash()// flashes green
    {
        Canflash = false ;
        NumbGen.Canpress = false;//disables button
        Color normalColor = Color.white;
        Color flashColor = Color.green;

        for (int i = 0; i < 5; i++)
        {
            // Flash the button with the new color
            button.color = flashColor;
            yield return new WaitForSeconds(1);

            // Return the button to its normal color
            button.color = normalColor;
            yield return new WaitForSeconds(1);
        }
        NumbGen.Canpress = true;
        NumbGen.CanLoad = true;// renables button
    }
    private IEnumerator FlashRed()//flashes red
    {
        CanflashRed = false;
        NumbGen.Canpress = false;//disables button
        Color normalColor = Color.white;
        Color flashColor = Color.red;

        for (int i = 0; i < 5; i++)
        {
            // Flash the button with the new color
            button.color = flashColor;
            yield return new WaitForSeconds(1);

            // Return the button to its normal color
            button.color = normalColor;
            yield return new WaitForSeconds(1);
        }
        NumbGen.Canpress = true;
        NumbGen.CanLoad = true;// renables button
    }
    // Update is called once per frame
   
}
