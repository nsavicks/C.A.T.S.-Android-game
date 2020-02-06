using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupButtons : MonoBehaviour
{

    public Dropdown dropdown;

    public void PlayButton()
    {

        PlayerPrefs.SetString("player", dropdown.captionText.text);

        SceneManager.LoadScene("Garage");
    }

    public void AddPlayerButton()
    {
        SceneManager.LoadScene("AddPlayer");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
