using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameManager gameManager;

    public Transform customerParent;
    public GameObject customerPrefab;
    public List<GameObject> carPrefabs;
    public List<GameObject> pointsOfInterest;
    public List<GameObject> carPointsOfInterest;
    public List<GameObject> registers;


    public int tick = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (gameManager.isRunning)
        {
            if (customerParent.childCount < 10 && tick % 180 == 0)
            {
                if (CountCars() < 2)
                {
                    CreateCustomer(Random.Range(0, 3));
                }
                else
                {
                    CreateCustomer(Random.Range(0, 2));
                }
            }
        }

        tick++;
    }

    int CountCars()
    {
        int count = 0;

        for (int i = 0; i < customerParent.childCount; i++)
        {
            GameObject obj = customerParent.GetChild(i).gameObject;
            CarController car = obj.GetComponent<CarController>();

            if (car != null)
            {
                count++;
            }
        }

        return count;
    }

    int FindPosInLine(GameObject PointOfInterest, int interestElement, CustomerStage stage)
    {
        // Find customers position in the line
        // Basically all this does is count the number of other customers before this customer that are also lining up behind the same point of interest
        // the number of customers in front gives the position in line

        // Since all customers should be the child of the same object by going through all the gameobjects in the parent you'll be running through all the customers

        int linePos = 0;

        for (int i = 0; i < customerParent.childCount; i++)
        {
            GameObject customer = customerParent.GetChild(i).gameObject;
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

    void CreateCustomer(int registerNum)
    {
        if (registerNum == 2)
        {
            GameObject newCustomer = Instantiate(carPrefabs[Random.Range(0, 5)]);
            newCustomer.transform.position = carPointsOfInterest[0].transform.position; // Set new customer to spawn pos
            newCustomer.GetComponent<CarController>().pointsOfInterest[0] = registers[registerNum];

            for (int i = 0; i < pointsOfInterest.Count; i++)
            {
                newCustomer.GetComponent<CarController>().pointsOfInterest[i + 1] = carPointsOfInterest[i];
            }

            newCustomer.transform.SetParent(customerParent);
        }
        else
        {
            GameObject newCustomer = Instantiate(customerPrefab);
            newCustomer.transform.position = pointsOfInterest[0].transform.position; // Set new customer to spawn pos
            newCustomer.GetComponent<CustomerController1>().pointsOfInterest[0] = registers[registerNum];

            for (int i = 0; i < pointsOfInterest.Count; i++)
            {
                newCustomer.GetComponent<CustomerController1>().pointsOfInterest[i + 1] = pointsOfInterest[i];
            }

            newCustomer.transform.SetParent(customerParent);
        }
    }
}
