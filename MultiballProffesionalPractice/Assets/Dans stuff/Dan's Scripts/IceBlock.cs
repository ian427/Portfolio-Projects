using UnityEngine;

public class IceBlock : MonoBehaviour
{
    [SerializeField] private Sprite[] iceStages;
    //private Sprite newSprite;
    [SerializeField] private float hitCount;
    [SerializeField] private GameObject iceShatterParticles;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitCount = 1;
        sr = gameObject.GetComponent<SpriteRenderer>() ;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(hitCount == 1)
        {
            newSprite = iceStages[0];
            //gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }

        if (hitCount == 2)
        {
            newSprite = iceStages[1];
           // gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }

        if (hitCount == 3)
        {
            newSprite = iceStages[2];
            // gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }

        if (hitCount > 3)
        {
            Instantiate(iceShatterParticles, this.transform.position, this.transform.rotation);
            this.gameObject.SetActive(false);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            hitCount += 1;
        }
        if (hitCount == 1)
        {
            //newSprite = iceStages[0];
            sr.sprite = iceStages[0];
            // gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }

        if (hitCount == 2)
        {
           // newSprite = iceStages[1];
            // gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            sr.sprite = iceStages[1];
        }

        if (hitCount == 3)
        {
            //newSprite = iceStages[2];
             //gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            sr.sprite = iceStages[2];
        }

        if (hitCount > 3)
        {
            Instantiate(iceShatterParticles, this.transform.position, this.transform.rotation);
            this.gameObject.SetActive(false);
        }
    }
}
