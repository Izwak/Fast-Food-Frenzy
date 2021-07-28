using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameManager gameManager;

    public Transform customerParent;
    public GameObject customerPrefab;
    public List<GameObject> pointsOfInterest;


    int tick = 0;

    public int customerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {
            if (customerCount < 1 && tick % 1000 == 0)
            {
                CreateCustomer();
            }

            tick++;
        }
    }

    void CreateCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab);
        newCustomer.transform.position = pointsOfInterest[0].transform.position;

        for (int i = 0; i < pointsOfInterest.Count; i++)
        {
            newCustomer.GetComponent<CustomerController1>().pointsOfInterest[i] = pointsOfInterest[i];
        }

        newCustomer.transform.SetParent(customerParent);
        customerCount++;
    }
}
