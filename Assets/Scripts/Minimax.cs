using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minimax : MonoBehaviour
{
    string[,] board = new string[3, 3];

    Dictionary<string, int> results = new Dictionary<string, int>() { { "X", -10 },
                                                                      { "O", 10 },
                                                                      {"tie", 0 }};

    string player;
    string ai;

    // Start is called before the first frame update
    void Awake()
    {
        player = Gamemanager.instance.GetPlayerSymbol();
        ai = Gamemanager.instance.GetAISymbol();
        AdjustReward();
    }

    public int[,] BestMove()
    {
        board = BoardManager.instance.GetBoard();
        var bestScore = Mathf.NegativeInfinity;
        int[,] bestMove = new int[1,2];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == "")
                {
                    board[i, j] = ai;
                    var score = MiniMax(board, 0, false);
                    board[i, j] = "";
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove[0, 0] = i;
                        bestMove[0, 1] = j;
                    }
                }
            }
        }
        return bestMove;
    }

    float MiniMax(string [,] board, int depth, bool isMaximaxing)
    {
        string result = BoardManager.instance.CheckWinner(false);
        if(result != "")
        {
            if (results[result] == 10)
                return results[result] - depth;
            else if (results[result] == -10)
                return results[result] + depth;
            else
                return results[result];
        }
        if (isMaximaxing)
        {
            var bestScore = Mathf.NegativeInfinity;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //is spot available
                    if (board[i, j] == "")
                    {
                        board[i, j] = ai;
                        float score = MiniMax(board, depth + 1, false);
                        board[i, j] = "";
                        if (score > bestScore)
                        {
                            bestScore = score;
                        }
                    }
                }
            }
            return bestScore;
        }
        else
        {
            var bestScore = Mathf.Infinity;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //is spot available
                    if (board[i, j] == "")
                    {
                        board[i, j] = player;
                        float score = MiniMax(board, depth + 1, true);
                        board[i, j] = "";
                        if (score < bestScore)
                        {
                            bestScore = score;
                        }
                    }
                }
            }
            return bestScore;
        }
    }

    //minimax rewards has to be changes if Bot goes first or second
    public void AdjustReward()
    {
        //bot plays second
        if (ai == "O")
        {
            results = new Dictionary<string, int>() { { "X", -10 },
                                                          { "O", 10 },
                                                          {"tie", 0 }};
        }
        else // bot plays first
        {
            results = new Dictionary<string, int>() { { "X", 10 },
                                                          { "O", -10 },
                                                          {"tie", 0 }};
        }
    }
}
