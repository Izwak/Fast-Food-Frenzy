using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerStage
{
    INLINE,
    ATCOUNTER,
    WAITING,
    PICKUP,
    LEAVING
}

public class CustomerController1 : MonoBehaviour
{
    public Transform emptySlot;
    public List<GameObject> pointsOfInterest;

    Rigidbody body;
    ServiceCounter register;
    public CustomerStage stage;

    Vector3 target;

    float speed = 3;
    public float dis;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        stage = CustomerStage.INLINE;
        body = GetComponent<Rigidbody>(); 
        register = pointsOfInterest[1].GetComponent<ServiceCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        // Always do this
        angle = Mathf.Atan2(target.x - transform.position.x, target.z - transform.position.z) * Mathf.Rad2Deg;
        dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));
        transform.rotation = Quaternion.Euler(0, angle, 0);

        // When in line walk to register
        if (stage == CustomerStage.INLINE)
        {
            body.velocity = transform.forward * speed; 
            target = pointsOfInterest[1].transform.position + new Vector3(0, 0, -2); // Target to be infront of register

            if (dis < 0.1f)
            {
                stage = CustomerStage.ATCOUNTER;
                register.umHelloImACustomer = true;
            }
        }
        // When at register wait for someone 2 take ur order
        if (stage == CustomerStage.ATCOUNTER)
        {
            body.velocity = Vector3.zero; 
            transform.rotation = Quaternion.Euler(0, 0, 0);

            // If your order was taken go to pick up area
            if (!register.umHelloImACustomer)
            {
                stage = CustomerStage.WAITING;
            }

            // If pushed away from countetr get back in line
            if (dis > 0.3f)
            {
                register.umHelloImACustomer = false;
                stage = CustomerStage.INLINE;
            }
        }
        if (stage == CustomerStage.WAITING)
        {
            target = pointsOfInterest[2].transform.position;

            if (dis > 0.5f)
            {
                body.velocity = transform.forward * speed;
            }
            else
            {
                body.velocity = Vector3.zero;
            }
        }
        else if (stage == CustomerStage.PICKUP)
        {
            // K Heres what happens
            // Check if a pickup is ready for you
            //  - order is ready when a fifth point of interest appears that is attached to a pick up counter
            // Go towards the pick up counter once within reaching distance then advance to collecting order
            // Can only pick up order if
            //  - there is an order in the menu
            //  - there is a order on the counter
            //  - there a matching order in menu and on counter
            // When collecting do the following
            //  - remove order from menu matching that to the one on the counter
            //  - take order from counter and put it into customers empty slot
            // And finally tell customer to leave




            if (pointsOfInterest.Count > 4)
            {
                target = pointsOfInterest[4].transform.position;
                dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));

                if (dis > 1.5f)
                {
                    body.velocity = transform.forward * speed;
                }
                else
                {
                    body.velocity = Vector3.zero;

                    Interact counter = pointsOfInterest[4].GetComponent<Interact>();
                    PickUp pickUp = pointsOfInterest[4].GetComponent<PickUp>();

                    // If theres an item on the pick up counter
                    if (counter != null && pickUp != null && counter.emptySlot.transform.childCount > 0)
                    {
                        GameObject item = counter.emptySlot.transform.GetChild(0).gameObject;

                        // Check if there is an order in the menu that matches the 1 on the counter
                        for (int i = 0; i < pickUp.orderMenu.transform.childCount; i++)
                        {
                            if (item.name == pickUp.orderMenu.GetChild(i).name)
                            {
                                print(item.name);

                                item.transform.SetParent(emptySlot);
                                item.transform.localPosition = Vector3.zero;
                                item.transform.localRotation = Quaternion.identity;

                                pickUp.RemoveDisplayOrder(i);
                                stage = CustomerStage.LEAVING;

                                break;
                            }
                        }


                        /*GameObject item = counter.emptySlot.transform.GetChild(0).gameObject;

                        item.transform.SetParent(emptySlot);
                        item.transform.localPosition = Vector3.zero;
                        item.transform.localRotation = Quaternion.identity;

                        stage = CustomerStage.LEAVING;*/
                    }
                }



                /*PickUp pickUp = pointsOfInterest[1].GetComponent<PickUp>();

                if (pickUp != null)
                {
                    pickUp.RemoveDisplayOrder(i);
                }*/
            }
        }
        else if (stage == CustomerStage.LEAVING)
        {
            target = pointsOfInterest[3].transform.position;
            body.velocity = transform.forward * speed;
        }

/*        // Target should be just in front of a the register
        Vector3 target = pos[1].transform.position + new Vector3(0, 0, -2);

        dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));

        angle = Mathf.Atan2(target.x - transform.position.x, target.z - transform.position.z) * Mathf.Rad2Deg;

        ServiceCounter register = pos[1].GetComponent<ServiceCounter>();

        if ()

        if (dis > 0.1f)
        {
            body.velocity = transform.forward * speed;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else if (body.velocity != Vector3.zero)
        {
            body.velocity = Vector3.zero;

            register.umHelloImACustomer = true;
        }
        else if (!register.umHelloImACustomer)
        {

        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.01f);
        }*/

        
    }
}
