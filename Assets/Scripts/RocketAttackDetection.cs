using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAttackDetection : MonoBehaviour
{

    public Game game;
    public int player, attackId;
    public GameObject rocket;
    public Vector3 direction;

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag.Equals("LeftWall") || collider.tag.Equals("RightWall") || collider.tag.Equals("Floor") || collider.tag.Equals("Sky"))
        {
            //Debug.Log(collider.name);
            game.RocketHit(player, rocket, direction);
        }
        else if (collider.tag.Equals("Chassis"))
        {
            game.RocketHitChassis(player, attackId, rocket, direction);
            //Debug.Log(collider.name);
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        
    }
}
