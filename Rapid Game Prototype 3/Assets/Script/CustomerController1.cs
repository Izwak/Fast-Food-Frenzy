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
    public int positionInLine;

    // Start is called before the first frame update
    void Start()
    {
        stage = CustomerStage.INLINE;
        body = GetComponent<Rigidbody>(); 
        register = pointsOfInterest[0].GetComponent<ServiceCounter>();
    }

    int FindPosInLine(GameObject PointOfInterest, int interestElement, CustomerStage stage) 
    {
        // Find customers position in the line
        // Basically all this does is count the number of other customers before this customer that are also lining up behind the same point of interest
        // the number of customers in front gives the position in line

        // Since all customers should be the child of the same object by going through all the gameobjects in the parent you'll be running through all the customers

        int linePos = 0;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            GameObject customer = transform.parent.GetChild(i).gameObject;
            CustomerController1 customerController = customer.GetComponent<CustomerController1>();

            if (customer == this.gameObject)
            {
                return linePos;
            }

            // If the point of interest and the stage matches this customer that means they are in the same line and this customer is behind 1 
            if (customerController != null && customerController.pointsOfInterest[interestElement] == PointOfInterest && customerController.stage == stage)
            {
                linePos++;
            }
        }

        return linePos;
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
            // Find customers position in the line
            positionInLine = FindPosInLine(pointsOfInterest[0], 0, CustomerStage.ATCOUNTER) + FindPosInLine(pointsOfInterest[0], 0, CustomerStage.INLINE);

            body.velocity = transform.forward * speed; 
            target = pointsOfInterest[0].transform.position + new Vector3(0, 0, -2 - positionInLine * 1.5f); // Target to be infront of register but also stay in line

            if (dis < 0.1f && positionInLine == 0)
            {
                stage = CustomerStage.ATCOUNTER;
                register.umHelloImACustomer = true;
            }
            if (dis < 0.1f)
            {
                body.velocity = Vector3.zero;
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

            positionInLine = FindPosInLine(pointsOfInterest[2], 2, CustomerStage.WAITING);

            target = pointsOfInterest[2].transform.position + new Vector3(0, 0, -positionInLine * 1.5f);

            if (dis > 0.1f)
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
                            item.transform.SetParent(emptySlot);
                            item.transform.localPosition = Vector3.zero;
                            item.transform.localRotation = Quaternion.identity;

                            //pickUp.RemoveDisplayOrder(i);
                            stage = CustomerStage.LEAVING;

                            GameManager.score++;

                            break;
                        }
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
            dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));

            body.velocity = transform.forward * speed;

            if (dis < 0.5f)
            {
                Destroy(this.gameObject);

            }
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
