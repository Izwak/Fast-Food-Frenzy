using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<GameObject> orders;

    public GameObject NewOrderObj()
    {
        GameObject newOrder = Instantiate(orders[0]);

        int orderSize = Random.Range(1, 5);

        for (int i = 0; i < orderSize; i++)
        {
            GameObject newFood = Instantiate(orders[Random.Range(1, orders.Count)]);
            newFood.transform.SetParent(newOrder.transform);
            newFood.transform.localPosition = Vector3.zero;
            newFood.transform.localScale = Vector3.one;
        }

        return newOrder;
        //return orders[Random.Range(0, orders.Count)];
    }

    public void CreateNewOrder()
    {

    }
}
    