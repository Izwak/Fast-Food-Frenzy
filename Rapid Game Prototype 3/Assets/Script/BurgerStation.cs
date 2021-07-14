using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerStation : MonoBehaviour
{
    public Transform paddyHeater;

    public void TakePaddy()
    {
        if (paddyHeater.childCount > 0)
        {
            GameObject quadPadObj = paddyHeater.transform.GetChild(paddyHeater.childCount - 1).gameObject;

            QuadPaddy quadPad = quadPadObj.GetComponent<QuadPaddy>();

            if (quadPad != null)
            {
                quadPad.TakePaddy();
            }
        }
    }
}
