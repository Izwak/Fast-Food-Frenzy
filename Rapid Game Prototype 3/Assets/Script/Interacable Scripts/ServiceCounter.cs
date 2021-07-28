using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceCounter : MonoBehaviour
{
    public Transform orderMenu;

    public OrderManager orderManager;

    public GameObject alert;

    public bool umHelloImACustomer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (umHelloImACustomer)
        {
            alert.SetActive(true);
        }
        else
        {
            alert.SetActive(false);
        }
    }

    public void AddOrdersToScreen()
    {
        GameObject newOrder = Instantiate(orderManager.NewOrderObj(), orderMenu.transform);

        newOrder.transform.localPosition = new Vector3(-1000 + 300 * (orderMenu.transform.childCount - 1), 500, 0);

        umHelloImACustomer = false;

        if (orderMenu.transform.childCount > 7)
        {
            for (int i = 0; i < orderMenu.transform.childCount; i++)
            {
                orderMenu.transform.GetChild(i).transform.localPosition = new Vector3(-1000 + (2000.0f / (orderMenu.transform.childCount - 1)) * (i) , 500, 0);
            }
        }
    }
}
