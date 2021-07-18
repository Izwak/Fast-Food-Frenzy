using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler
{
    public RectTransform dragRectTrans;
    public void OnDrag(PointerEventData eventData)
    {
        dragRectTrans.anchoredPosition += eventData.delta;
    }
}
