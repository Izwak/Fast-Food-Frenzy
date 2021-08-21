using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderBehaviour : MonoBehaviour
{
    public GameObject emptySlot;
    public Slider bar;

    public CustomerType type;

    public GameObject alertYellow;
    public GameObject alertRed;

    public GameObject takeAway;
    public GameObject driveThru;
    public GameObject dineIn;

    public int GetFoodCount()
    {
        return emptySlot.transform.childCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        //bar.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Increate timer
        bar.value += Time.deltaTime;

        // Alert Activation
        if (bar.value > bar.maxValue * 5/6)
        {
            alertRed.SetActive(true);
            alertYellow.SetActive(false);
        }
        else if (bar.value > bar.maxValue * 2/3)
        {
            alertYellow.SetActive(true);
        }

        // Display order type

        if (type == CustomerType.DRIVETHRU)
        {
            driveThru.SetActive(true);
        }
        else if (type == CustomerType.TAKEAWAY)
        {
            takeAway.SetActive(true);
        }
        else if (type == CustomerType.DINEIN)
        {
            dineIn.SetActive(true);
        }

        // Set Colour
        if (bar.value < bar.maxValue / 2) { bar.image.color = Color.Lerp(Color.green, Color.yellow, bar.value / (bar.maxValue / 2)); }
        else { bar.image.color = Color.Lerp(Color.yellow, Color.red, (bar.value - (bar.maxValue / 2)) / (bar.maxValue / 2)); }

        // Organises the how the food is displayed in the order
        for (int i = 0; i < GetFoodCount(); i++)
        {
            Transform food = emptySlot.transform.GetChild(i).transform;

            if (GetFoodCount() == 3)
            {
                food.localScale = Vector3.one;

                if (i < 2)
                {
                    food.localPosition = new Vector3(i * 75 - 50, -i * 20, 0);
                }
                else
                {
                    food.localPosition = new Vector3(-20, -45, 0);
                }
            }
            else if (GetFoodCount() == 4)
            {
                food.localScale = Vector3.one;

                if (i == 0)
                {
                    food.localPosition = new Vector3(-15, 0, 0);
                }
                else if (i == 3)
                {
                    food.localPosition = new Vector3(-15, -50, 0);
                }
                else
                {
                    food.localPosition = new Vector3((i - 1) * 90 - 60, -20 - (i - 1) * 10, 0);
                }
            }
            else if (GetFoodCount() == 2)
            {
                food.localPosition = new Vector3(i * 85 - 55, -i * 25 - 15, 0);
                food.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                food.localPosition = new Vector3(-15, -25, 0);
                food.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }
    }

    public void PulseIcon()
    {
        StartCoroutine(waitThenDisableIcon(takeAway, 1));
        StartCoroutine(waitThenDisableIcon(driveThru, 1));
        StartCoroutine(waitThenDisableIcon(dineIn, 1));
    }

    IEnumerator waitThenDisableIcon(GameObject orderType, float time)
    {
        Throb throbIcon = orderType.GetComponent<Throb>();

        throbIcon.enabled = true;

        yield return new WaitForSeconds(time);

        throbIcon.enabled = false;
        orderType.transform.localScale = Vector3.one;
    }
}
