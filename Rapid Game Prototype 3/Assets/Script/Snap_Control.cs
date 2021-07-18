using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap_Control : MonoBehaviour
{
    public List<Transform> SPoints;
    public List<Drag_N_Drop> DObjects;
    public float SRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Drag_N_Drop drag in DObjects)
        {
            drag.dragEndCallBack = OnDragEnd;
        }
    }
    private void OnDragEnd(Drag_N_Drop drag)
    {
        float closeDist = -1;
        Transform closeSPoint = null;

        foreach(Transform SPoint in SPoints)
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
        }
    }
}
