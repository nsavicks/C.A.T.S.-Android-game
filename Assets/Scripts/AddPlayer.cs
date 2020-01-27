using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AddPlayer : MonoBehaviour
{

    public InputField nickname;
    public InputField firstName;
    public InputField lastName;
    public InputField age;


    public void AddPlayerButton()
    {
        string nick = nickname.text;
        string first = firstName.text;
        string last = lastName.text;
        int a = int.Parse(age.text);


        Player p = DatabaseDataAcces.getPlayerWithNickname(nick);

        if (p == null)
        {
            p = new Player();
            p.nickname = nick;
            p.firstName = first;
            p.lastName = last;
            p.age = a;

            DatabaseDataAcces.insertPlayer(p);

        }

        SceneManager.LoadScene("StartupScene");

    }

}
