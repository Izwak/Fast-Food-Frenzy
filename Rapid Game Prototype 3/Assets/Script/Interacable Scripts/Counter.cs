using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameObject emptySlot;
    public GameObject item;
    public GameObject counterTopper;

    bool timer = false;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (emptySlot.transform.childCount > 0)
        {
            emptySlot.transform.GetChild(0).localPosition = Vector3.zero;
        }
        if (timer)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timer = false;
                gameObject.transform.GetComponent<TMP_Text>().text = "";
            }
        }

        //check if counter has an item on it (checks if EmptySlot has a child)
        if (gameObject.transform.childCount > 1)
        {
            if (gameObject.transform.GetChild(1).childCount > 0)
            {
                item = gameObject.transform.GetChild(1).GetChild(0).transform.gameObject;
                if (item.CompareTag("Patty"))
                {

                }
            }
        }
    }

    void DoTimer(float seconds)
    {
        if (!timer)
        {
            timer = true;
            timeLeft = seconds;
        }
    }

    void AddTime(float s)
    {
        if (timer)
            timeLeft += s;
    }
}
