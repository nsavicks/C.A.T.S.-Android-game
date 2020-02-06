using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public Toggle music, controls;
    public GameObject newItem;

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("music", (music.isOn) ? 1 : 0);
        PlayerPrefs.SetInt("controls", (controls.isOn) ? 1 : 0);

        AudioSource source = FindObjectOfType<AudioSource>();

        if (source != null && !music.isOn && source.isPlaying)
        {
            source.Stop();
        }
    }

    public void CloseNewItem()
    {
        Transform itemTransform = newItem.transform.GetChild(3);
        Destroy(itemTransform.gameObject);
        newItem.SetActive(false);
    }
}
