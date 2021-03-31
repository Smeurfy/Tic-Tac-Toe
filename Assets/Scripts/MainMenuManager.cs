using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Gamemanager.instance.AssignSymbols("X");
        Gamemanager.instance.SetDifficulty("Easy");
    }

    public void ChangePlayerSymbol(string symbol)
    {
        Gamemanager.instance.AssignSymbols(symbol);
    }

    public void ChangeDifficulty(Text t)
    {
        Gamemanager.instance.SetDifficulty(t.text);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
