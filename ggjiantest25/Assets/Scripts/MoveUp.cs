using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    
    [SerializeField] private float movedownspeed = 25000f;

    public float DisappearingSpeed = 0.5f;

    public SpriteRenderer spriteRenderer;

    private Rigidbody2D m_Rigidbody;
    private bool CanDestroy = true;
    private bool Disappearing = false;
    private float Alpha;
    private Color NewColor;
    private SpeedControl Speed;

    private void Start()
    {
        NewColor = spriteRenderer.color;

        Speed = GameObject.Find("GameManager").GetComponent<SpeedControl>();//find cript on manager
    }


    // Update is called once per frame

    void Update()
    {
        transform.Translate(Vector3.up * Speed.movementSpeed * Time.deltaTime);

        if (Disappearing)
        {
            Alpha = spriteRenderer.color.a;

            NewColor.a = Alpha - DisappearingSpeed * Time.deltaTime;

            spriteRenderer.color = NewColor;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanDestroy)
        {
            CanDestroy = false;
            Disappearing = true;
            StartCoroutine(countDown());
        }

    }
    IEnumerator countDown()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        // Debug.Log("out");
    }

}
