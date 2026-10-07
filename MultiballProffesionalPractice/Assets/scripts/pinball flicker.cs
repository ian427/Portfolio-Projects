using UnityEngine;

public class PinballFlicker : MonoBehaviour
{
    public float minDelay = 1f;
    public float maxDelay = 3f;

    public float flickSpeed = 800f;
    public float returnSpeed = 200f;

    private float targetRotation = 0f;
    private bool CanFlick = true;

    void Start()
    {
       // ScheduleNextFlick();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        CanFlick = true;
    }
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0, 0, targetRotation),
            (targetRotation != 0 ? flickSpeed : returnSpeed) * Time.deltaTime
        );

        if (CanFlick)
        {
            CanFlick = false;
            targetRotation = Random.Range(40f, 100f);
            StartCoroutine(ReturnAfterFlick());
            
        }
        if(transform.rotation.eulerAngles == Vector3.zero)
        {
            //CanFlick=true;
        }
    }
    System.Collections.IEnumerator ReturnAfterFlick()
    {
        yield return new WaitForSeconds(0.1f);
        targetRotation = 0;
     
    }
}
