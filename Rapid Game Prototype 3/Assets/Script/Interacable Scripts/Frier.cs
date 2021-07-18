using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frier : MonoBehaviour
{
    public GameObject[] traySlots = new GameObject[4];


    public bool isFull ()
    {

        for (int i = 0; i < 4; i++)
        {
            if (traySlots[i].transform.childCount == 0)
            {
                return false;
            }
        }
        return true;
    }
    public bool isEmpty()
    {
        for (int i = 0; i < 4; i++)
        {
            if (traySlots[i].transform.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
