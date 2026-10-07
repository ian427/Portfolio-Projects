using UnityEngine;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour
{
    //The brightness image and slider
    [SerializeField] private Image brightnessImage;
    [SerializeField] private Slider brightnessSlider;

    //Values to do with the brightness alterations
    [Range(0f, 1f)]
    [SerializeField] private float maxDarkness = 0.9f;
    private float currentBrightness;
    private float savedBrightness;

    //Starts by loading the saved value and setting the slider in the same position
    private void Start()
    {
        savedBrightness = PlayerPrefs.GetFloat("ScreenBrightness", 0f);
        brightnessSlider.value = savedBrightness;
        SetBrightness(savedBrightness);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    //Void is called with the assigned slider
    //The alpha of the overlay image is changed and the value is saved each time the void is called
    public void SetBrightness(float value)
    {
        PlayerPrefs.SetFloat("ScreenBrightness", value);
        currentBrightness = Mathf.Lerp(maxDarkness, 0f, value);
        Color color = brightnessImage.color;
        color.a = currentBrightness;
        brightnessImage.color = color;
    }

    //The use of AI (ChatGPT) has been featured to help with development of this code
}
