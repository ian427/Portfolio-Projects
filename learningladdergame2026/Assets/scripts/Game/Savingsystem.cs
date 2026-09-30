using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;

public class Savingsystem : MonoBehaviour
{
   
        public List<LeaderBoardEntry> Leaderboard = new List<LeaderBoardEntry>();

        [System.Serializable]
        public class LeaderBoardEntry
        {
            public int score =0;
            public string name = "Enter Name";
        }

        [System.Serializable]
        private class LeaderboardData
        {
            public List<LeaderBoardEntry> entries = new List<LeaderBoardEntry>();
        }

      
    private string GenerateChecksum(string data)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = sha.ComputeHash(bytes);
            return System.BitConverter.ToString(hash).Replace("-", "");
        }
    }

    private string Encode(string plain)
    {
        return System.Convert.ToBase64String(
            Encoding.UTF8.GetBytes(plain)
        );
    }

    private string Decode(string encoded)
    {
        byte[] bytes = System.Convert.FromBase64String(encoded);
        return Encoding.UTF8.GetString(bytes);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
       LoadLeaderboard();

    }
    public void SetLeaderboard(List<LeaderBoardEntry> data)
    {
        Leaderboard = new List<LeaderBoardEntry>(data);
    }
    public List<LeaderBoardEntry> GetLeaderboard()
    {
        return Leaderboard;
    }
    public void SaveLeaderboard()
    {
        LeaderboardData data = new LeaderboardData
        {
            entries = Leaderboard
        };

        string json = JsonUtility.ToJson(data);
        string encoded = Encode(json);
        string checksum = GenerateChecksum(json);

        PlayerPrefs.SetString("Leaderboard", encoded);
        PlayerPrefs.SetString("Leaderboard_chk", checksum);

        PlayerPrefs.Save();
    }
    public void LoadLeaderboard()
    {
        if (!PlayerPrefs.HasKey("Leaderboard"))
        {
            Leaderboard = new List<LeaderBoardEntry>();
        }
        else
        {
            string encoded = PlayerPrefs.GetString("Leaderboard");
            string savedChecksum = PlayerPrefs.GetString("Leaderboard_chk", "");

            string json = Decode(encoded);

            if (GenerateChecksum(json) != savedChecksum)
            {
                Debug.LogWarning("Leaderboard data tampered with.");
                Leaderboard = new List<LeaderBoardEntry>();
            }
            else
            {
                LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
                Leaderboard = data.entries ?? new List<LeaderBoardEntry>();
            }
        }

        // Ensure 10 entries exist
        while (Leaderboard.Count < 10)
        {
            Leaderboard.Add(new LeaderBoardEntry
            {
                name = "Enter Name",
                score = 0
            });
        }
    }

}
