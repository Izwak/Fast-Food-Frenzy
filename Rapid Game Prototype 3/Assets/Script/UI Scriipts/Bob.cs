using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    Vector3 startingPos;

    float tick;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tick -= Time.deltaTime * 8;

        transform.position = new Vector3(startingPos.x, startingPos.y + Mathf.Sin(tick) / 10, startingPos.z);
    }
}
