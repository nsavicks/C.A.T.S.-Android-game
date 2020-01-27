using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttackDetection : MonoBehaviour
{

    public Game game;
    public int player;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("LeftWall") || collision.gameObject.tag.Equals("RightWall"))
        {
            game.HitWall(player);
        }
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("LeftWall") || collision.gameObject.tag.Equals("RightWall"))
        {
            game.ExitFromWall(player);
        }

    }

}
