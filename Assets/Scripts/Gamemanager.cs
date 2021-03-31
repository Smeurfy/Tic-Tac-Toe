using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField]
    private string player;

    [SerializeField]
    private string ai;

    [SerializeField]
    private string difficulty;
    [SerializeField]
    private string currentPlayer;

    [SerializeField]
    List<Sprite> sprites;
    [SerializeField]
    private Sprite playerSprite;
    [SerializeField]
    private Sprite aiSprite;
    [SerializeField]
    private bool gameOver = false;
    [SerializeField]
    private bool playAgain = true;

    private string winner;



    #region Getters and Setters
    public Sprite GetPlayerSprite() { return playerSprite; }
    public string GetPlayerSymbol() { return player; }
    public Sprite GetAISprite() { return aiSprite; }
    public string GetAISymbol() { return ai; }
    public string GetCurrentPlayer() { return currentPlayer; }
    public string GetDifficulty() { return difficulty; }
    public bool GetGameOver() { return gameOver; }
    public string GetWinner() { return winner; }
    public List<Sprite> GetSprites() { return sprites; }

    public void SetDifficulty(string dif) { difficulty = dif; }
    public void SetPlayAgain(bool value) { playAgain = value; }
    public void SetGameOver(bool value) { gameOver = value; }
    #endregion

    private void Awake()
    {
        MakeThisObjectSingleton();
        PlayerPrefs.SetInt("X", 0);
        PlayerPrefs.SetInt("O", 0);
    }

    private void Update()
    {
        if (playAgain && SceneManager.GetActiveScene().buildIndex == 1)
        {
            playAgain = false;
            gameOver = false;
            currentPlayer = "X";

            //Reset Board Values
            BoardManager.instance.ResetBoardValues();


            //bot plays first when scene is loaded
            if (ai == "X")
            {
                BotPlaysFirst();
            }
        }
    }

    private void BotPlaysFirst()
    {
        BotController.instance.MakePlay();
        NextTurn();
    }

    public void NextTurn()
    {
        var result = BoardManager.instance.CheckWinner(true);
        if (result != "")
        {
            gameOver = true;
            winner = result;
            FindObjectOfType<ScoreBoardManager>().IncreaseScore(winner);
            StartCoroutine(LoadWinnerScene());
        }
        else
        {
            //changes turns between the player and the bot
            if (currentPlayer == "X")
            {
                if (player == "X")
                {
                    currentPlayer = "O";
                    BotController.instance.MakePlay();
                    NextTurn();
                }
                else
                {
                    currentPlayer = "O";
                }
            }
            else
            {
                if (ai == "O")
                {
                    currentPlayer = "X";
                }
                else
                {
                    currentPlayer = "X";
                    BotController.instance.MakePlay();
                    NextTurn();
                }
            }
        }
    }

    private IEnumerator LoadWinnerScene()
    {
        //wait 1.5 seconds before loading winner scene
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MakePlayerPlay(GameObject gObj)
    {
        int x = gObj.GetComponent<CellInfo>().X();
        int y = gObj.GetComponent<CellInfo>().Y();
        var board = BoardManager.instance.GetBoard();
        if (board[x, y] == "")
        {
            BoardManager.instance.PutSymbol(x, y);
            NextTurn();
        }
    }


    public void AssignSymbols(string symbol)
    {
        player = symbol;

        //Allows the player to start first or second
        if (player == "X")
        {
            playerSprite = sprites[0];
            aiSprite = sprites[1];

            currentPlayer = player;
            ai = "O";
        }
        else
        {
            playerSprite = sprites[1];
            aiSprite = sprites[0];

            ai = "X";
            currentPlayer = ai;
        }

    }

    private void MakeThisObjectSingleton()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
