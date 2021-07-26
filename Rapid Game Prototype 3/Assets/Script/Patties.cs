using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patties : MonoBehaviour
{
    public bool isCooked;
    public bool isBurnt;

    Color red = new Color(1, 0.2f, 0.2f, 1);
    Color brown = new Color(0.6f, 0.2f, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        isCooked = false;
        isBurnt = false;
    }

    // Update is called once per frame
    void Update()
    {
        //is patty burnt?
        if (isBurnt)
        {
            //hardcoded for now
            for (int i = 0; i < 3; i++)
            {
                GameObject paddy = transform.GetChild(i).gameObject;

                if (paddy != null)
                {
                    Material mat = paddy.GetComponent<Material>();

                    if (mat != null)
                    {
                        mat.SetColor("burnt", Color.black);
                    }
                }
            }
        }
        //if not burnt, is it cooked?
        else if (isCooked)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject paddy = transform.GetChild(i).gameObject;

                if (paddy != null)
                {
                    Material mat = paddy.GetComponent<Material>();

                    if (mat != null)
                    {
                        mat.SetColor("cooked", brown);
                    }
                }
            }
        }
        //if not cooked or burnt, must be raw
        else
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject paddy = transform.GetChild(i).gameObject;

                if (paddy != null)
                {
                    Material mat = paddy.GetComponent<Material>();

                    if (mat != null)
                    {
                        mat.SetColor("raw", red);
                    }
                }
            }
        }
    }
}
