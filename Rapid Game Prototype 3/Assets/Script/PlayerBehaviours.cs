using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviours : MonoBehaviour
{
    public GameObject empltySlot;
    Transform slotPos;

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

        //Debug.Log("target " + tartgetPoint + " angle " + angle + " mag " + tartgetPoint.magnitude);

        body.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        transform.rotation = Quaternion.Euler(0, angle, 0);

        OutlineCounter();

        if (empltySlot.transform.childCount > 0)
        {
            empltySlot.transform.GetChild(0).localPosition = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractCounter();
        }

    }

    void OutlineCounter()
    {
        RaycastHit newhit;


        if (Physics.Raycast(transform.position - new Vector3(0, 1, 0), transform.forward, out newhit))
        {
            if (hit.transform != null)
            { 
                if (!newhit.Equals(hit))
                {
                    Outline oldOutline = hit.transform.GetComponent<Outline>();
                    if (oldOutline != null)
                    {
                        oldOutline.enabled = false;
                        //oldOutline.OutlineWidth = 0;
                    }
                }
            }

            hit = newhit;
            TMP_Text textObj = null;
            if (hit.transform.GetComponentInChildren<TMP_Text>() != null)
                textObj = hit.transform.GetComponentInChildren<TMP_Text>();

            Outline outline = hit.transform.GetComponent<Outline>();
            if (outline != null) {
                if (hit.distance < 2)
                {
                    outline.enabled = true;
                    textObj.text = "yee";
                    //outline.OutlineWidth = 4;
                }
                else
                {
                    outline.enabled = false;
                    textObj.text = "noo";
                    //outline.OutlineWidth = 0;
                }
            }
        }
    }

    void InteractCounter()
    {
        // Swap items In and out of counters

        RaycastHit hit;

        Interact counter;

        // Check if looking at an object
        if (Physics.Raycast(transform.position - new Vector3(0, 1, 0), transform.forward, out hit))
        {
            // Check if it's in reach
            if (hit.distance < 2)
            {
                // Check if it's a counter
                counter = hit.transform.GetComponent<Interact>();

                if (counter != null)
                {
                    int children = empltySlot.transform.childCount;
                    int counterChildren = counter.emptySlot.transform.childCount;


                    //GameObject playerObject = empltySlot.transform.GetChild(0).gameObject;
                    //GameObject counterObject = counter.emptySlot.transform.GetChild(0).gameObject;

                    // Swap object from hand to counter
                    if (children > 0 && counterChildren == 0)
                    {
                        print("swap hand with counter");

                        GameObject playerObject = empltySlot.transform.GetChild(0).gameObject;

                        playerObject.transform.SetParent(counter.emptySlot.transform);
                    }
                    // Swap object from counter to hand
                    else if (counterChildren > 0 && children == 0)
                    {
                        print("swap counter with hand");

                        GameObject counterObject = counter.emptySlot.transform.GetChild(0).gameObject;
                        counterObject.transform.SetParent(empltySlot.transform);
                    }
                }
            }
        }

    }
}
