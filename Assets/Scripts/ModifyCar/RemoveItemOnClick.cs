using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemOnClick : MonoBehaviour
{

    public int type;
    public GameObject item;
    public ModifyCar modifyCar;

    public void OnMouseDown()
    {
        modifyCar.RemoveItem(item, type);
    }

}
