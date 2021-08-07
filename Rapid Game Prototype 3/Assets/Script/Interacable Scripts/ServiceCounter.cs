using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceCounter : MonoBehaviour
{
    public Transform orderMenu;

    public OrderManager orderManager;

    public GameObject alert;

    public CustomerType customerAtRegister = CustomerType.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (customerAtRegister != CustomerType.NONE)
        {
            alert.SetActive(true);
        }
        else
        {
            alert.SetActive(false);
        }
    }

    public void AddOrdersToScreen(bool isDriveThru)
    {
        // Create new order and set it to the menu
        GameObject newOrder = orderManager.NewOrderObj();

        newOrder.transform.SetParent(orderMenu.transform);
        newOrder.transform.localPosition = new Vector3(-1050 + 300 * (orderMenu.transform.childCount - 1), 500, 0);
        newOrder.transform.localScale = Vector3.one;

        // Set Order Type
        OrderBehaviour orderBehaviour = newOrder.GetComponent<OrderBehaviour>();
        orderBehaviour.type = customerAtRegister;

        // Reset Register
        customerAtRegister = CustomerType.NONE;

        // Setting pos
        if (orderMenu.transform.childCount > 5)
        {
            for (int i = 0; i < orderMenu.transform.childCount; i++)
            {
                orderMenu.transform.GetChild(i).transform.localPosition = new Vector3(-1050 + (1200.0f / (orderMenu.transform.childCount - 1)) * (i) , 500, 0);
            }
        }
    }
}
