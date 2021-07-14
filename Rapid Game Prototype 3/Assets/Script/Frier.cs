using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frier : MonoBehaviour
{
    public GameObject[] FriesSpots = new GameObject[4];

    public bool[] spotTaken = new bool[4];

    public bool isEmpty, isFull;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isEmpty = true;
        isFull = true;

        for (int i = 0; i < 4; i++)
        {
            FriesSpots[i].SetActive(spotTaken[i]);

            if (spotTaken[i])
            {
                isEmpty = false;
            }
            if (!spotTaken[i])
            {
                isFull = false;
            }
        }
    }
}
