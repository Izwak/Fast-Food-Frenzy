using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceCounter : MonoBehaviour
{
    public Canvas canvas;

    public GameObject fryPrefab;

    public GameObject alert;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddOrdersToScreen()
    {
        GameObject newOrder = Instantiate(fryPrefab, canvas.transform);

        newOrder.transform.localPosition = new Vector3(-1000 + 300 * (canvas.transform.childCount - 1), 500, 0);
        alert.SetActive(false);
    }
}
