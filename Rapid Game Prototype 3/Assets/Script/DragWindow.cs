using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool beingDragged;

    public RectTransform dragRectTrans;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTrans.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beingDragged = false;
    }
}
