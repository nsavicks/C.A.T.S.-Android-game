using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GamePlayed
{
   
    public int id
    {
        get;
        set;
    }

    public Player firstPlayer
    {
        get;
        set;
    }

    public Player secondPlayer
    {
        get;
        set;
    }

    public int winner
    {
        get;
        set;
    }

    public DateTime datetime
    {
        get;
        set;
    }
}
