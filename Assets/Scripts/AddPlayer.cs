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

        p = DatabaseDataAcces.getPlayerWithNickname(nick);

        int chassisId = DatabaseDataAcces.InsertHasCarPart(p.id, 1, 1);
        int attackId = DatabaseDataAcces.InsertHasCarPart(p.id, 7, 1);
        int wheelId = DatabaseDataAcces.InsertHasCarPart(p.id, 5, 1);
        int wheel2Id = DatabaseDataAcces.InsertHasCarPart(p.id, 5, 1);

        HasCarPart chassis = DatabaseDataAcces.getHasCarPart(chassisId);
        HasCarPart attack = DatabaseDataAcces.getHasCarPart(attackId);
        HasCarPart wheel = DatabaseDataAcces.getHasCarPart(wheelId);
        HasCarPart wheel2 = DatabaseDataAcces.getHasCarPart(wheel2Id);

        DatabaseDataAcces.InsertPlayerCar(p.id);

        DatabaseDataAcces.changePart(chassis, 1);
        DatabaseDataAcces.changePart(attack, 1);
        DatabaseDataAcces.changePart(wheel, 1);
        DatabaseDataAcces.changePart(wheel2, 2);

        DatabaseDataAcces.insertBox(p.id, 0);
        DatabaseDataAcces.insertBox(p.id, 0);

        SceneManager.LoadScene("StartupScene");

    }

}
