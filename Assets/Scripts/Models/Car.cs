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

    public int chassisStars
    {
        get;
        set;
    }

    public CarPart attack1
    {
        get;
        set;
    }

    public int attack1Stars
    {
        get;
        set;
    }

    public CarPart attack2
    {
        get;
        set;
    }

    public int attack2Stars
    {
        get;
        set;
    }

    public CarPart frontWheel
    {
        get;
        set;
    }

    public int frontWheelStars
    {
        get;
        set;
    }

    public CarPart backWheel
    {
        get;
        set;
    }

    public int backWheelStars
    {
        get;
        set;
    }

    public CarPart forklift
    {
        get;
        set;
    }

    public int forkliftStars
    {
        get;
        set;
    }

    public RenderedCar renderedCar
    {
        get;
        set;
    }

    public int GetCarEnergyLeft()
    {
        int energy = 0;

        if (chassis != null)
        {
            energy += chassis.energy * chassisStars;
        }

        if (attack1 != null)
        {
            energy -= attack1.energy * attack1Stars;
        }

        if (attack2 != null)
        {
            energy -= attack2.energy * attack2Stars;
        }

        if (frontWheel != null)
        {
            energy -= frontWheel.energy * frontWheelStars;
        }

        if (backWheel != null)
        {
            energy -= backWheel.energy * backWheelStars;
        }

        if (forklift != null)
        {
            energy -= forklift.energy * forkliftStars;
        }

        return energy;

    }

    public int GetCarPower()
    {
        int power = 0;

        if (chassis != null)
        {
            power += chassis.power * chassisStars;
        }

        if (attack1 != null)
        {
            power += attack1.power * attack1Stars;
        }

        if (attack2 != null)
        {
            power += attack2.power * attack2Stars;
        }

        if (frontWheel != null)
        {
            power += frontWheel.power * frontWheelStars;
        }

        if (backWheel != null)
        {
            power += backWheel.power * backWheelStars;
        }

        if (forklift != null)
        {
            power += forklift.power * forkliftStars;
        }

        return power;
    }

    public int GetCarHealth()
    {
        int health = 0;

        if (chassis != null)
        {
            health += chassis.health * chassisStars;
        }

        if (attack1 != null)
        {
            health += attack1.health * attack1Stars;
        }

        if (attack2 != null)
        {
            health += attack2.health * attack2Stars;
        }

        if (frontWheel != null)
        {
            health += frontWheel.health * frontWheelStars;
        }

        if (backWheel != null)
        {
            health += backWheel.health * backWheelStars;
        }

        if (forklift != null)
        {
            health += forklift.health * forkliftStars;
        }

        return health;
    }

}
