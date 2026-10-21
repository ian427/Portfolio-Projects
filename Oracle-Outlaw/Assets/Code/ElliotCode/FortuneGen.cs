using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FortuneGen : MonoBehaviour
{
    public WordArray wordArray;
    public TMP_Text fortuneTold;

    // Start is called before the first frame update
    void Start()
    {
        string fortuneGen = "a " + wordArray.wordAdj[Random.Range(0, wordArray.wordAdj.Length)] + " " + wordArray.wordnoun[Random.Range(0, wordArray.wordnoun.Length)] + " " + wordArray.wordverb[Random.Range(0, wordArray.wordverb.Length)] + " a " + wordArray.wordAdj[Random.Range(0, wordArray.wordAdj.Length)] + " " + wordArray.wordnoun[Random.Range(0, wordArray.wordnoun.Length)];

        fortuneTold.text = fortuneGen;


        //wordArray.wordverb[];
        //wordArray.wordnoun[];
        //wordArray.wordAdj[];
    }
}
