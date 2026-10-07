#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public static class PlanetSaverEditor
{
    public static void SavePlanetAsPNG(Texture2D tex, string fileName)
    {
        string folderPath = "Assets/Resources/GeneratedPlanets";

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string path = $"{folderPath}/{fileName}.png";

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(path, bytes);

        AssetDatabase.Refresh();

        Debug.Log("Saved planet to: " + path);
    }
}
#endif
