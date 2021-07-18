using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap_Control : MonoBehaviour
{
    public List<Transform> SPoints;
    public List<Drag_N_Drop> DObjects;
    public bool[] correctPos = new bool[6];

    public float SRange = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        foreach(Drag_N_Drop drag in DObjects)
        {
            drag.dragEndCallBack = OnDragEnd;
        }

        for (int i = 0; i < 6; i++) 
        {
            correctPos[i] = false;
        }
    }
    private void OnDragEnd(Drag_N_Drop drag)
    {
        float closeDist = -1;
        Transform closeSPoint = null;

        correctPos[getDragElement(drag)] = false;

        foreach (Transform SPoint in SPoints)
        {
            float currentDist = Vector2.Distance(drag.transform.localPosition, SPoint.localPosition);
            if(closeSPoint == null || currentDist < closeDist)
            {
                closeSPoint = SPoint;
                closeDist = currentDist;
            }
        }

        if(closeSPoint != null && closeDist <= SRange)
        {
            drag.transform.localPosition = closeSPoint.localPosition;

            print("Click");

            print("Correct Pos: " + elementsMatch(drag, closeSPoint));

            if (elementsMatch(drag, closeSPoint))
            {
                correctPos[getDragElement(drag)] = true;
            }

            print("Has Won: " + won());
        }
    }

    int getDragElement(Drag_N_Drop drag)
    {
        int dragElememt = -1;

        for (int i = 0; i < 6; i++)
        {
            if (drag.transform == DObjects[i].transform)
            {
                dragElememt = i;
            }
        }

        return dragElememt;
    }


    bool elementsMatch(Drag_N_Drop drag, Transform snap)
    {
        int dragElememt = -1;
        int snapElememt = -1;

        for (int i = 0; i < 6; i++)
        {
            if (drag.transform == DObjects[i].transform)
            {
                dragElememt = i;
            }

            if (snap == SPoints[i].transform)
            {
                snapElememt = i;
            }
        }


        if (dragElememt == -1 || snapElememt == -1)
        {
            print("Lol error");
        }

        print("Element " + dragElememt + ", " + snapElememt);

        if (dragElememt == snapElememt)
        {
            return true;
        }

        return false;
    }

    bool won ()
    {
        bool result = true;

        for (int i = 0; i < 6; i++)
        {
            if (!correctPos[i])
            {
                result = false;
            }
        }

        return result;
    }
}
