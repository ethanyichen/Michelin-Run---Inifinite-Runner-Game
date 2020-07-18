using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {
    public HighScoreCollector highScoreCollector;

    public Text HighscoreText;
    public int HighestScore;



    // Use this for initialization

    void Update()
    {
        HighestScore = highScoreCollector.GetHighestScore();
        HighscoreText.text = HighestScore.ToString();
    }

    // Update is called once per frame

}
