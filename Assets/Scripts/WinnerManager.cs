using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class WinnerManager : MonoBehaviour
{
    [SerializeField]
    private Image imageSlot;

    [SerializeField]
    private Sprite draw;

    private void Start()
    {
        ShowWinner();
    }

    private void ShowWinner()
    {
        string winner = Gamemanager.instance.GetWinner();
        switch (winner)
        {
            case "X":
                imageSlot.sprite = Gamemanager.instance.GetSprites()[0];
                break;
            case "O":
                imageSlot.sprite = Gamemanager.instance.GetSprites()[1];
                break;
            case "tie":
                imageSlot.sprite = draw;
                imageSlot.transform.localScale = new Vector3(6, 4, 4);
                imageSlot.GetComponentInChildren<Text>().text = "Draw";
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        Gamemanager.instance.SetPlayAgain(true);
        SceneManager.LoadScene(1);
        
    }

    public void MainMenu()
    {
        Gamemanager.instance.SetGameOver(false);
        Gamemanager.instance.SetPlayAgain(true);
        SceneManager.LoadScene(0);
    }
}
