using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Interactables
{
    COUNTER,
    BIN,
    FRIER,
    BURGERSTATION,
    SERVICECOUNTER,
    FRYSTATION,
    FRIDGE,
    HOTPLATE,
    HEATER,
    NOTHING,
    PICKUP,
    WINDOWPICKUP,
    TRAYDISPENCER,
}
public class Interact : MonoBehaviour
{
    public GameObject emptySlot;
    public GameObject createObject;
    public Interactables type;

    // Start is called before the first frame update
    void Start()
    {
        // Edit THIS IS THE 1
    }

    // Update is called once per frame
    void Update()
    {
        if (emptySlot.transform.childCount > 0)
        {
            // IDK WAT THIS WOULD BE USED FOR?
            //emptySlot.transform.GetChild(0).transform.localPosition = Vector3.zero;
        }
    }

    public int HoldingNum()
    {
        return emptySlot.transform.childCount;
    }
}
