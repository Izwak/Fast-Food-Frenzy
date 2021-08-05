using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<GameObject> pointsOfInterest;

    public CustomerStage stage;

    Rigidbody body;
    Vector3 target;
    ServiceCounter register;

    float speed = 3;
    public float dis;
    public float angle;
    public int positionInLine;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        stage = CustomerStage.INLINE;
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
            CarController carController = customer.GetComponent<CarController>();

            if (customer == this.gameObject)
            {
                return linePos;
            }

            // If the point of interest and the stage matches this customer that means they are in the same line and this customer is behind 1 
            if (carController != null && carController.pointsOfInterest[interestElement] == PointOfInterest && carController.stage == stage)
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

        if (stage == CustomerStage.INLINE)
        {
            positionInLine = FindPosInLine(pointsOfInterest[0], 0, CustomerStage.ATCOUNTER) + FindPosInLine(pointsOfInterest[0], 0, CustomerStage.INLINE) + FindPosInLine(pointsOfInterest[0], 0, CustomerStage.WAITING);
            body.velocity = transform.forward * speed;
            target = pointsOfInterest[2].transform.position + new Vector3(-10 * positionInLine, 0, 0); // Target to be infront of register but also stay in line
            dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));


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
        if (stage == CustomerStage.ATCOUNTER)
        {
            body.velocity = Vector3.zero;

            if (!register.umHelloImACustomer)
            {
                stage = CustomerStage.WAITING;
            }
        }
        if (stage == CustomerStage.WAITING)
        {
            //if (pointsOfInterest[0])
        }
        if (stage == CustomerStage.PICKUP)
        {
            //GameObject item = pointsOfInterest[4].GetComponent<Interact>().emptySlot.transform.GetChild(0).gameObject;

            Interact counter = pointsOfInterest[4].GetComponent<Interact>();
            PickUp pickUp = pointsOfInterest[4].GetComponent<PickUp>();

            // If theres an item on the pick up counter
            if (counter != null && pickUp != null && counter.emptySlot.transform.childCount > 0)
            {
                GameObject item = counter.emptySlot.transform.GetChild(0).gameObject;

                if (item.CompareTag("Happy Meal"))
                {
                    print(item.name);

                    Destroy(item);

                    stage = CustomerStage.LEAVING;

                    GameManager.score++;
                }
            }
        }
        if (stage == CustomerStage.LEAVING)
        {
            body.velocity = transform.forward * speed;
            target = pointsOfInterest[3].transform.position;
            dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));

            if (dis < 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
