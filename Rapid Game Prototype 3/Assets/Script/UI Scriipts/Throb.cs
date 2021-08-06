using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throb : MonoBehaviour
{
    float tick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        tick -= Time.deltaTime * 8;

        transform.localScale = Vector3.one * ((Mathf.Sin(tick) + 3) /3);
    }
}
