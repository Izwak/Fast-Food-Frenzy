using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispencer : MonoBehaviour
{
    Interact interact;

    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<Interact>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interact != null && interact.emptySlot.transform.childCount == 0)
        {
            GameObject newTray = Instantiate(interact.createObject);
            newTray.transform.localPosition = Vector3.zero;
            newTray.transform.localRotation = Quaternion.identity;
            newTray.transform.SetParent(interact.emptySlot.transform);
        }
    }
}
