using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    [SerializeField] 
    private Transform entryContainer;

    [SerializeField] 
    private Transform _entryTemplate;

    [SerializeField] 
    private List<Transform> highscoreEntryTransformList;
    
    private int _jsonCounter;

    //[SerializeField]
    //private List<HighscoreEntry> highscoreEntryList;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // initially everything is inactive
        this.gameObject.SetActive(false);
        _entryTemplate.gameObject.SetActive(false);
    }
    
    // gets called when player dies
    public void MakeScoreboard (int score)
    {
        // make leaderboard appear
        this.gameObject.SetActive(true);

        // add new score from player tot he storage list
        AddHighscoreEntry(score);
        
        // make storage list accessable
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        // sort the list
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        
        // create the actual entry for the leaderboard
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntry(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    // create new entry for the leaderboard
    private void CreateHighscoreEntry(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        // how much entries are apart in leaderboard
        float templateHeight = 0.8f;
        
        // instantiate template with right hight
        Transform entryTransform = Instantiate(_entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform >();
        entryRectTransform.anchoredPosition = new Vector3(0,-templateHeight * transformList.Count, 0);

        // make entry appear if position/rank is 10 or lower
        if (transformList.Count < 10)
        {
            entryTransform.gameObject.SetActive(true);
        }

        // number for the position/rank
        string rank;
        if (transformList.Count > 8)
        {
            rank = (transformList.Count + 1).ToString();
        }
        else
        {
            rank = "0" + (transformList.Count + 1);
        }
        entryTransform.Find("PositionEntry").GetComponent<TextMeshPro>().text = rank;

        // number for the score
        int score = highscoreEntry.score;
        entryTransform.Find("ScoreEntry").GetComponent<TextMeshPro>().text = score.ToString();

        // highlight the entry
        // if its the newest score
        entryTransform.Find("BackgroundEntry").gameObject.SetActive(highscoreEntry.number == _jsonCounter);
        
        // add entry to the leaderboard list
        transformList.Add(entryTransform);
    }

    // save new score
    // add score to storage list
    public void AddHighscoreEntry(int score)
    {
        // make storage list accessable
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        _jsonCounter = highscores.highscoreEntryList.Count;
        
        // remove all previously stored entries from storage list
        // if over 30 entries in list
        if (_jsonCounter > 30)
        {
            highscores.highscoreEntryList = new List<HighscoreEntry>() { };
        }
        
        // make new highscore entry and add to storage list
        HighscoreEntry highscoreEntry = new HighscoreEntry {score = score, number = highscores.highscoreEntryList.Count + 1};
        highscores.highscoreEntryList.Add(highscoreEntry);

        // update jsonCounter to new length of storage list
        _jsonCounter = highscores.highscoreEntryList.Count;

        // save updated storage list
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    
    // reset storage list 
    // remove all the scores so far
    private void ResetScores()
    {
        
    }

    // class for storage list with all the highscores 
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    
    // represents a single highscore entry
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public int number;
    }
    
}


