using UnityEngine;

public class TestHeartControler : MonoBehaviour
{
    public Animator animator; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAnimation ()
    {
        animator.SetBool("HeartIsLost", true);
    }
}
