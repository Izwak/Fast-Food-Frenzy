using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject HappyMeal;

    public Transform orderMenu;
    public Transform CustomerParent;

    public bool isDriveTru;

    public int DoesOrderMatch2(GameObject orderObj)
    {
        for (int i = 0; i < orderMenu.transform.childCount; i++)
        {
            GameObject order = orderMenu.transform.GetChild(i).gameObject;
            Tray tray = orderObj.GetComponent<Tray>();
            int orderSize = order.transform.childCount;

            if (orderObj.CompareTag("Food") && orderSize == 1)
            {
                //print(orderObj.name + " =? " + order.transform.GetChild(0).name);

                if (orderObj.name == order.transform.GetChild(0).name)
                {
                    return i;
                }
            }

            if (true)
            {/*
                if (pickUp != null && obj.emptySlot.transform.childCount == 1)
                {
                    Tray tray = obj.emptySlot.transform.GetChild(0).GetComponent<Tray>();

                    // Makes sure there are orders and the tray isnt empty
                    if (tray != null && tray.emptySlot.transform.childCount > 0 && gameManager.orderScreen.transform.childCount > 0)
                    {
                        List<string> foodOnTray = new List<string>();
                        List<int> numFoodOnTray = new List<int>();

                        // name of food on tray = tray.emptySlot.transform.GetChild(i).name

                        // Get all the food from the tray
                        for (int i = 0; i < tray.emptySlot.transform.childCount; i++)
                        {
                            bool foodTypeLogged = false;

                            // Log food

                            // Check if food type is in system
                            if (foodOnTray.Count > 0)
                            {
                                for (int j = 0; j < foodOnTray.Count; j++)
                                {
                                    // Food is in system so add
                                    if (tray.emptySlot.transform.GetChild(i).name == foodOnTray[j])
                                    {
                                        foodTypeLogged = true;
                                        numFoodOnTray[j]++;
                                        break;
                                    }
                                }
                            }

                            // If its a new food create add it to database
                            if (!foodTypeLogged)
                            {
                                foodOnTray.Add(tray.emptySlot.transform.GetChild(i).name);
                                numFoodOnTray.Add(1);
                            }

                        }

                        // Show whats on the tray
                        for (int i = 0; i < foodOnTray.Count; i++)
                        {
                            print("Num of " + foodOnTray[i] + " = " + numFoodOnTray[i]);
                        }

                        //print("FIRST ORDER");

                        // Get all the elements the orders in the menu list
                        for (int orderNum = 0; orderNum < gameManager.orderScreen.transform.childCount; orderNum++)
                        {
                            GameObject order = gameManager.orderScreen.transform.GetChild(orderNum).gameObject;

                            List<string> foodInOrder = new List<string>();
                            List<int> numFoodInOrder = new List<int>();

                            bool foodTypeLogged = false;

                            // Get all food from order

                            for (int foodNUm = 0; foodNUm < order.transform.childCount; foodNUm++)
                            {
                                // Log food

                                // Check if food type is in system
                                if (foodInOrder.Count > 0)
                                {
                                    for (int foodType = 0; foodType < foodInOrder.Count; foodType++)
                                    {
                                        // Food is in system so add
                                        if (order.transform.GetChild(foodNUm).name == foodInOrder[foodType])
                                        {
                                            foodTypeLogged = true;
                                            numFoodInOrder[foodType]++;
                                            break;
                                        }
                                    }
                                }

                                // If its a new food create add it to database
                                if (!foodTypeLogged)
                                {
                                    foodInOrder.Add(order.transform.GetChild(foodNUm).name);
                                    numFoodInOrder.Add(1);
                                }
                            }


                            // Show elements of the first order
                            if (orderNum == 0)
                            {
                                for (int j = 0; j < foodInOrder.Count; j++)
                                {
                                    //print("Num in Order of " + foodInOrder[j] + " = " + numFoodInOrder[j]);
                                }
                            }

                            // Compare order to tray

                            // Check if the number of different objects match
                            if (foodOnTray.Count == foodInOrder.Count)
                            {
                                int numOfMatchingRows = 0;

                                // Go through the food on the tray
                                for (int j = 0; j < foodOnTray.Count; j++)
                                {

                                    // Go through the food on the tray
                                    for (int k = 0; k < foodInOrder.Count; k++)
                                    {
                                        if (foodOnTray[j] == foodInOrder[k] && numFoodOnTray[j] == numFoodInOrder[k])
                                        {
                                            numOfMatchingRows++;

                                            print("Same num of " + foodOnTray[j] + " in order and tray = " + numFoodOnTray[j]);
                                        }
                                    }
                                }

                                // This is where is says it works
                                if (numOfMatchingRows == foodInOrder.Count)
                                {
                                    print("U FOUND A MATCH");
                                }
                                else
                                {
                                    print("NO DICE");
                                }
                            }
                        }
                    }
                }*/
            }

        }

        return -1;
    }

    public int DoesOrderMatch(GameObject orderObj)
    {
        Tray tray = orderObj.GetComponent<Tray>();
        int numOforders = orderMenu.transform.childCount;

        if (numOforders > 0)
        {
            // If you placed food on the counter
            if (orderObj.CompareTag("Food"))
            {
                for (int i = 0; i < numOforders; i++)
                {
                    GameObject order = orderMenu.transform.GetChild(i).gameObject;

                    if (order.transform.childCount == 1 && orderObj.name == order.transform.GetChild(0).name)
                    {
                        return i;
                    }
                }
            }

            // If you placed a tray of food on counter
            else if (tray != null && tray.emptySlot.transform.childCount > 0)
            {
                List<string> foodOnTray = new List<string>();
                List<int> numFoodOnTray = new List<int>();

                // Get all the food from the tray
                for (int i = 0; i < tray.emptySlot.transform.childCount; i++)
                {
                    bool foodTypeLogged = false;

                    // Log food

                    // Check if food type is in system
                    if (foodOnTray.Count > 0)
                    {
                        for (int j = 0; j < foodOnTray.Count; j++)
                        {
                            // Food is in system so add
                            if (tray.emptySlot.transform.GetChild(i).name == foodOnTray[j])
                            {
                                foodTypeLogged = true;
                                numFoodOnTray[j]++;
                            }
                        }
                    }

                    // If its a new food create add it to database
                    if (!foodTypeLogged)
                    {
                        foodOnTray.Add(tray.emptySlot.transform.GetChild(i).name);
                        numFoodOnTray.Add(1);
                    }

                }

                // Show whats on the tray
                print("TRAY");
                for (int i = 0; i < foodOnTray.Count; i++)
                {
                    print("Num of " + foodOnTray[i] + " = " + numFoodOnTray[i]);
                }

                // Get all the elements the orders in the menu list
                for (int orderNum = 0; orderNum < orderMenu.transform.childCount; orderNum++)
                {
                    GameObject order = orderMenu.transform.GetChild(orderNum).gameObject;

                    List<string> foodInOrder = new List<string>();
                    List<int> numFoodInOrder = new List<int>();


                    // Run through each order in order menu

                    for (int foodNum = 0; foodNum < order.transform.childCount; foodNum++)
                    {
                        bool foodTypeLogged = false;

                        // Log food

                        // Check if food type is in system
                        if (foodInOrder.Count > 0)
                        {
                            for (int foodType = 0; foodType < foodInOrder.Count; foodType++)
                            {
                                // Food is in system so add
                                if (order.transform.GetChild(foodNum).name == foodInOrder[foodType])
                                {
                                    foodTypeLogged = true;
                                    numFoodInOrder[foodType]++;
                                }
                            }
                        }

                        // If its a new food create add it to database
                        if (!foodTypeLogged)
                        {
                            foodInOrder.Add(order.transform.GetChild(foodNum).name);
                            numFoodInOrder.Add(1);
                        }
                    }

                    // Print all orders
                    print("ORDER " + orderNum + ", SIZE: " + order.transform.childCount);
                    for (int i = 0; i < foodInOrder.Count; i++)
                    {
                        print("Num of " + foodInOrder[i] + " = " + numFoodInOrder[i]);
                    }


                    // Compare order to tray

                    // Check if the order values match those of the tray
                    if (foodOnTray.Count == foodInOrder.Count)
                    {
                        // Yes this varible has a confusing name i am confused and tired just know it works
                        int numOfMatchingRows = 0;

                        // Compare each value
                        for (int j = 0; j < foodOnTray.Count; j++)
                        {
                            // Go through the food on the tray
                            for (int k = 0; k < foodInOrder.Count; k++)
                            {
                                if (foodOnTray[j] == foodInOrder[k] && numFoodOnTray[j] == numFoodInOrder[k])
                                {
                                    numOfMatchingRows++;

                                    print("Same num of " + foodOnTray[j] + " in order and tray = " + numFoodOnTray[j]);
                                }
                            }
                        }

                        // This is where is says it works
                        if (numOfMatchingRows == foodInOrder.Count)
                        {
                            print("U FOUND A MATCH");
                                                        
                            return orderNum;
                        }
                        else
                        {
                            print("NO DICE");
                        }
                    }
                }
            }
        }

        // If codes gotten to this point there was no matches
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
