using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OrderMechanics
{
    public static List<string> orders = new List<string>();

    public static void CreateNewOrder()
    {
        orders.Add("Food");

        foreach (string food in orders)
        {
            Debug.Log(food); 
        }
    }
}
