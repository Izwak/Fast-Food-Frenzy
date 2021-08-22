using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamMachine : MonoBehaviour
{
    public GameManager gameManager;
    public float tick = 60;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isRunning)
        {
            tick += Time.deltaTime;
        }
    }
}
