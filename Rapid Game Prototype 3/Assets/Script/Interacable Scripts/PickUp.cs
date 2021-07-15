using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveDisplayOrder(int index)
    {
        if (canvas.transform.childCount >= index)
        {
            Destroy(canvas.transform.GetChild(index).gameObject);
        }
    }
}
