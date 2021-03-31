using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public void Click ()
    {
        var instance = Gamemanager.instance;
        if(!instance.GetGameOver() && instance.GetCurrentPlayer() == instance.GetPlayerSymbol())
        {
            Gamemanager.instance.MakePlayerPlay(gameObject);
        }
    }
}
