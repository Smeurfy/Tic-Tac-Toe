using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour
{

    [SerializeField]
    private Text xWins;
    [SerializeField]
    private Text oWins;

    private void Start()
    {
        UpdateScore();
    }

    public void IncreaseScore(string winner)
    {

        if(winner != "tie")
        {
            int score = PlayerPrefs.GetInt(winner);
            score++;
            PlayerPrefs.SetInt(winner, score);
        }
        UpdateScore();
    }

    private void UpdateScore()
    {
        xWins.text = PlayerPrefs.GetInt("X").ToString();
        oWins.text = PlayerPrefs.GetInt("O").ToString();
    }
}
