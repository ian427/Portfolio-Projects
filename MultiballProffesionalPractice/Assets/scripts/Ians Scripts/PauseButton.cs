using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private bool ispaused = false;
   
    [SerializeField]private GameObject canvas;
   public void Pause( )
   {
        if ( ispaused )
        {
            Time.timeScale = 1;
            ispaused = false;
            canvas.SetActive( false );
        }
        else
        {
            Time.timeScale = 0;
            ispaused = true;
            canvas.SetActive( true );   
        }
        

   }
}
