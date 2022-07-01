using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;
    public string currentPlayerName;
    public int currentScore;
    public string bestPlayerName;
    public int bestScore;
    public List<ScoreData> scoreDataList;

    private void Awake()
    {
        LoadScores();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    [System.Serializable]
    public class HighScoreData
    {
        public List<ScoreData> scoreDataList;
    }

    //Adds player name and score into an object and store in a list.
    public void AddScore(string name, int score)
    {
        ScoreData scoreData = new ScoreData();
        scoreData.playerName = name;
        scoreData.score = score;

        if (scoreDataList.Count == 0)
        {
            scoreDataList.Add(scoreData);
            return;
        }

        //Insert object data based on score.
        for(int i = 0; i < scoreDataList.Count; i++)
        {
            //Place score infront of current index if score is higher.
            if (scoreDataList[i].score < scoreData.score)
            {
                scoreDataList.Insert(i, scoreData);
                return;
            }
            //Place score infront of current index if score is equal.
            else if (scoreDataList[i].score == scoreData.score)
            {
                scoreDataList.Insert(i, scoreData);
                return;
            }
        }
        // Otherwise put at end of highscore.
        scoreDataList.Add(scoreData);
        
    }

    //Saves scores to JSON from a object that holds a list of scoreData objects.
    public void SaveScores()
    {
        HighScoreData data = new HighScoreData();
        data.scoreDataList = scoreDataList;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    //Loads scores from a JSON.
    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            scoreDataList = data.scoreDataList;
            bestScore = scoreDataList[0].score;
            bestPlayerName = scoreDataList[0].playerName;
        }
        else
        {
            //If no saveFile
            bestScore = 0;
            bestPlayerName = "No-One";
        }
    }
}

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public int score;
}
