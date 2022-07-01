using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighscoreUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoresText;


    private void Start()
    {
        string highScores = "";
        List<ScoreData> scoreDataList = HighScoreManager.Instance.scoreDataList;
        // If no scores stored.
        if(scoreDataList.Count == 0)
        {
            highScoresText.SetText("No one here!");
            return;
        }
        //Go through the list of score data and add the scores to a string.
        for(int i = 0; i < scoreDataList.Count; i++)
        {
            highScores += (i+1) + ". " + scoreDataList[i].playerName + " ... " + scoreDataList[i].score + "\n";
        }
        //Display scores
        highScoresText.SetText(highScores);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
