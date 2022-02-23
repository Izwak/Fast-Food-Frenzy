using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheBoi : MonoBehaviour
{
    public GameObject theBoi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = theBoi.transform.position;
    }
}
