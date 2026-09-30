using System;
using UnityEngine;
using UnityEngine.UIElements;
using static Spawnpointhandler;

public class ObsticalHandler : MonoBehaviour
{
    public Spawnpointhandler handler;
    public Sprite Defaultsprite;
    public Sprite Desertsprite;
    public Sprite Arcticsprite;
    public Sprite Junglesprite;
    public Sprite Volcanosprite;
    public SpriteRenderer sr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //this.gameObject.SetActive(true);
       handler = GameObject.Find("Manager").GetComponent<Spawnpointhandler>();

         sr = GetComponent<SpriteRenderer>();
        Sprite current = sr.sprite;
        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    public void UpdateSprite()
    {
        int temp = handler.GetBackground();
        switch (temp)
        {
            case (int)CurrentBackground.Desert:
                sr.sprite = Desertsprite;
                break;

            case (int)CurrentBackground.Arctic:
                sr.sprite = Arcticsprite;
                break;

            case (int)CurrentBackground.Jungle:
                sr.sprite = Junglesprite;
                break;
            case (int)CurrentBackground.Volcano:
                sr.sprite = Volcanosprite;
                break;

            default:
                sr.sprite = Defaultsprite;
                break;
        }
    }
}
