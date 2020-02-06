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

        

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        Collider2D collider = col.collider;
        ContactPoint2D contactPoint = col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.right, contactPoint.normal);
        

        if (collider.tag.Equals("LeftWall") || collider.tag.Equals("RightWall") || collider.tag.Equals("Floor") || collider.tag.Equals("Sky"))
        {

            GameObject explosion = Instantiate(game.explosionPrefab, contactPoint.point, rot);
            //Debug.Log(collider.name);
            game.RocketHit(player, rocket, direction);
        }
        else if (collider.tag.Equals("Chassis"))
        {
            GameObject explosion = Instantiate(game.explosionPrefab, contactPoint.point, rot);
            game.RocketHitChassis(player, attackId, rocket, direction);
            //Debug.Log(collider.name);
        }

    }
}
