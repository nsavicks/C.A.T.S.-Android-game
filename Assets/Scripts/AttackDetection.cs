using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    public Game game;
    public int player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Chassis"))
        {
            game.StartedAttack(player);
        }
        
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag.Equals("Chassis"))
        {
            game.EndedAttack(player);
        }
    }
}
