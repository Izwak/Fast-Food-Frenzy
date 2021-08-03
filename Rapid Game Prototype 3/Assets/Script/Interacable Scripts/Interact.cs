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
    WINDOWCOUNTER,
}
public class Interact : MonoBehaviour
{
    public GameObject emptySlot;
    public GameObject createObject;
    public Interactables type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (emptySlot.transform.childCount > 0)
        {
            emptySlot.transform.GetChild(0).transform.localPosition = Vector3.zero;
        }
    }
}
