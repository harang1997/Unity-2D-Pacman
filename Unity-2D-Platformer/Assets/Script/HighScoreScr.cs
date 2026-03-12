using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScr : MonoBehaviour
{
    public Text scoretext;
    public Text highscoretext;

    public int highscore = 0;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("Key_Int", highscore);
        highscoretext.text = highscore.ToString();
    }

    void Update()
    {
        ScoreScr scr = GameObject.Find("Score").GetComponent<ScoreScr>();
        if (scr.score > highscore)
        {
            PlayerPrefs.SetInt("Key_Int", scr.score);
            highscoretext.text = scr.score.ToString();
        }
    }
}
