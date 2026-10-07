using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "Scores", menuName = "Scriptable Objects/Scores")]
public class Scores : ScriptableObject
{
    public int Curentscore = 0;
    public int HighScore = 0;
   // SecureSaverSerilizer Secure;
    void Start()
    {
        

    }

}
