using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerButton : MonoBehaviour
{
    public void GoToGarage()
    {
        SceneManager.LoadScene("Garage");
    }
}
