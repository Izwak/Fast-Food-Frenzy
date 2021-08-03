using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviours1 : MonoBehaviour
{
    public GameManager gameManager;
    public bool isRunning = true;

    public GameObject empltySlot;

    public Transform target;

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
        if (isRunning)
        {
            tartgetPoint += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            angle = Mathf.Atan2(tartgetPoint.x, tartgetPoint.y) * Mathf.Rad2Deg;

            if (tartgetPoint.magnitude > speed)
            {
                tartgetPoint = tartgetPoint.normalized * speed;
            }

            body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, body.velocity.y, Input.GetAxis("Vertical") * speed);
            transform.rotation = Quaternion.Euler(0, angle, 0);


            if (Input.GetButtonDown("Fire3"))
            {
                speed = 8;
            }
            if (Input.GetButtonUp("Fire3"))
            {
                speed = 5;
            }

            LookingAtObjects3();

            if (Input.GetButtonDown("Interact") && gameManager.isRunning)
            {
                Interactions();
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

    void DisableOutline(RaycastHit newhit)
    {
        Outline outline = hit.transform.GetComponentInParent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
    }


    void LookingAtObjects3()
    {
        RaycastHit newhit;

        if (Physics.Raycast(transform.position - new Vector3(0, 0.7f, 0), transform.forward, out newhit))
        {
            target.position = newhit.point;

            // Deselect old Object if look new object is different
            if (hit.transform != null)
            {
                GameObject oldObj = hit.transform.parent.gameObject;

                if (!newhit.transform.Equals(hit.transform) && oldObj != null)
                {
                    Outline oldOutline = oldObj.GetComponent<Outline>();
                    Pointer pointer = oldObj.GetComponent<Pointer>();
                    DisplayIcon displayIcon = oldObj.GetComponent<DisplayIcon>();

                    if (oldOutline != null)
                        oldOutline.enabled = false;
                    if (pointer != null)
                        pointer.pointer.gameObject.SetActive(false);
                    if (displayIcon != null)
                        displayIcon.icon.gameObject.SetActive(false);
                }
            }

            Interact obj = newhit.transform.GetComponentInParent<Interact>();

            if (obj != null)
            {
                int holdingNum = empltySlot.transform.childCount;
                int counterHoldingNum = obj.emptySlot.transform.childCount;

                Pointer pointer = newhit.transform.GetComponentInParent<Pointer>();
                DisplayIcon displayIcon = newhit.transform.GetComponentInParent<DisplayIcon>();

                RenderOutline(newhit);

                // If objects have a icon whether or not it's displays
                if (displayIcon != null && newhit.distance < 2)
                {
                    displayIcon.icon.gameObject.SetActive(true);

                    if (holdingNum > 0 || counterHoldingNum > 0)
                    {
                        displayIcon.icon.gameObject.SetActive(false);
                    }
                }

                // Interactable types
                if (obj.type == Interactables.NOTHING)
                {
                    DisableOutline(newhit);
                }
                if (obj.type == Interactables.COUNTER || obj.type == Interactables.PICKUP)
                {
                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Food"))
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.HOTPLATE)
                {
                    if (holdingNum > 0)
                    {
                        if (counterHoldingNum > 0 && obj.emptySlot.transform.GetChild(0).CompareTag("Raw Paddies"))
                        {
                            DisableOutline(newhit);
                        }
                        if (!empltySlot.transform.GetChild(0).CompareTag("Raw Paddies") && !empltySlot.transform.GetChild(0).CompareTag("Cooked Paddies") &&
                            !empltySlot.transform.GetChild(0).CompareTag("Burnt Paddies"))
                        {
                            DisableOutline(newhit);
                        }
                        if (empltySlot.transform.GetChild(0).CompareTag("Raw Paddies") || empltySlot.transform.GetChild(0).CompareTag("Cooked Paddies") ||
                            empltySlot.transform.GetChild(0).CompareTag("Burnt Paddies"))
                        {
                            if (counterHoldingNum > 0)
                            {
                                if (empltySlot.transform.GetChild(0).tag != obj.emptySlot.transform.GetChild(0).tag)
                                {
                                    DisableOutline(newhit);
                                }
                                else if (holdingNum >= 2)
                                {
                                    DisableOutline(newhit);
                                }
                            }
                            else if (!empltySlot.transform.GetChild(0).CompareTag("Raw Paddies"))
                            {
                                DisableOutline(newhit);
                            }
                        }
                    }
                }
                if (obj.type == Interactables.FRIDGE)
                {
                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Raw Paddies"))
                    {
                        DisableOutline(newhit);

                    }

                    if (holdingNum >= 2)
                    {
                        DisableOutline(newhit);
                    }

                    /*if (holdingNum >= 2 || (holdingNum >= 1 && !obj.createObject.gameObject.CompareTag("Raw Paddies")))
                    {
                        DisableOutline(newhit);
                    }*/
                }
                if (obj.type == Interactables.SERVICECOUNTER)
                {
                    if (holdingNum > 0)
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.BURGERSTATION)
                {
                    if (holdingNum > 0)
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.FRYSTATION)
                {
                    FryStation fryStation = newhit.transform.GetComponentInParent<FryStation>();

                    if (fryStation != null)
                    {
                        if (fryStation.fryLvl > 1 && holdingNum > 0)
                        {
                            DisableOutline(newhit);
                        }
                    }

                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Cooked Fries"))
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.FRIER)
                {
                    Frier frier = newhit.transform.GetComponentInParent<Frier>();

                    if (frier != null)
                    {
                        if (frier.isFull() && holdingNum > 0)
                        {
                            DisableOutline(newhit);
                        }
                    }

                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Raw Fries"))
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.HEATER)
                {
                    if (counterHoldingNum >= 3)
                    {
                        DisableOutline(newhit);
                    }
                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Cooked Paddies"))
                    {
                        DisableOutline(newhit);
                    }
                }
            }
        }
        // If i do see anything deselect last object
        else
        {
            // Deselect old Object if not looking at any object
            if (hit.transform != null)
            {
                Outline oldOutline = hit.transform.GetComponentInParent<Outline>();
                if (oldOutline != null)
                {
                    oldOutline.enabled = false;
                }
            }
        }
    }

    void LookingAtObjects2()
    {
        RaycastHit newhit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(transform.position - new Vector3(0, 0.7f, 0), transform.forward, out newhit))
        if (Physics.Raycast(ray, out newhit, 1000))
        {
            //if (newhit != null)
                print(newhit.transform.name);
            target.position = newhit.point;

            // Deselect old Object if look new object is different
            if (hit.transform != null)
            {
                GameObject oldObj = hit.transform.parent.gameObject;

                if (!newhit.transform.Equals(hit.transform) && oldObj != null)
                {
                    Outline oldOutline = oldObj.GetComponent<Outline>();
                    Pointer pointer = oldObj.GetComponent<Pointer>();
                    DisplayIcon displayIcon = oldObj.GetComponent<DisplayIcon>();

                    if (oldOutline != null)
                        oldOutline.enabled = false;
                    if (pointer != null)
                        pointer.pointer.gameObject.SetActive(false);
                    if (displayIcon != null)
                        displayIcon.icon.gameObject.SetActive(false);
                }
            }

            Interact obj = newhit.transform.GetComponentInParent<Interact>();

            if (obj != null)
            {
                int holdingNum = empltySlot.transform.childCount;
                int counterHoldingNum = obj.emptySlot.transform.childCount;

                Pointer pointer = newhit.transform.GetComponentInParent<Pointer>();
                DisplayIcon displayIcon = newhit.transform.GetComponentInParent<DisplayIcon>();

                RenderOutline(newhit);

                // If objects have a icon whether or not it's displays
                if (displayIcon != null && newhit.distance < 2)
                {
                    displayIcon.icon.gameObject.SetActive(true);

                    if (holdingNum > 0 || counterHoldingNum > 0)
                    {
                        displayIcon.icon.gameObject.SetActive(false);
                    }
                }

                // Interactable types
                if (obj.type == Interactables.NOTHING)
                {
                    DisableOutline(newhit);
                }
                if (obj.type == Interactables.COUNTER || obj.type == Interactables.PICKUP)
                {
                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Food"))
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.HOTPLATE)
                {
                    if (holdingNum > 0)
                    {
                        if (counterHoldingNum > 0 && obj.emptySlot.transform.GetChild(0).CompareTag("Raw Paddies"))
                        {
                            DisableOutline(newhit);
                        }
                        if (!empltySlot.transform.GetChild(0).CompareTag("Raw Paddies") && !empltySlot.transform.GetChild(0).CompareTag("Cooked Paddies") && 
                            !empltySlot.transform.GetChild(0).CompareTag("Burnt Paddies"))
                        {
                            DisableOutline(newhit);
                        }
                        if (empltySlot.transform.GetChild(0).CompareTag("Raw Paddies") || empltySlot.transform.GetChild(0).CompareTag("Cooked Paddies") || 
                            empltySlot.transform.GetChild(0).CompareTag("Burnt Paddies"))
                        {
                            if (counterHoldingNum > 0)
                            {
                                if (empltySlot.transform.GetChild(0).tag != obj.emptySlot.transform.GetChild(0).tag)
                                {
                                    DisableOutline(newhit);
                                }
                                else if (holdingNum >= 2)
                                {
                                    DisableOutline(newhit);
                                }
                            }
                            else if (!empltySlot.transform.GetChild(0).CompareTag("Raw Paddies"))
                            {
                                DisableOutline(newhit);
                            }
                        }
                    }
                }
                if (obj.type == Interactables.FRIDGE)
                {
                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Raw Paddies"))
                    {
                        DisableOutline(newhit);

                    }

                    if (holdingNum >= 2)
                    {
                        DisableOutline(newhit);
                    }

                    /*if (holdingNum >= 2 || (holdingNum >= 1 && !obj.createObject.gameObject.CompareTag("Raw Paddies")))
                    {
                        DisableOutline(newhit);
                    }*/
                }
                if (obj.type == Interactables.SERVICECOUNTER)
                {
                    if (holdingNum > 0)
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.BURGERSTATION)
                {
                    if (holdingNum > 0)
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.FRYSTATION)
                {
                    FryStation fryStation = newhit.transform.GetComponentInParent<FryStation>();

                    if (fryStation != null)
                    {
                        if (fryStation.fryLvl > 1 && holdingNum > 0)
                        {
                            DisableOutline(newhit);
                        }
                    }

                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Cooked Fries"))
                    {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.FRIER)
                {
                    Frier frier = newhit.transform.GetComponentInParent<Frier>();

                    if (frier != null)
                    {
                        if (frier.isFull() && holdingNum > 0)
                        {
                            DisableOutline(newhit);
                        }
                    }

                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Raw Fries")) {
                        DisableOutline(newhit);
                    }
                }
                if (obj.type == Interactables.HEATER)
                {
                    if (counterHoldingNum >= 3)
                    {
                        DisableOutline(newhit);
                    }
                    if (holdingNum > 0 && !empltySlot.transform.GetChild(0).CompareTag("Cooked Paddies"))
                    {
                        DisableOutline(newhit);
                    }
                }
            }
        }
        // If i do see anything deselect last object
        else
        {
            // Deselect old Object if not looking at any object
            if (hit.transform != null)
            {
                Outline oldOutline = hit.transform.GetComponentInParent<Outline>();
                if (oldOutline != null)
                {
                    oldOutline.enabled = false;
                }
            }
        }
    }

    void Interactions()
    {
        // Swap items In and out of counters

        RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit, 100))

        // Check if looking at an object
        if (Physics.Raycast(body.position - new Vector3(0, 0.7f, 0), transform.forward, out hit))
        //if (Physics.Raycast(ray, out hit, 100))
        {
            //Debug.Log(hit.transform.name);
            //Debug.Log("hit");
            // Check if it's in reach
            if (hit.distance < 2)
            {
                // Check if it's a Interactable
                Interact obj = hit.transform.GetComponentInParent<Interact>();

                if (obj != null)
                {
                    int holdingNum = empltySlot.transform.childCount;
                    int counterHoldingNum = obj.emptySlot.transform.childCount;

                    Pointer pointer = obj.GetComponent<Pointer>();

                    if (pointer != null && (holdingNum == 0 && counterHoldingNum == 0))
                    {
                        pointer.pointer.gameObject.SetActive(true);
                    }

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
                            GameManager.score -= 2;
                            Destroy(empltySlot.transform.GetChild(holdingNum - 1).gameObject);
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
                                    if (frier.traySlots[i].transform.childCount > 0 && !frier.traySlots[i].transform.GetChild(0).CompareTag("Raw Fries"))
                                    {
                                        GameObject friedFries = frier.traySlots[i].transform.GetChild(0).gameObject;

                                        friedFries.transform.SetParent(empltySlot.transform);
                                        friedFries.transform.localPosition = Vector3.zero;
                                        friedFries.transform.localRotation = Quaternion.identity;

                                        Cooking cooking = friedFries.GetComponent<Cooking>();

                                        if (friedFries.CompareTag("Cooked Fries") && cooking != null) {
                                            cooking.beingCooked = false;
                                        }

                                        if (pointer != null)
                                        {
                                            pointer.pointer.gameObject.SetActive(false);
                                        }
                                        break;
                                    }
                                }
                            }
                            // Puting in the frier
                            else if (holdingNum > 0)
                            {
                                GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;
                                Cooking cooking = empltySlot.transform.GetChild(0).GetComponent<Cooking>();

                                if (playersObject.CompareTag("Raw Fries") && cooking != null)
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        if (frier.traySlots[i].transform.childCount == 0)
                                        {
                                            playersObject.transform.SetParent(frier.traySlots[i].transform);
                                            playersObject.transform.localPosition = Vector3.zero;
                                            playersObject.transform.localRotation = Quaternion.identity;

                                                cooking.beingCooked = true;

                                            break;
                                        }
                                    }
                                }
                            }
                        }


                        /*Frier frier = obj.GetComponent<Frier>();

                        print(holdingNum);

                        if (frier != null)
                        {
                            // Take Fries out of frier
                            if (holdingNum == 0)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (frier.traySlots[i].transform.childCount == 0)
                                    {
                                        // Create new fry tray
                                        GameObject newFryTray = Instantiate(obj.createObject);
                                        newFryTray.transform.SetParent(empltySlot.transform);
                                        newFryTray.transform.localPosition = Vector3.zero;
                                        newFryTray.transform.forward = transform.forward;

                                        if (pointer != null)
                                        {
                                            pointer.pointer.gameObject.SetActive(false);
                                        }
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
                        }*/

                        if (pointer != null)
                        {
                            if (!frier.isEmpty())
                            {
                                pointer.pointer.gameObject.SetActive(false);
                            }
                        }
                    }

                    else if (obj.type == Interactables.FRYSTATION)
                    {
                        FryStation fryStation = obj.GetComponent<FryStation>();

                        if (fryStation != null)
                        {
                            // Put fry in station
                            if (holdingNum > 0)
                            {
                                GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;

                                if (playersObject.CompareTag("Cooked Fries"))
                                {
                                    if (fryStation.fryLvl <= 1)
                                    {
                                        fryStation.fryLvl++;
                                        Destroy(playersObject);
                                    }
                                }
                            }
                            // Take fries in station
                            else
                            {
                                if (fryStation.fryLvl > 0)
                                {
                                    GameObject newFries = Instantiate(obj.createObject);
                                    newFries.transform.SetParent(empltySlot.transform);
                                    newFries.transform.localPosition = Vector3.zero;
                                    newFries.transform.forward = transform.forward;


                                    fryStation.fryLvl -= 0.25f;

                                    if (pointer != null)
                                    {
                                        pointer.pointer.gameObject.SetActive(false);
                                    }
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
                                food.transform.localPosition = Vector3.zero;
                            else
                                food.transform.localPosition = Vector3.zero;

                            food.transform.forward = transform.forward;
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

                        if (holdingNum == 0 && burgerStation != null)
                        {
                            if (burgerStation.paddyHeater.childCount > 0)
                            {
                                GameObject newBurger = Instantiate(obj.createObject);
                                newBurger.transform.SetParent(empltySlot.transform);
                                newBurger.transform.localPosition = Vector3.zero;
                                newBurger.transform.forward = transform.forward;

                                burgerStation.TakePaddy();
                                isRunning = false;

                                if (pointer != null)
                                {
                                    pointer.pointer.gameObject.SetActive(false);
                                }
                            }
                        }
                    }

                    else if (obj.type == Interactables.HOTPLATE)
                    {
                        // Put paddys on hotplate
                        if (holdingNum > 0 && counterHoldingNum == 0)
                        {
                            GameObject paddy = empltySlot.transform.GetChild(holdingNum -1).gameObject;
                            if (paddy.CompareTag("Raw Paddies"))
                            {
                                paddy.transform.SetParent(obj.emptySlot.transform);
                                paddy.transform.localPosition = Vector3.zero;
                                paddy.transform.localRotation = Quaternion.identity;

                                Cooking cooking = paddy.GetComponent<Cooking>();

                                if (cooking != null)
                                {
                                    cooking.beingCooked = true;
                                }
                            }
                        }

                        // Take paddys off hotplate
                        else if (counterHoldingNum > 0 && holdingNum == 0 && !obj.emptySlot.transform.GetChild(0).CompareTag("Raw Paddies"))
                        {
                            Transform cookedPaddy = obj.emptySlot.transform.GetChild(0).transform;
                            cookedPaddy.SetParent(empltySlot.transform);
                            cookedPaddy.localPosition = Vector3.zero;
                            cookedPaddy.localRotation = Quaternion.identity;

                            Cooking cooking = cookedPaddy.GetComponent<Cooking>();

                            if (cooking != null)
                            {
                                cooking.beingCooked = false;
                            }
                        }

                        // Can grab 2 paddies at a time
                        else if (counterHoldingNum > 0 && holdingNum == 1)
                        {
                            if (empltySlot.transform.GetChild(0).CompareTag("Cooked Paddies") || empltySlot.transform.GetChild(0).CompareTag("Burnt Paddies"))
                            {
                                Transform cookedPaddy = obj.emptySlot.transform.GetChild(0).transform;
                                cookedPaddy.SetParent(empltySlot.transform);
                                cookedPaddy.localPosition = new Vector3(0, 0.1f, 0);
                                cookedPaddy.localRotation = Quaternion.identity;

                                Cooking cooking = cookedPaddy.GetComponent<Cooking>();

                                if (cooking != null)
                                {
                                    cooking.beingCooked = false;
                                }
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

                        if (service != null && holdingNum == 0 && service.umHelloImACustomer)
                        {
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
                                playersObject.transform.SetParent(obj.emptySlot.transform);
                                playersObject.transform.localPosition = Vector3.zero;
                                playersObject.transform.localRotation = Quaternion.identity;

                                // Check for food to match orders
                                for (int i = 0; i < pickUp.orderMenu.transform.childCount; i++)
                                {
                                    GameObject order = pickUp.orderMenu.transform.GetChild(i).gameObject;

                                    // If an order matches ur food
                                    if (playersObject.name == order.name && pickUp.CustomerParent.childCount > 0)
                                    {
                                        // Search for valid customer
                                        for (int j = 0; j < pickUp.CustomerParent.childCount; j++)
                                        {
                                            GameObject customer = pickUp.CustomerParent.GetChild(j).gameObject;
                                            CustomerController1 customerController = customer.GetComponent<CustomerController1>();

                                            if (customerController != null && customerController.stage == CustomerStage.WAITING)
                                            {
                                                customerController.stage = CustomerStage.PICKUP;
                                                customerController.pointsOfInterest.Add(obj.gameObject);
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }

                        // Take object from counter
                        else if (counterHoldingNum > 0 && holdingNum == 0)
                        {
                            GameObject counterObject = obj.emptySlot.transform.GetChild(0).gameObject;
                            counterObject.transform.SetParent(empltySlot.transform);
                            counterObject.transform.localPosition = Vector3.zero;
                            counterObject.transform.localRotation = Quaternion.identity;
                        }

                    }

                    else if (obj.type == Interactables.WINDOWCOUNTER)
                    {
                        PickUp pickUp = obj.GetComponent<PickUp>();

                        // Put object on counter
                        if (holdingNum > 0 && counterHoldingNum == 0 && pickUp != null)
                        {
                            GameObject playersObject = empltySlot.transform.GetChild(0).gameObject;

                            // Can put objects on counters if they're a food
                            if (playersObject.CompareTag("Food"))
                            {
                                playersObject.transform.SetParent(obj.emptySlot.transform);
                                playersObject.transform.localPosition = Vector3.zero;
                                playersObject.transform.localRotation = Quaternion.identity;

                                // Check for food to match orders
                                for (int i = 0; i < pickUp.orderMenu.transform.childCount; i++)
                                {
                                    GameObject order = pickUp.orderMenu.transform.GetChild(i).gameObject;

                                    // If an order matches ur food
                                    if (playersObject.name == order.name && pickUp.CustomerParent.childCount > 0)
                                    {
                                        // Search for valid customer
                                        for (int j = 0; j < pickUp.CustomerParent.childCount; j++)
                                        {
                                            GameObject customer = pickUp.CustomerParent.GetChild(j).gameObject;
                                            CarController carController = customer.GetComponent<CarController>();

                                            if (carController != null && carController.stage == CustomerStage.WAITING)
                                            {
                                                carController.stage = CustomerStage.PICKUP;
                                                carController.pointsOfInterest.Add(obj.gameObject);
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }

                        // Take object from counter
                        else if (counterHoldingNum > 0 && holdingNum == 0)
                        {
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
