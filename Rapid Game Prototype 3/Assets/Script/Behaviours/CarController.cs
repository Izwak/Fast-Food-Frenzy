using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<GameObject> pointsOfInterest;

    public CustomerStage stage;

    public AudioSource honk;
    float timer = 20;

    Rigidbody body;
    Vector3 target;
    ServiceCounter register;

    float speed = 3;
    public float dis;
    public float angle;
    public int positionInLine;
    public int currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        stage = CustomerStage.INLINE;
        register = pointsOfInterest[0].GetComponent<ServiceCounter>();
        honk = GetComponent<AudioSource>();
        currentTarget = 3;
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
        //play a honk sound every 20 seconds
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //honk.Play();
            timer = 20;
        }

        // Always do this
        angle = Mathf.Atan2(target.x - transform.position.x, target.z - transform.position.z) * Mathf.Rad2Deg;
        dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));
        transform.rotation = Quaternion.Euler(0, angle, 0);

        if (stage == CustomerStage.INLINE)
        {
            positionInLine = FindPosInLine(pointsOfInterest[0], 0, CustomerStage.ATCOUNTER) + FindPosInLine(pointsOfInterest[0], 0, CustomerStage.INLINE);
            body.velocity = transform.forward * speed;
            target = pointsOfInterest[2].transform.position + new Vector3(-10 * positionInLine, 0, 0); // Target to be infront of register but also stay in line
            dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));


            if (dis < 0.1f && positionInLine == 0)
            {
                stage = CustomerStage.ATCOUNTER;
                register.customerAtRegister = CustomerType.DRIVETHRU;
            }
            if (dis < 0.1f)
            {
                body.velocity = Vector3.zero;
            }
        }
        if (stage == CustomerStage.ATCOUNTER)
        {
            body.velocity = Vector3.zero;


            if (dis > 0.5)
            {
                stage = CustomerStage.INLINE;
                register.customerAtRegister = CustomerType.NONE;
            }
            // If your order was taken go to pick up area
            if (register.customerAtRegister == CustomerType.NONE)
            {
                stage = CustomerStage.WAITING;
            }
        }
        if (stage == CustomerStage.WAITING)
        {
            positionInLine = FindPosInLine(pointsOfInterest[5], currentTarget, CustomerStage.WAITING) + FindPosInLine(pointsOfInterest[4], currentTarget, CustomerStage.WAITING);
            target = pointsOfInterest[currentTarget].transform.position + new Vector3(0, 0, 5 * positionInLine);
            dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));

            if (currentTarget < 4)
            {
                body.velocity = transform.forward * speed;
                if (dis < 0.5)
                {
                    currentTarget++;
                }
            }
            else
            {
                if (dis > 0.5)
                {
                    body.velocity = transform.forward * speed;
                }
                else
                {
                    body.velocity = Vector3.zero;
                }
            }
        }
        if (stage == CustomerStage.PICKUP)
        {
            //GameObject item = pointsOfInterest[4].GetComponent<Interact>().emptySlot.transform.GetChild(0).gameObject;

            Interact counter = pointsOfInterest[6].GetComponent<Interact>();
            PickUp pickUp = pointsOfInterest[6].GetComponent<PickUp>();

            // If theres an item on the pick up counter
            if (counter != null && pickUp != null && counter.emptySlot.transform.childCount > 0)
            {
                GameObject item = counter.emptySlot.transform.GetChild(0).gameObject;

                if (item.CompareTag("Happy Meal"))
                {
                    print(item.name);

                    Destroy(item);

                    stage = CustomerStage.LEAVING;
                    currentTarget = 5;

                    GameManager.Instance.score++;
                }
            }
        }
        if (stage == CustomerStage.LEAVING)
        {
            //print("First target" + currentTarget);
            speed = 7;
            body.velocity = transform.forward * speed;
            target = pointsOfInterest[currentTarget].transform.position + new Vector3(2, 0, 0);
            dis = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z));


            if (dis < 0.1f)
            {

                currentTarget++;
            }
            if (currentTarget == 6)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
