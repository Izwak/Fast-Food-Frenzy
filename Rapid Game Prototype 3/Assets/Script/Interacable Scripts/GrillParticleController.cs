using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillParticleController : MonoBehaviour
{
    public GameObject emptySlot;
    public GameObject pattyParticles;
    GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        clone = Instantiate(pattyParticles, emptySlot.transform.position, Quaternion.identity);
        clone.transform.Rotate(new Vector3(-90, 0, 0));
        //clone.transform.Translate(new Vector3(0, 0, 1));
        clone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (emptySlot.transform.childCount > 0)
        {
            //print("yes patties");
            if (emptySlot.transform.GetChild(0) != null)
            {
                //print("particles");
                //clone.transform.position = gameObject.transform.position;//so we can offset the position to be more correct
                clone.SetActive(true);
            }
        }
        if (emptySlot.transform.childCount <= 0)
            clone.SetActive(false);
    }
}
