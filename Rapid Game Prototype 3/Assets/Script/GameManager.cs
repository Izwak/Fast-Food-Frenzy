using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject customerPrefab;

    //for testing, can remove
    float timer = 10;

    GameObject customer;
    // Start is called before the first frame update
    void Start()
    {
        //customer = Instantiate(customerPrefab, customerPrefab.GetComponent<CustomerController>().spawnPos.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
