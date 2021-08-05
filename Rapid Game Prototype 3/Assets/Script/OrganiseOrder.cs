using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganiseOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.childCount == 3)
            {
                transform.GetChild(i).localScale = Vector3.one;

                if (i < 2)
                {
                    transform.GetChild(i).localPosition = new Vector3(i * 75 - 50, - i * 20, 0);
                }
                else
                {
                    transform.GetChild(i).localPosition = new Vector3(-20, -45, 0);
                }
            }
            else if (transform.childCount == 4)
            {
                transform.GetChild(i).localScale = Vector3.one;

                if (i == 0)
                {
                    transform.GetChild(i).localPosition = new Vector3(-15, 0, 0);
                }
                else if (i == 3)
                {
                    transform.GetChild(i).localPosition = new Vector3(-15, -50, 0);
                }
                else
                {
                    transform.GetChild(i).localPosition = new Vector3((i - 1) * 90 - 60, -20 - (i - 1) * 10, 0);
                }
            }
            else if (transform.childCount == 2)
            {
                transform.GetChild(i).localPosition = new Vector3(i * 85 - 55, -i * 25 - 15, 0);
                transform.GetChild(i).localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                transform.GetChild(i).localPosition = new Vector3(- 15, -25, 0);
                transform.GetChild(i).localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }
    }
}
