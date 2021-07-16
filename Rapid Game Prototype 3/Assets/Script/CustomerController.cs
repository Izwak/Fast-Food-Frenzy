using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    //essentially waypoints. These are TRANSFORMS, not GAMEOBJECTS
    public Transform spawnPos;
    public Transform orderPos;
    public Transform waitingPos;
    public Transform leavingPos;

    //the higher the number, the faster it will move
    public float step;

    enum States
    {
        ARRIVING,
        ORDERING,
        WAITING,
        LEAVING
    }

    States state;

    //for testing, can remove
    float timer = 5;

    // Start is called before the first frame update
    void Start()
    {
        state = States.ARRIVING;
    }

    // Update is called once per frame
    void Update()
    {
        //this whole thing is essentially a state machine (needs a way to change from ordering to waiting, and waiting to leaving)
        if (state == States.ARRIVING)
        {
            transform.position = Vector3.MoveTowards(transform.position, orderPos.position, step * Time.deltaTime);
            //check if distance is tiny enough because float point errors
            if (Vector3.Distance(transform.position, orderPos.position) <= 0.01f)
            {
                transform.position = orderPos.position;
                state = States.ORDERING;
            }
        }

        //if customer has ordered already
        if (state == States.WAITING)
        {
            if (Vector3.Distance(transform.position, waitingPos.position) >= 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, waitingPos.position, step * Time.deltaTime);
            }
        }

        //if customer is leaving
        if (state == States.LEAVING)
        {
            if (Vector3.Distance(transform.position, leavingPos.position) >= 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, leavingPos.position, step * Time.deltaTime);
            }
            else
            {
                //delete it when it gets to leavingPos
                Destroy(gameObject);
            }
        }

        //for testing, can remove
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            print("0");
            if (state == States.WAITING)
            {
                print("switch 2");
                timer = 5;
                state = States.LEAVING;
            }
            else if (state == States.ORDERING)
            {
                print("switch 1");
                timer = 5;
                state = States.WAITING;
            }
        }
    }
}
