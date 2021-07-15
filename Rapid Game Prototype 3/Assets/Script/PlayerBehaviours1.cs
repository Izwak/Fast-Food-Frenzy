using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviours1 : MonoBehaviour
{
    public GameObject empltySlot;

    Rigidbody body;

    float speed = 5;
    float angle;

    RaycastHit hit;

    Vector2 tartgetPoint = new Vector2(0, -1);

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tartgetPoint += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        angle = Mathf.Atan2(tartgetPoint.x, tartgetPoint.y) * Mathf.Rad2Deg;

        if (tartgetPoint.magnitude > speed)
        {
            tartgetPoint = tartgetPoint.normalized * speed;
        }

        body.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        transform.rotation = Quaternion.Euler(0, angle, 0);

        //GameManager.Instance.test;

        //OutlineCounter();
        ShadeInteracatbles();

        if (Input.GetButtonDown("Interact"))
        {
            Interactions();
        }

    }

    void ShadeInteracatbles()
    {
        RaycastHit newhit;

        if (Physics.Raycast(transform.position - new Vector3(0, 1, 0), transform.forward, out newhit))
        {
            Interact obj = newhit.transform.GetComponentInParent<Interact>();

            if (obj != null)
            {
                int holdingNum = empltySlot.transform.childCount;
                int counterHoldingNum = obj.emptySlot.transform.childCount;

                if (hit.transform != null)
                {
                    if (!newhit.Equals(hit))
                    {
                        Outline oldOutline = hit.transform.GetComponentInParent<Outline>();
                        if (oldOutline != null)
                        {
                            oldOutline.enabled = false;
                        }
                    }
                }


                if (obj.type == Interactables.BIN)
                {
                    RenderOutline(newhit);
                }
                else if (holdingNum > 0)
                {

                    GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;

                    if (playersObject.CompareTag("Raw Fries") && obj.type == Interactables.FRIER)
                    {
                        Frier frier = obj.GetComponent<Frier>();
                        if (frier != null)
                        {
                            if (!frier.isFull)
                            RenderOutline(newhit);
                        }
                    }

                    else if (playersObject.CompareTag("Fry Tray") && obj.type == Interactables.FRYSTATION)
                    {
                        FryStation fryStation = obj.GetComponent<FryStation>();

                        if (fryStation != null)
                        {
                            if  (fryStation.fryLvl <= 1)
                            {
                                RenderOutline(newhit);
                            }
                        }
                    }

                    else if(playersObject.CompareTag("Cooked Paddies") && obj.type == Interactables.HEATER)
                    {
                        RenderOutline(newhit);
                    }

                    else if ((playersObject.CompareTag("Food")) && (obj.type == Interactables.COUNTER || obj.type == Interactables.PICKUP))
                    {
                        RenderOutline(newhit);
                    }

                    else if (obj.type == Interactables.HOTPLATE)
                    {
                        RenderOutline(newhit);
                    }
                }

                // If not holding anything and any of these things
                else if (obj.type == Interactables.COUNTER || obj.type == Interactables.FRIDGE || obj.type == Interactables.SERVICECOUNTER 
                    || obj.type == Interactables.BURGERSTATION || obj.type == Interactables.HOTPLATE || obj.type == Interactables.PICKUP)
                {
                    RenderOutline(newhit);
                }

                else if (obj.type == Interactables.FRIER)
                {
                    Frier frier = obj.GetComponent<Frier>();
                    
                    // Check if you can interact with frier
                    if (frier != null)
                    {
                        if (!frier.isEmpty)
                            RenderOutline(newhit);
                    }
                }

                else if (obj.type == Interactables.FRYSTATION)
                {
                    FryStation fryStation = obj.GetComponent<FryStation>();

                    // Check if you can interact with frier
                    if (fryStation != null)
                    {
                        if (fryStation.fryLvl > 0)
                            RenderOutline(newhit);
                    }
                }
            }
        }
    }

    void RenderOutline(RaycastHit newhit)
    {
        if (hit.transform != null)
        {
            if (!newhit.Equals(hit))
            {
                Outline oldOutline = hit.transform.GetComponentInParent<Outline>();
                if (oldOutline != null)
                {
                    oldOutline.enabled = false;
                }
            }
        }

        hit = newhit;


        Outline outline = hit.transform.GetComponentInParent<Outline>();
        if (outline != null)
        {
            if (hit.distance < 2)
            {
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }
        }
    }

    void Interactions()
    {
        // Swap items In and out of counters

        RaycastHit hit;

        // Check if looking at an object
        if (Physics.Raycast(body.position - new Vector3(0, 1, 0), transform.forward, out hit))
        {
            // Check if it's in reach
            if (hit.distance < 2)
            {
                // Check if it's a counter
                Interact obj = hit.transform.GetComponentInParent<Interact>();

                if (obj != null)
                {
                    int holdingNum = empltySlot.transform.childCount;
                    int counterHoldingNum = obj.emptySlot.transform.childCount;

                    if (obj.type == Interactables.COUNTER )
                    {
                        // Swap object from hand to counter
                        if (holdingNum > 0 && counterHoldingNum == 0)
                        {

                            GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;

                            // Can put objects on counters if they're a food
                            if (playersObject.CompareTag("Food"))
                            {
                                print("swap hand with counter");
                                playersObject.transform.SetParent(obj.emptySlot.transform);
                                playersObject.transform.localPosition = Vector3.zero;
                                playersObject.transform.localRotation = Quaternion.identity;
                            }

                        }

                        // Swap object from counter to hand
                        else if (counterHoldingNum > 0 && holdingNum == 0)
                        {
                            print("swap counter with hand");

                            GameObject counterObject = obj.emptySlot.transform.GetChild(0).gameObject;
                            counterObject.transform.SetParent(empltySlot.transform);
                            counterObject.transform.localPosition = Vector3.zero;
                            counterObject.transform.localRotation = Quaternion.identity;
                        }
                    }

                    else if (obj.type == Interactables.BIN)
                    {
                        // Checks that you have something to discard
                        if (holdingNum > 0)
                        {
                            // Discard item in bin
                            GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;
                            playersObject.transform.SetParent(obj.emptySlot.transform);
                            playersObject.SetActive(false);
                        }
                    }

                    else if (obj.type == Interactables.FRIER)
                    {
                        Frier frier = obj.GetComponent<Frier>();

                        if (frier != null)
                        {
                            // Take Fries out of frier
                            if (holdingNum == 0)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (frier.spotTaken[i])
                                    {
                                        // Create new fry tray
                                        GameObject newFryTray = Instantiate(obj.createObject);
                                        newFryTray.transform.SetParent(empltySlot.transform);
                                        newFryTray.transform.localPosition = Vector3.zero;
                                        newFryTray.transform.forward = transform.forward;
                                        frier.spotTaken[i] = false;
                                        break;
                                    }
                                }
                            }
                            // Put fries in frier
                            else if (holdingNum > 0)
                            {
                                GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;

                                // Can put objects on counters if they're a food
                                if (playersObject.CompareTag("Raw Fries"))
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        if (!frier.spotTaken[i])
                                        {
                                            frier.spotTaken[i] = true;
                                            Destroy(playersObject);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (obj.type == Interactables.FRYSTATION)
                    {
                        FryStation fryStation = obj.GetComponent<FryStation>();

                        if (fryStation != null)
                        {
                            if (holdingNum > 0)
                            {
                                GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;

                                if (playersObject.CompareTag("Fry Tray"))
                                {
                                    if (fryStation.fryLvl <= 1)
                                    {
                                        fryStation.fryLvl++;
                                        Destroy(playersObject);
                                    }
                                }
                            }
                            else
                            {
                                if (fryStation.fryLvl > 0)
                                {
                                    GameObject newFries = Instantiate(obj.createObject);
                                    newFries.transform.SetParent(empltySlot.transform);
                                    newFries.transform.localPosition = Vector3.zero;
                                    newFries.transform.forward = transform.forward;

                                    fryStation.fryLvl -= 0.25f;
                                }
                            }
                        }
                    }

                    else if (obj.type == Interactables.FRIDGE)
                    {
                        if (holdingNum == 0)
                        {
                            GameObject food = Instantiate(obj.createObject);
                            food.transform.SetParent(empltySlot.transform);

                            if (food.CompareTag("Raw Fries"))
                                food.transform.localPosition = new Vector3(0.3f, -0.1f, -0.2f);
                            else
                                food.transform.localPosition = Vector3.zero;

                            food.transform.forward = transform.right;
                        }
                        else if (holdingNum == 1 && obj.createObject.CompareTag("Raw Paddies"))
                        {
                            if (empltySlot.transform.GetChild(0).CompareTag("Raw Paddies") )
                            {

                                GameObject food = Instantiate(obj.createObject);
                                food.transform.SetParent(empltySlot.transform);
                                food.transform.localPosition = new Vector3(0, 0.1f, 0);
                                food.transform.localRotation = Quaternion.identity;
                            }
                        }
                    }
                    
                    else if (obj.type == Interactables.BURGERSTATION)
                    {
                        BurgerStation burgerStation = obj.GetComponent<BurgerStation>();

                        if (holdingNum == 0 && burgerStation != null )
                        {
                            if (burgerStation.paddyHeater.childCount > 0)
                            {
                                GameObject newBurger = Instantiate(obj.createObject);
                                newBurger.transform.SetParent(empltySlot.transform);
                                newBurger.transform.localPosition = Vector3.zero;
                                newBurger.transform.forward = transform.forward;

                                burgerStation.TakePaddy();
                            }
                        }
                    }

                    else if (obj.type == Interactables.HOTPLATE)
                    {
                        if (holdingNum > 0 && counterHoldingNum == 0)
                        {
                            GameObject paddy = empltySlot.transform.GetChild(0).gameObject;
                            if (paddy.CompareTag("Raw Paddies"))
                            {
                                Destroy(paddy);

                                GameObject cookedPaddy = Instantiate(obj.createObject);
                                cookedPaddy.transform.SetParent(obj.emptySlot.transform);
                                cookedPaddy.transform.localPosition = Vector3.zero;
                                cookedPaddy.transform.rotation = Quaternion.identity;
                            }
                        }

                        else if (counterHoldingNum > 0 && holdingNum == 0)
                        {
                            Transform cookedPaddy = obj.emptySlot.transform.GetChild(0).transform;
                            cookedPaddy.SetParent(empltySlot.transform);
                            cookedPaddy.localPosition = Vector3.zero;
                            cookedPaddy.localRotation = Quaternion.identity;
                        }
                        // Can grab 2 paddies at a time
                        else if (counterHoldingNum > 0 && holdingNum == 1)
                        {
                            if (empltySlot.transform.GetChild(0).CompareTag("Cooked Paddies"))
                            {
                                Transform cookedPaddy = obj.emptySlot.transform.GetChild(0).transform;
                                cookedPaddy.SetParent(empltySlot.transform);
                                cookedPaddy.localPosition = new Vector3(0, 0.1f, 0);
                                cookedPaddy.localRotation = Quaternion.identity;
                            }
                        }
                    }

                    else if (obj.type == Interactables.HEATER)
                    {
                        if (holdingNum > 0 && counterHoldingNum < 3)
                        {
                            GameObject paddy = empltySlot.transform.GetChild(0).gameObject;

                            if (paddy.CompareTag("Cooked Paddies"))
                            {
                                paddy.transform.SetParent(obj.emptySlot.transform);
                                paddy.transform.localRotation = Quaternion.identity;


                                paddy.transform.localPosition = new Vector3(0, counterHoldingNum * 0.3f, 0);
                            }
                        }
                    }

                    else if (obj.type == Interactables.SERVICECOUNTER)
                    {
                        ServiceCounter service = obj.GetComponent<ServiceCounter>();

                        if (service != null)
                        {
                            OrderMechanics.CreateNewOrder();

                            service.AddOrdersToScreen();
                        }
                    }

                    else if (obj.type == Interactables.PICKUP)
                    {
                        PickUp pickUp = obj.GetComponent<PickUp>();

                        // Put object on counter
                        if (holdingNum > 0 && counterHoldingNum == 0 && pickUp != null)
                        {

                            GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;

                            // Can put objects on counters if they're a food
                            if (playersObject.CompareTag("Food"))
                            {
                                print("swap hand with counter");
                                playersObject.transform.SetParent(obj.emptySlot.transform);
                                playersObject.transform.localPosition = Vector3.zero;
                                playersObject.transform.localRotation = Quaternion.identity;

                                for (int i = 0; i < pickUp.canvas.transform.childCount; i++)
                                {
                                    GameObject order = pickUp.canvas.transform.GetChild(i).gameObject;

                                    if (playersObject.name == order.name)
                                    {
                                        print("Success");

                                        pickUp.RemoveDisplayOrder(i);
                                        OrderMechanics.orders.RemoveAt(i);
                                        break;
                                    }
                                }
                                /*for (int i = 0; i < OrderMechanics.orders.Count; i++)
                                {
                                    if (playersObject.name == OrderMechanics.orders[i])
                                    {
                                        print("Success");

                                        OrderMechanics.orders.RemoveAt(0);
                                        pickUp.RemoveDisplayOrder(0);
                                        break;
                                    }
                                }*/
                            }
                        }

                        // Take object from counter
                        else if (counterHoldingNum > 0 && holdingNum == 0)
                        {
                            print("swap counter with hand");

                            GameObject counterObject = obj.emptySlot.transform.GetChild(0).gameObject;
                            counterObject.transform.SetParent(empltySlot.transform);
                            counterObject.transform.localPosition = Vector3.zero;
                            counterObject.transform.localRotation = Quaternion.identity;
                        }
                    }
                }
            }
        }
    }
}
