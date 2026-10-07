using UnityEngine;

public class Glow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] float fadeTime = 1f;
    [SerializeField][Range(0, 255)] float targetColour = 1f;
    [SerializeField] private int rareGlyphID;

    private float changeBy;
    private bool glyphAcquired = false;

    //SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
       // glyphAcquired = RareGlyphTracker.GetRareGlyphFound(rareGlyphID - 1);
        Debug.Log(glyphAcquired);

        targetColour = targetColour / 255;
        changeBy = targetColour / fadeTime;
    }

    private void Update()
    {
        if (glyphAcquired)
        {
            ChangeColour();
        }
    }

    private void ChangeColour()
    {
        float r = SpriteRenderer.color.r; r += changeBy * Time.deltaTime;
        float g = SpriteRenderer.color.g; g += changeBy * Time.deltaTime;
        float b = SpriteRenderer.color.b; b += changeBy * Time.deltaTime;

        float a = SpriteRenderer.color.a;

        Color newColour = new(r, g, b, a);

        SpriteRenderer.color = newColour;
    }
}
