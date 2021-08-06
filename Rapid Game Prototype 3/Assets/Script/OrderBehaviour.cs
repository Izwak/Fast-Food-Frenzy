using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderBehaviour : MonoBehaviour
{
    public GameObject emptySlot;
    public GameObject alertYellow;
    public GameObject alertRed;

    public Slider bar;
    public Image barColor;

    float timer = 0;

    public int GetFoodCount()
    {
        return emptySlot.transform.childCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        bar.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        bar.value = timer;


        if (bar.value >= bar.maxValue)
        {
            // Do the code here
        }
        else if (bar.value > bar.maxValue * 5/6)
        {
            alertRed.SetActive(true);
            alertYellow.SetActive(false);
            barColor.color = Color.red;
        }
        else if (bar.value > bar.maxValue * 2/3)
        {

            alertYellow.SetActive(true);
            barColor.color = Color.yellow;

            //barColor.color = Color.Lerp(Color.yellow, Color.Red, (bar.value - bar.maxValue / 2) / (bar.maxValue / 6));
        }
        else if (bar.value < bar.maxValue / 2)
        {

            //barColor.color = Color.Lerp(Color.green, Color.yellow, (bar.value - bar.maxValue / 2) / (bar.maxValue / 6));
        }

        if (bar.value < bar.maxValue / 2)
        {
            barColor.color = Color.Lerp(Color.green, Color.yellow, bar.value / (bar.maxValue / 2));
        }
        else
        {
            barColor.color = Color.Lerp(Color.yellow, Color.red, (bar.value - (bar.maxValue / 2)) / (bar.maxValue / 2));
        }


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
}
