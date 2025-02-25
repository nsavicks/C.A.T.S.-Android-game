﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarageButtons : MonoBehaviour
{

    public GameObject settingsMenu;
    public GameObject gameHistory;
    public Toggle music, controls;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
        if (!gameHistory.activeSelf)
        {
            music.isOn = PlayerPrefs.GetInt("music") == 1;
            controls.isOn = PlayerPrefs.GetInt("controls") == 1;
            settingsMenu.SetActive(true);
        }
    }

}
