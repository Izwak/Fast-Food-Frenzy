using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviours : MonoBehaviour
{

    Rigidbody body;

    float speed = 5;
    float angle;

    RaycastHit hit;

    Vector2 tartgetPoint = new Vector2(0, -1);

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tartgetPoint += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        angle = Mathf.Atan2(tartgetPoint.x, tartgetPoint.y) * Mathf.Rad2Deg;

        if (tartgetPoint.magnitude > speed)
        {
            tartgetPoint = tartgetPoint.normalized * speed;
        }

        //Debug.Log("target " + tartgetPoint + " angle " + angle + " mag " + tartgetPoint.magnitude);

        body.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        transform.rotation = Quaternion.Euler(0, angle, 0);

        Shoot();
    }

    void Shoot()
    {
        //Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit newhit;


        if (Physics.Raycast(transform.position - new Vector3(0, 1, 0), transform.forward, out newhit))
        {
            if (hit.transform != null)
            { 
                if (!newhit.Equals(hit))
                {
                    Outline oldOutline = hit.transform.GetComponent<Outline>();
                    if (oldOutline != null)
                    {
                        oldOutline.enabled = false;
                        //oldOutline.OutlineWidth = 0;
                    }
                }
            }

            hit = newhit;

            // For some reason this shoots out in funky direction
            //target = hit.point;
            Debug.Log("You hit a boi");
            Debug.Log(hit.transform.tag);

            Outline outline = hit.transform.GetComponent<Outline>();
            if (outline != null) {
                if (hit.distance < 2)
                {
                    outline.enabled = true;
                    //outline.OutlineWidth = 4;
                }
                else
                {
                    outline.enabled = false;
                    //outline.OutlineWidth = 0;
                }
            }
        }
    }
}
