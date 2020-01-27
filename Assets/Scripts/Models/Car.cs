using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car
{
   
    public int playerId
    {
        get;
        set;
    }

    public CarPart chassis
    {
        get;
        set;
    }

    public CarPart attack1
    {
        get;
        set;
    }

    public CarPart attack2
    {
        get;
        set;
    }

    public CarPart frontWheel
    {
        get;
        set;
    }

    public CarPart backWheel
    {
        get;
        set;
    }

    public CarPart forklift
    {
        get;
        set;
    }

    public RenderedCar renderedCar
    {
        get;
        set;
    }

}
