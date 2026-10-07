using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
public class SecureSaverSerilizer : MonoBehaviour
{

    public int HighTime = 0;
    public int GCrystal = 0, RCrystal = 0, CCrystal = 0, GoldCrystal = 0;
    public List<Unlockdata> Unlocked = new List<Unlockdata>();
    [System.Serializable]
    public class Unlockdata
    {
        public int TextureID;
        public bool unlocked = false;
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
        LoadHighTime();
        //load alltextures

    }

    public void LoadHighTime()
    {

        string encoded = PlayerPrefs.GetString("HighScore", "");
        string checksumSaved = PlayerPrefs.GetString("HighScore_chk", "");

        // 2) Decode to RAW text (safe-guard for malformed data)
        string decoded;
        try
        {
            decoded = Decode(encoded);
        }
        catch
        {
            // Corrupted Base64 → treat as tampered and reset
            Debug.LogWarning("HighScore decode failed. Resetting to default.");
            HighTime = 0;
            return;
        }

        // 3) If there is a saved checksum, verify it
        if (!string.IsNullOrEmpty(checksumSaved))
        {
            string checksumComputed = GenerateChecksum(decoded);

            if (!checksumSaved.Equals(checksumComputed))
            {
                // Tamper detected → reset to safe default
                Debug.LogWarning("HighScore checksum mismatch. Tamper detected; resetting to default.");
                HighTime = 0;
                return;
            }
        }

        if (int.TryParse(decoded, out int value))
            HighTime = value;
        else
            HighTime = 0; // bad data → default

    }

    public void SaveHighestime()
    {
        string raw = HighTime.ToString();

        // 2) Compute checksum over the RAW value
        string checksum = GenerateChecksum(raw);

        // 3) Encode (Base64) the RAW value for obfuscation
        string encoded = Encode(raw);

        // 4) Save both encoded value and checksum
        PlayerPrefs.SetString("HighScore", encoded);
        PlayerPrefs.SetString("HighScore_chk", checksum);

        PlayerPrefs.Save();

    }

    public void SetHighTime(int time)
    {
        HighTime = time;
        SaveHighestime();
    }

    public int GetHighTime()
    {
        return HighTime;
    }
    public void LoadCurrency()
    {
        GCrystal = LoadOneCurrency("GCrystal");
        RCrystal = LoadOneCurrency("RCrystal");
        CCrystal = LoadOneCurrency("CCrystal");
        GoldCrystal = LoadOneCurrency("GoldCrystal");
    }

    private int LoadOneCurrency(string key)
    {
        string encoded = PlayerPrefs.GetString(key, "");
        string checksumSaved = PlayerPrefs.GetString(key + "_chk", "");

        string decoded;

        // Decode Base64 safely
        try
        {
            decoded = Decode(encoded);
        }
        catch
        {
            Debug.LogWarning($"{key} decode failed. Reset to 0.");
            return 0;
        }

        // Validate checksum
        if (!string.IsNullOrEmpty(checksumSaved))
        {
            string checksumComputed = GenerateChecksum(decoded);

            if (!checksumSaved.Equals(checksumComputed))
            {
                Debug.LogWarning($"{key} checksum mismatch. Resetting to 0.");
                return 0;
            }
        }

        // Parse integer
        if (int.TryParse(decoded, out int value))
            return value;

        Debug.LogWarning($"{key} stored value invalid. Resetting to 0.");
        return 0;
    }

    private void SaveOneValue(string key, int value)
    {
        // RAW string
        string raw = value.ToString();

        // Compute checksum from RAW data
        string checksum = GenerateChecksum(raw);

        // Encode the raw value (Base64)
        string encoded = Encode(raw);

        // Save
        PlayerPrefs.SetString(key, encoded);
        PlayerPrefs.SetString(key + "_chk", checksum);
    }



    public void SaveCurrency()
    {
        // Save in order (same order as LoadCurrency)

        SaveOneValue("GCrystal", GCrystal);
        SaveOneValue("RCrystal", RCrystal);
        SaveOneValue("CCrystal", CCrystal);
        SaveOneValue("GoldCrystal", GoldCrystal);

        PlayerPrefs.Save();
    }


    public void SetCurrency(int gCrystal, int rCrystal, int cCrystal, int goldCrystal)
    {
        GCrystal = gCrystal;
        RCrystal = rCrystal;
        CCrystal = cCrystal;
        GoldCrystal = goldCrystal;

        SaveCurrency();
    }

    public  (int gCrystal ,int rCrystal ,int cCrystal ,int goldCrystal) GetCurrency()
    {
        return  (GCrystal,  RCrystal, CCrystal, GoldCrystal);
    }
    // --- SAVE ---
    private void SaveUnlock(Unlockdata data)
    {
        string key = "Unlock_" + data.TextureID;

        string json = JsonUtility.ToJson(data);
        string encoded = Encode(json);
        string checksum = GenerateChecksum(json);

        PlayerPrefs.SetString(key, encoded);
        PlayerPrefs.SetString(key + "_chk", checksum);
        PlayerPrefs.Save();
    }

    private Unlockdata LoadUnlock(int textureID)
    {
        string key = "Unlock_" + textureID;

        if (!PlayerPrefs.HasKey(key))
        {
            return new Unlockdata
            {
                TextureID = textureID,
                unlocked = false
            };
        }
        string encoded = PlayerPrefs.GetString(key);
        string checksumSaved = PlayerPrefs.GetString(key + "_chk", "");

        string json = Decode(encoded);

        // Generate a checksum of the decoded JSON
        string checksumComputed = GenerateChecksum(json);

        // Compare — if mismatch, data was tampered with
        if (checksumSaved != checksumComputed)
        {
            Debug.LogWarning("TAMPER DETECTED for unlock ID " + textureID);

            // Return default safe value
            return new Unlockdata
            {
                TextureID = textureID,
                unlocked = false
            };
        }

        // Valid data → return it
        return JsonUtility.FromJson<Unlockdata>(json);

    }
    
    public void SaveAllUlocks(List<Unlockdata> data)//save all data from list in this script
    {
        data.RemoveAll(cell => cell.unlocked == false);
        foreach (Unlockdata temp in data)
        {
            SaveUnlock(temp);
        }
    }
    public void LoadAllUnlocks(int numberofUnlocks)//load all data into list in this script
    {
        for (int i = 0; i < numberofUnlocks; i++)
        {
            string tempkey = "Unlock_" + i;
            if (PlayerPrefs.HasKey(tempkey))
            {
                Unlocked.Add(LoadUnlock(i));

            }

        }

    }

    
    public void SetUnlocked(int textureID, bool value)//setting in list
    {
        Unlocked[textureID].unlocked = value;
    }

   
    public Unlockdata GetUnlocked(int textureID)//returns texture from list
    {
        Unlockdata data;
        //check id exists
        string tempkey = "Unlock_" + textureID;
        if (PlayerPrefs.HasKey(tempkey))
        {
            data = Unlocked[textureID];
        }
        else
        {
            data = new Unlockdata { TextureID = textureID, unlocked = false };
        }
           
        return data;

    }
    public void CreateNewUlockable(int ID)
    {
        Unlockdata data = new Unlockdata { TextureID = ID, unlocked = true }; ;
        SaveUnlock(data);
    }

}

