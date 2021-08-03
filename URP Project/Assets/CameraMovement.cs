using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float height;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        height = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            height -= 0.05f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            height += 0.05f;
        }

        transform.position += new Vector3(0, height, 0);
        transform.rotation = Quaternion.Euler(-Input.mousePosition.y / 10, Input.mousePosition.x / 10, 0);

        transform.position += transform.forward * Input.GetAxis("Vertical") / 5;
        transform.position += transform.right * Input.GetAxis("Horizontal") / 5;
    }
}
