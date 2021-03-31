using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo : MonoBehaviour
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    #region Getters
    public int X() { return x; }
    public int Y() { return y; }

    #endregion
}
