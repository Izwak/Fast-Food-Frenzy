using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<GameObject> orders;

    public GameObject NewOrderObj()
    {
        return orders[Random.Range(0, orders.Count)];
    }

    public void CreateNewOrder()
    {

    }
}
