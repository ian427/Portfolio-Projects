using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    
    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        
        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{ score = 534098, name = "AAA"},
            new HighscoreEntry{ score = 768432, name = "AMM"},
            new HighscoreEntry{ score = 902345, name = "HAT"},
            new HighscoreEntry{ score = 13875, name = "JON"},
            new HighscoreEntry{ score = 645178, name = "MAX"},
            new HighscoreEntry{ score = 56734, name = "DAV"},
            new HighscoreEntry{ score = 671937, name = "Tom"},
            new HighscoreEntry{ score = 395647, name = "JOE"},
        };

        // sort entry list by score
        for(int i = 0; i < highscoreEntryList.Count; i++)
        {
            for(int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    //swap positions
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {


        // making the table 
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRestTransform = entryTransform.GetComponent<RectTransform>();
        float rowHeight = 30f;
        entryRestTransform.anchoredPosition = new Vector2(0, -rowHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;

            }

        entryTransform.Find("TextRank").GetComponent<TMP_Text>().text = rankString;

            int score = highscoreEntry.score;

            entryTransform.Find("TextScore").GetComponent<TMP_Text>().text = score.ToString();

            string name = highscoreEntry.name;
            entryTransform.Find("TextName").GetComponent<TMP_Text>().text = name;

            transformList.Add(entryTransform);

    }

    // Represents a single high score entry
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }


}
