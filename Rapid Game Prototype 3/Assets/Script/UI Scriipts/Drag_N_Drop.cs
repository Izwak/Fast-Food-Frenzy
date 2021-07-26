using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_N_Drop : MonoBehaviour
{
    public delegate void dragEnd(Drag_N_Drop DObjects);

    public dragEnd dragEndCallBack;

    private bool drag = false;
    private Vector3 mouseDstart;
    private Vector3 spriteDstart;
    private void OnMouseDown()
    {
        drag = true;
        mouseDstart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDstart = transform.localPosition;
    }
    private void OnMouseDrag()
    {
        if(drag)
        {
            transform.localPosition = spriteDstart + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDstart);
        }
    }
    private void OnMouseUp()
    {
        drag = false;

        dragEndCallBack(this);
    }
}
