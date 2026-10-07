using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public Language CurrentLanguage { get; private set; } = Language.English;

    public delegate void LanguageChanged();
    public event LanguageChanged OnLanguageChanged;

    private Dictionary<string, Dictionary<Language, string>> translations;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadTranslations();
        LoadSavedLanguage();
    }

    void LoadSavedLanguage()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            CurrentLanguage = (Language)PlayerPrefs.GetInt("Language");
        }
        else
        {
            CurrentLanguage = Language.English;
        }
    }

    void LoadTranslations()
    {
        translations = new Dictionary<string, Dictionary<Language, string>>
        {
            {
                "MULTIBALL", new Dictionary<Language, string>
                {
                    { Language.English, "Multiball" },
                    { Language.German, "Mehrball" },
                    { Language.Spanish, "Multibola" },
                    { Language.French, "Multiballe" }
                }
            },
            {
                "KEEP_BALL_UP", new Dictionary<Language, string>
                {
                    { Language.English, "Keep the ball up" },
                    { Language.German, "Halte den Ball oben" },
                    { Language.Spanish, "Mantén la pelota en el aire" },
                    { Language.French, "Garde la balle en l’air" }
                }
            },
            {
                "DONT_DROP", new Dictionary<Language, string>
                {
                    { Language.English, "Don’t let them drop" },
                    { Language.German, "Lass sie nicht fallen" },
                    { Language.Spanish, "No dejes que caigan" },
                    { Language.French, "Ne les laisse pas tomber" }
                }
            },
            {
                "SETTINGS", new Dictionary<Language, string>
                {
                    { Language.English, "Settings" },
                    { Language.German, "Einstellungen" },
                    { Language.Spanish, "Ajustes" },
                    { Language.French, "Paramètres" }
                }
            },
            {
                "LANGUAGE", new Dictionary<Language, string>
                {
                    { Language.English, "Language" },
                    { Language.German, "Sprache" },
                    { Language.Spanish, "Idioma" },
                    { Language.French, "Langue" }
                }
            },
            {
                "BALL_SKINS", new Dictionary<Language, string>
                {
                    { Language.English, "Ball skins" },
                    { Language.German, "Ball-Skins" },
                    { Language.Spanish, "Skins de pelota" },
                    { Language.French, "Skins de balle" }
                }
            }
        };
    }

    public string GetText(string key)
    {
        if (translations.ContainsKey(key))
            return translations[key][CurrentLanguage];

        return key;
    }

    public void SetLanguage(Language newLanguage)
    {
        CurrentLanguage = newLanguage;
        PlayerPrefs.SetInt("Language", (int)newLanguage);
        PlayerPrefs.Save();

        OnLanguageChanged?.Invoke();
    }
}
