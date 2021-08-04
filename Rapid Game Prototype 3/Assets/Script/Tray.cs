using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    public GameObject emptySlot;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < emptySlot.transform.childCount; i++)
        {
            GameObject food = emptySlot.transform.GetChild(i).gameObject;

            if (emptySlot.transform.childCount == 1)
            {
                food.transform.localPosition = new Vector3(0, 0, 0);
            }
            else if (emptySlot.transform.childCount == 2)
            {
                food.transform.localPosition = new Vector3(0.2f - i * 0.4f, 0, 0);
            }
            else if (emptySlot.transform.childCount == 3)
            {
                food.transform.localPosition = new Vector3(0.2f - i * 0.2f, 0, -0.15f + i % 2 * 0.3f);
            }
            else if (emptySlot.transform.childCount == 4)
            {
                if (i < 2)
                {
                    food.transform.localPosition = new Vector3(0.2f - i % 2 * 0.4f, 0, -0.15f);
                }
                else
                {
                    food.transform.localPosition = new Vector3(0.2f - i % 2 * 0.4f, 0, 0.15f);
                }
            }
        }
    }
}
