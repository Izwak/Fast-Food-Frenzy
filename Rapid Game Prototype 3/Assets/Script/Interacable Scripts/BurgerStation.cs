using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerStation : MonoBehaviour
{
    public Transform paddyHeater;

    public Transform Minigame;

    public bool CanTakePaddy()
    {

        if (paddyHeater.childCount > 1)
        {
            return true;
        }
        else if (paddyHeater.childCount > 0)
        {
            GameObject quadPadObj = paddyHeater.transform.GetChild(paddyHeater.childCount - 1).gameObject;

            QuadPaddy quadPad = quadPadObj.GetComponent<QuadPaddy>();


            if (quadPad != null && quadPad.paddyCount > 0)
            {
                return true;
            }
        }
        return false;
    }

    public void TakePaddy()
    {
        Minigame.gameObject.SetActive(true);

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
