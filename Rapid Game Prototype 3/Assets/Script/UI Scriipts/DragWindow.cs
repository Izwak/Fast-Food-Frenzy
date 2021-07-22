using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    public bool beingDragged;

    public bool hasDropped = false;
    public bool justMoved = false;

    public RectTransform dragRectTrans;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = true;
        justMoved = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTrans.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beingDragged = false;
        hasDropped = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTrans.SetAsLastSibling();
    }
}
