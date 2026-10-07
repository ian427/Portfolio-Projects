using UnityEngine;

public class SlowMoTrigger : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private SlowmoZoom slowmo;
    public int count = 0;
   private void Start()
   {
        slowmo = camera.GetComponent
        <SlowmoZoom>();
        
   }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.CompareTag("Ball"))
        {
             count++;
             if (count == 3)
             { 
                slowmo.ToggleBulletTime(obj.transform);
             } // Do something if this object has the "Player" tag
        }

       
    }
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Ball"))
        {
           
            if (count == 3)
            {
                slowmo.ToggleBulletTime(obj.transform);
            } // Do something if this object has the "Player" tag
        }

    }
}
