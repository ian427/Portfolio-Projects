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
       //handler = GameObject.Find("Manager").GetComponent<Spawnpointhandler>();

         sr = GetComponent<SpriteRenderer>();
        Sprite current = sr.sprite;
        //this.gameObject.SetActive(false);

    }

    // Update is called once per frame
   
}
