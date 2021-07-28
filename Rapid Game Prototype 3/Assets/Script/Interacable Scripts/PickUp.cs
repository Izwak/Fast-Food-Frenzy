using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform orderMenu;
    public Transform CustomerParent;

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
        if (orderMenu.transform.childCount >= index)
        {
            Destroy(orderMenu.transform.GetChild(index).gameObject);

            // Reset pos of orders
            for (int i = 0; i < orderMenu.transform.childCount; i++)
            {
                if (i < index)
                {
                    orderMenu.transform.GetChild(i).localPosition = new Vector3(-1000 + 300 * i, 500, 0);
                }
                else 
                {
                    orderMenu.transform.GetChild(i).localPosition = new Vector3(-1000 + 300 * (i - 1), 500, 0);
                }

                if (orderMenu.transform.childCount > 8)
                {
                    if (i < index)
                    {
                        orderMenu.transform.GetChild(i).transform.localPosition = new Vector3(-1000 + (2000.0f / (orderMenu.transform.childCount - 2)) * (i), 500, 0);
                    }
                    else
                    {
                        orderMenu.transform.GetChild(i).transform.localPosition = new Vector3(-1000 + (2000.0f / (orderMenu.transform.childCount - 2)) * (i - 1), 500, 0);
                    }
                }
            }
        }
    }
}
