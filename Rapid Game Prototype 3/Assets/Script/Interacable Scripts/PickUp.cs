using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject HappyMeal;

    public Transform orderMenu;
    public Transform CustomerParent;

    public int DoesOrderMatch(GameObject orderObj)
    {
        for (int i = 0; i < orderMenu.transform.childCount; i++)
        {
            GameObject order = orderMenu.transform.GetChild(i).gameObject;
            int orderSize = order.transform.childCount;

            if (orderObj.CompareTag("Food") && orderSize == 1)
            {
                //print(orderObj.name + " =? " + order.transform.GetChild(0).name);

                if (orderObj.name == order.transform.GetChild(0).name)
                {
                    return i;
                }
            }
            else if (orderObj.CompareTag("Tray"))
            {
                //for ()
            }
        }



        return -1;
    }

    public void RemoveDisplayOrder(int index)
    {
        if (orderMenu.transform.childCount >= index)
        {

        }
    }

    public void RemoveDisplayOrder2(int index)
    {
        if (orderMenu.transform.childCount >= index)
        {
            Destroy(orderMenu.transform.GetChild(index).gameObject);

            // Reset pos of orders
            for (int i = 0; i < orderMenu.transform.childCount; i++)
            {
                if (i < index)
                {
                    orderMenu.transform.GetChild(i).localPosition = new Vector3(-1050 + 300 * i, 500, 0);
                }
                else 
                {
                    orderMenu.transform.GetChild(i).localPosition = new Vector3(-1050 + 300 * (i - 1), 500, 0);
                }

                if (orderMenu.transform.childCount > 6)
                {
                    if (i < index)
                    {
                        orderMenu.transform.GetChild(i).transform.localPosition = new Vector3(-1050 + (1200.0f / (orderMenu.transform.childCount - 2)) * (i), 500, 0);
                    }
                    else
                    {
                        orderMenu.transform.GetChild(i).transform.localPosition = new Vector3(-1050 + (1200.0f / (orderMenu.transform.childCount - 2)) * (i - 1), 500, 0);
                    }
                }
            }
        }
    }
}
