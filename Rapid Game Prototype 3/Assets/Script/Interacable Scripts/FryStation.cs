using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryStation : MonoBehaviour
{
    public Transform FryLevel;

    float defaultYLvl;
    float fryLvlY;

    public float fryLvl = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultYLvl = FryLevel.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        fryLvlY = defaultYLvl + 0.05f * fryLvl;

        FryLevel.transform.position = new Vector3(FryLevel.transform.position.x, fryLvlY, FryLevel.transform.position.z);
    }
}
