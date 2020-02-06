using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupScene : MonoBehaviour
{

    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {

        List<Player> players = DatabaseDataAcces.getAllPlayers();

        dropdown.ClearOptions();

        foreach(Player p in players)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = p.nickname });
        }

        dropdown.RefreshShownValue();

    }

}
