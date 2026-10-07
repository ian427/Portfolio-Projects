using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGlyphTest : MonoBehaviour
{
    public GameObject Glyph;
    // Start is called before the first frame update
    void Start()
    {
        Glyph.transform.localScale = new Vector3(1.0f, 1.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        float width = Glyph.transform.localScale.x;
        float height = Glyph.transform.localScale.y;
        if (width > 0.2f && height > 0.2f)
        { Glyph.transform.localScale -= new Vector3(0.5f, 0.5f) * Time.deltaTime; }

    }
}
