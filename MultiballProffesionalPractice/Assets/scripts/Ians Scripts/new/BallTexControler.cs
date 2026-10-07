using UnityEngine;

public class BallTexControler : MonoBehaviour
{
    TexturesList tex;
    DataSerilizer Data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tex = GameObject.Find("DataHolder").GetComponent<TexturesList>();
        Data = GameObject.Find("DataHolder").GetComponent<DataSerilizer>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = tex.Textures[Data.CurrentTextureID].sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
