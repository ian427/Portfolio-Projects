using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string translationKey;
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        LanguageManager.Instance.OnLanguageChanged += UpdateText;
        UpdateText();
    }

    void OnDisable()
    {
        LanguageManager.Instance.OnLanguageChanged -= UpdateText;
    }

    void UpdateText()
    {
        text.text = LanguageManager.Instance.GetText(translationKey);
    }
}
