using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    public HasCarPart hasCarPart;
    public ModifyCar modifyCar;

    public void OnBeginDrag(PointerEventData eventData)
    {
        modifyCar.SelectItem(hasCarPart);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = new Vector3(0, 46.5f);
        modifyCar.UnselectItem();
    }
}
