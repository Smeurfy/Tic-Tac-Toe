using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public static BotController instance;

    private void Awake()
    {
        MakeThisObjectSingleton();
    }

    public void MakePlay()
    {
        var difficulty = Gamemanager.instance.GetDifficulty();
        int[,] bestMove = new int[1, 2];

        //make move based on difficulty
        switch (difficulty)
        {
            case "Easy":
                bestMove = PickRandom();
                break;
            case "Medium":
                bestMove = PickBtwTwo();
                break;
            case "Hard":
                bestMove = GetComponent<Minimax>().BestMove();
                break;
        }
        MakeAIPlay(bestMove);

    }

    private int[,] PickBtwTwo()
    {
        float randVal = Random.Range(0.0f, 1.0f);
        if(randVal > 0.5)
        {
            return PickRandom();
        }
        else
        {
            return GetComponent<Minimax>().BestMove();
        }
    }

    private int[,] PickRandom()
    {
        var board = BoardManager.instance.GetBoard();
        int[,] bestMove = new int[1, 2];
        bool emptyMove = false;
        while (!emptyMove)
        {
            int x = Random.Range(0, 3);
            int y = Random.Range(0, 3);
            if(board[x, y] == "")
            {
                emptyMove = true;
                bestMove = new int[1, 2] { { x, y } };
                return bestMove;
            }
        }
        return bestMove;
    }

    public void MakeAIPlay(int[,] bestMove)
    {
        BoardManager.instance.PutSymbol(bestMove[0, 0], bestMove[0, 1]);
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
