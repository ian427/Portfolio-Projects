using UnityEngine;
using UnityEngine.UI;

public class LoadBrightness : MonoBehaviour
{
    //Image overlay brightness and values
    [SerializeField] private Image brightnessImage;
    [SerializeField] private float loadedBrightness;
    private float maxDarkness = 0.9f;

    //When the game starts, the values from the brightness settings script are loaded here and the image's alpha is adjusted
    private void Start()
    {
        loadedBrightness = PlayerPrefs.GetFloat("ScreenBrightness");
        loadedBrightness = Mathf.Lerp(maxDarkness, 0f, loadedBrightness);
        Color color = brightnessImage.color;
        color.a = loadedBrightness;
        brightnessImage.color = color;
    }
}
