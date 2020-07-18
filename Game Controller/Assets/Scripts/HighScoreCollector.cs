using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreCollector : MonoBehaviour
{
    private PlayerProgress playerProgress;
    // Use this for initialization
    void Start()
    {
        LoadProgress();
    }


    private void LoadProgress()
    {
        playerProgress = new PlayerProgress();
        if(PlayerPrefs.HasKey("highestScore")) 
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }
    public int GetHighestScore() 
    { 
         return playerProgress.highestScore;
    }

    public void SaveProgress() 
    {
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }
    public void UpdatePlayerPref(int gameScore)
    {
        if (gameScore > playerProgress.highestScore) 
        {
            playerProgress.highestScore = gameScore;
            SaveProgress();
         }
    }
}