using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoxOnClick : MonoBehaviour
{

    public Garage garage;
    public Box box;

    public void OnMouseDown()
    {
        long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;

        long elapsed = (now - box.acquired) / (1000 * 60);

        if (elapsed >= 120)
        {
            garage.OpenBox(box.id);
        }    
    }

}
