using UnityEngine;

public class LanguageButtons : MonoBehaviour
{
    public void English()
    {
        LanguageManager.Instance.SetLanguage(Language.English);
    }

    public void German()
    {
        LanguageManager.Instance.SetLanguage(Language.German);
    }

    public void Spanish()
    {
        LanguageManager.Instance.SetLanguage(Language.Spanish);
    }

    public void French()
    {
        LanguageManager.Instance.SetLanguage(Language.French);
    }
}
