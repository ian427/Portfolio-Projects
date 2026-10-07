using UnityEngine;

public class effectspawner : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D col)
    {

        GameObject temp;
        temp = Instantiate(effect);
        temp.transform.position = this.transform.position;
    }

}
