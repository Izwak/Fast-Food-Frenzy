using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServiceCounter : MonoBehaviour
{
    public Transform orderMenu;
    public Transform customers;

    public OrderManager orderManager;

    public GameObject alert;
    public Slider slider;

    AudioSource bellDing;
    bool hasPlayed;

    public CustomerType customerAtRegister = CustomerType.NONE;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
        bellDing = GetComponent<AudioSource>();
        slider.maxValue = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (customerAtRegister != CustomerType.NONE)
        {
            alert.SetActive(true);
            slider.value += Time.deltaTime;
            if (!hasPlayed)
            {
                bellDing.time = 3.0f;
                bellDing.Play();
                hasPlayed = true;
            }
        }
        else
        {
            alert.SetActive(false);
            hasPlayed = false;
        }

        // Set Colour
        if (slider.value < slider.maxValue / 2) { slider.image.color = Color.Lerp(Color.green, Color.yellow, slider.value / (slider.maxValue / 2)); }
        else { slider.image.color = Color.Lerp(Color.yellow, Color.red, (slider.value - (slider.maxValue / 2)) / (slider.maxValue / 2)); }

        // Customer leaves if bar is full
        if (slider.value >= slider.maxValue)
        {
            CusomerGetsMadAndLeaves();
        }
    }

    void CusomerGetsMadAndLeaves()
    {
        if (customerAtRegister == CustomerType.TAKEAWAY)
        {
            if (customers.childCount > 0)
            {
                bool isCustomerAtCounterYet = false;
                for (int i = 0; i < customers.childCount; i++)
                {
                    CustomerController1 customer = customers.GetChild(i).GetComponent<CustomerController1>();

                    if (customer != null && customer.stage == CustomerStage.ATCOUNTER)
                    {
                        GameManager.score--;
                        slider.value = 0;
                        customer.stage = CustomerStage.LEAVING;
                        customerAtRegister = CustomerType.NONE;
                        isCustomerAtCounterYet = true;
                        break;
                    }
                }
                if (!isCustomerAtCounterYet)
                {
                    for (int i = 0; i < customers.childCount; i++)
                    {
                        CustomerController1 customer = customers.GetChild(i).GetComponent<CustomerController1>();

                        if (customer != null && customer.stage == CustomerStage.INLINE)
                        {
                            GameManager.score--;
                            slider.value = 0;
                            customer.stage = CustomerStage.LEAVING;
                            customerAtRegister = CustomerType.NONE;
                            isCustomerAtCounterYet = true;
                            break;
                        }
                    }
                }
            }
        }
        else if (customerAtRegister == CustomerType.DRIVETHRU)
        {
            if (customers.childCount > 0)
            {
                bool isCustomerAtCounterYet = false;

                for (int i = 0; i < customers.childCount; i++)
                {
                    CarController customer = customers.GetChild(i).GetComponent<CarController>();

                    if (customer != null && customer.stage == CustomerStage.WAITING)
                    {
                        GameManager.score--;
                        slider.value = 0;
                        customer.stage = CustomerStage.LEAVING;
                        customerAtRegister = CustomerType.NONE;
                        isCustomerAtCounterYet = true;
                        break;
                    }
                }
                if (!isCustomerAtCounterYet)
                {
                    for (int i = 0; i < customers.childCount; i++)
                    {
                        CarController customer = customers.GetChild(i).GetComponent<CarController>();

                        if (customer != null && customer.stage == CustomerStage.INLINE)
                        {
                            GameManager.score--;
                            slider.value = 0;
                            customer.stage = CustomerStage.LEAVING;
                            customerAtRegister = CustomerType.NONE;
                            isCustomerAtCounterYet = true;
                            break;
                        }
                    }
                }
            }
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
        slider.value = 0;

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
