using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameManager gameManager;

    public Transform customerParent;
    public GameObject customerPrefab;
    public List<GameObject> pointsOfInterest;
    public List<GameObject> registers;


    int tick = 0;

    //public int customerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {
            if (customerParent.childCount < 8 && tick % 1000 == 0)
            {

                CreateCustomer(Random.Range(0, 2));
            }

            tick++;
        }
    }

    void CreateCustomer(int registerNum)
    {
        GameObject newCustomer = Instantiate(customerPrefab);
        newCustomer.transform.position = pointsOfInterest[0].transform.position; // Set new customer to spawn pos
        newCustomer.GetComponent<CustomerController1>().pointsOfInterest[0] = registers[registerNum];

        for (int i = 0; i < pointsOfInterest.Count; i++)
        {
            newCustomer.GetComponent<CustomerController1>().pointsOfInterest[i + 1] = pointsOfInterest[i];
        }

        newCustomer.transform.SetParent(customerParent);
        //customerCount++;
    }
}
