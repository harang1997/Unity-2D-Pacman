using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScr : MonoBehaviour
{
    public Text scoretext;
    public int score = 0;

    void Start()
    {
        score = 0;
        scoretext = GetComponent<Text>();
    }

    void Update()
    {

    }

    public void Score()
    {
        score += 10;
        scoretext.text = score.ToString();
    }

    public void PowerItemScore()
    {
        score += 50;
        scoretext.text = score.ToString();
    }

    public void EnemyScore()
    {
        score += 200;
        scoretext.text = score.ToString();
    }
}
