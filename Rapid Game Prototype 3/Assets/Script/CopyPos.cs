using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CopyPos : MonoBehaviour
{
    public GameObject target;
    //public TMP_Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
    }
}
