using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;

    private string[,] board = new string[3, 3] { {"", "", "" },
                                                 {"", "", "" },
                                                 {"", "" ,"" }};
    [SerializeField]
    private List<CellInfo> cells;

    [SerializeField]
    private List<GameObject> winningLines;

    #region
    public string[,] GetBoard() { return board; }
    #endregion

    private void Awake()
    {
        MakeThisObjectSingleton();
    }

    public void PutSymbol(int x, int y)
    {
        var currenPlayer = Gamemanager.instance.GetCurrentPlayer();

        foreach (var item in cells)
        {
            if ( currenPlayer == Gamemanager.instance.GetAISymbol() && x == item.X() && y == item.Y())
            {
                board[x, y] = Gamemanager.instance.GetAISymbol();
                item.GetComponent<Image>().sprite = Gamemanager.instance.GetAISprite();
            }
            else
            {
                if(x == item.X() && y == item.Y())
                {
                    board[x, y] = Gamemanager.instance.GetPlayerSymbol();
                    item.GetComponent<Image>().sprite = Gamemanager.instance.GetPlayerSprite();
                }
            }
        }
    }

    public string CheckWinner(bool drawLine)
    {
        string winner = "";

        //horizontal
        for (int i = 0; i < 3; i++)
        {
            if (Equals3(board[i, 0], board[i, 1], board[i, 2]))
            {
                winner = board[i, 0];
                if(drawLine)
                    DrawLine(i, 1, "h");
            }
        }

        //vertical
        for (int i = 0; i < 3; i++)
        {
            if (Equals3(board[0, i], board[1, i], board[2, i]))
            {
                winner = board[0, i];
                if (drawLine)
                    DrawLine(1, i, "v");
            }
        }

        //diagonal
        if (Equals3(board[0, 0], board[1, 1], board[2, 2]))
        {
            winner = board[0, 0];
            if (drawLine)
                DrawLine(1, 1, "v135");
        }

        if (Equals3(board[2, 0], board[1, 1], board[0, 2]))
        {
            winner = board[2, 0];
            if (drawLine)
                DrawLine(1, 1, "v45");
        }

        if (winner == "" && NoAvailableSpots())
        {
            return "tie";
        }
        else
        {
            return winner;
        }
    }

    private void DrawLine(int x, int y, string typeOfLine)
    {
        GameObject centerCell = new GameObject(); 
        foreach (var item in cells)
        {
            if(x == item.X() && y == item.Y())
            {
                centerCell = item.gameObject;
            }
        }

        switch (typeOfLine)
        {
            case "h":
                Instantiate(winningLines[0], centerCell.transform.position, winningLines[0].transform.rotation, centerCell.transform.parent);
                break;
            case "v":
                Instantiate(winningLines[1], centerCell.transform.position, winningLines[1].transform.rotation, centerCell.transform.parent);
                break;
            case "v135":
                Instantiate(winningLines[2], centerCell.transform.position, winningLines[2].transform.rotation, centerCell.transform.parent);
                break;
            case "v45":
                Instantiate(winningLines[3], centerCell.transform.position, winningLines[3].transform.rotation, centerCell.transform.parent);
                break;
        }
    }

    private bool Equals3(string a, string b, string c)
    {
        return (a != "" && a == b && b == c);
    }

    private bool NoAvailableSpots()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == "")
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void ResetBoardValues()
    {
        //reset board
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = "";
            }
        }

        //Get board cells
        CellInfo[] cellsInfo = FindObjectsOfType<CellInfo>();
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i] = cellsInfo[i];
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
