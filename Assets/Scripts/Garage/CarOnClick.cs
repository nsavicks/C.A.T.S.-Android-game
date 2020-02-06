using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarOnClick : MonoBehaviour
{

    public Garage garage;

    public void OnMouseDown()
    {
        if (!garage.settingsMenu.activeSelf && !garage.gameHistory.activeSelf)
        {
            SceneManager.LoadScene("ModifyCar");
        }
        
    }
}
