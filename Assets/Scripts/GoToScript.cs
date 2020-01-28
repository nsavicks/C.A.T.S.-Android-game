using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScript : MonoBehaviour
{
    
    public void StartupScene()
    {
        SceneManager.LoadScene("StartupScene");
    }

    public void GarageScene()
    {
        SceneManager.LoadScene("Garage");
    }

    public void AddPlayerScene()
    {
        SceneManager.LoadScene("AddPlayer");
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
