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

            // Reset pos of orders
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                if (i < index)
                {
                    canvas.transform.GetChild(i).localPosition = new Vector3(-1000 + 300 * i, 500, 0);
                }
                else 
                {
                    canvas.transform.GetChild(i).localPosition = new Vector3(-1000 + 300 * (i - 1), 500, 0);
                }
            }
        }
    }
}
