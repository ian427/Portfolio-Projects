using UnityEngine;
using static UnityEngine.ParticleSystem;
using System.IO;
public enum NoiseMode
{
    Cloudy,
    HorizontalLines,
    VerticalLines,
    WavyLines,
    Ridged,
    Topographic,
    DiagonalLines
}

public class PlanetGenerator : MonoBehaviour
{

    private int size = 256;
    public float noiseScale = 4f;
    public SpriteRenderer spriteRenderer;
    public Sprite shapeSprite;
    public NoiseMode noiseMode = NoiseMode.Cloudy;
    public float lineStrength = 20f;

    private int existingSpriteCount = 0;
    string folderPath;

    void Start()
    {
        folderPath = Application.dataPath + "/Resources/GeneratedPlanets/";

        // Make sure folder exists
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Count how many PNGs are in it
        existingSpriteCount = Directory.GetFiles(folderPath, "*.png").Length;

        Debug.Log("Sprites in folder: " + existingSpriteCount);
    }

    public void NewPlanet()
    {
        Sprite planet = GeneratePlanet();
        spriteRenderer.sprite = planet;

        // Save the texture to the Editor
#if UNITY_EDITOR
        // PlanetSaverEditor.SavePlanetAsPNG(planet.texture, "Planet_" + Random.Range(1000, 9999));

        existingSpriteCount++;
        PlanetSaverEditor.SavePlanetAsPNG(planet.texture, "Planet_" + existingSpriteCount);

#endif
    }
    float GetNoise(float nx, float ny)
    {
        switch (noiseMode)
        {
            case NoiseMode.Cloudy:
                return Mathf.PerlinNoise(nx, ny);

            case NoiseMode.HorizontalLines:
                return Mathf.PerlinNoise(0, ny * noiseScale);

            case NoiseMode.VerticalLines:
                return Mathf.PerlinNoise(nx * noiseScale, 0);

            case NoiseMode.WavyLines:
                {
                    float n = Mathf.PerlinNoise(nx, ny);
                    return Mathf.Abs(Mathf.Sin(n * lineStrength));
                }

            case NoiseMode.Ridged:
                {
                    float n = Mathf.PerlinNoise(nx, ny);
                    return 1f - Mathf.Abs(n * 2f - 1f);
                }

            case NoiseMode.Topographic:
                {
                    float n = Mathf.PerlinNoise(nx, ny);
                    return Mathf.Repeat(n * lineStrength, 1f);
                }

            case NoiseMode.DiagonalLines:
                {
                    float diagonal = (nx + ny) * 0.5f;
                    return Mathf.PerlinNoise(diagonal, diagonal);
                }
        }

        return 0f;
    }
    Sprite GeneratePlanet()
    {
        Texture2D tex = new Texture2D(size, size);
        tex.filterMode = FilterMode.Point;

        // Random palette
        Color[] palette = new Color[4];
        for (int i = 0; i < palette.Length; i++)
            palette[i] = new Color(Random.value, Random.value, Random.value);

        // Pull original sprite data
        Texture2D shapeTex = shapeSprite.texture;
        Rect shapeRect = shapeSprite.textureRect;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                // ----- MAP OUTPUT PIXEL -> SPRITE UV -----

                float u = (float)x / size;
                float v = (float)y / size;

                // Convert to sprite rect coords inside its texture atlas
                float px = Mathf.Lerp(shapeRect.xMin, shapeRect.xMax, u);
                float py = Mathf.Lerp(shapeRect.yMin, shapeRect.yMax, v);

                float texU = px / shapeTex.width;
                float texV = py / shapeTex.height;

                // Sample sprite pixel (bilinear smoothing)
                Color src = shapeTex.GetPixelBilinear(texU, texV);

                // We use the sprite's alpha as the shape!
                if (src.a <= 0.01f)
                {
                    tex.SetPixel(x, y, Color.clear);
                    continue;
                }

                // ----- YOUR NOISE GENERATION -----
                float nx = (float)x / size * noiseScale;
                float ny = (float)y / size * noiseScale;
                float noise = GetNoise(nx, ny);

                Color c = Color.Lerp(palette[0], palette[1], noise);
                c = Color.Lerp(c, palette[2], noise * noise);
                c = Color.Lerp(c, palette[3], Mathf.Sqrt(noise));

                // Keep the sprite’s transparent edge softness
                c.a *= src.a;

                tex.SetPixel(x, y, c);
            }
        }

        tex.Apply();

        return Sprite.Create(
            tex,
            new Rect(0, 0, size, size),
            new Vector2(0.5f, 0.5f),
            100f
        );
    }
}
    /*
    Sprite GeneratePlanet()
    {
        Texture2D tex = new Texture2D(size, size);
        tex.filterMode = FilterMode.Point;

        // Random palette
        Color[] palette = new Color[4];
        for (int i = 0; i < palette.Length; i++)
            palette[i] = new Color(Random.value, Random.value, Random.value);

        Vector2 center = new Vector2(size / 2f, size / 2f);
        float radius = size / 2f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), center);

                if (dist > radius)
                {
                    tex.SetPixel(x, y, Color.clear);
                    continue;
                }

                float nx = (float)x / size * noiseScale;
                float ny = (float)y / size * noiseScale;
                float noise = Mathf.PerlinNoise(nx, ny);

                Color c = Color.Lerp(palette[0], palette[1], noise);
                c = Color.Lerp(c, palette[2], noise * noise);
                c = Color.Lerp(c, palette[3], Mathf.Sqrt(noise));

                tex.SetPixel(x, y, c);
            }
        }

        tex.Apply();

        return Sprite.Create(
            tex,
            new Rect(0, 0, size, size),
            new Vector2(0.5f, 0.5f),
            100f
        );
    }
}
    */