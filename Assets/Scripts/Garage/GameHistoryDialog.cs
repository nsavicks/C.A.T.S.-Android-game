using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistoryDialog : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject gameHistory;

    public void OpenGameHistory()
    {
        if (!settingsMenu.activeSelf)
        {
            gameHistory.SetActive(true);
        }
    }

    public void CloseGameHistory()
    {
        gameHistory.SetActive(false);
    }
}
