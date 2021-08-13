using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTip : MonoBehaviour
{
    public GameObject holdText;
    public GameObject placeText;

    public float tick = 0;

    private void Start()
    {
        Interact interact = this.gameObject.GetComponent<Interact>();

        if (interact != null)
        {
            GameObject newTray = Instantiate(interact.createObject);
            newTray.transform.SetParent(interact.emptySlot.transform);
            newTray.transform.localPosition = Vector3.zero;
            newTray.transform.localRotation = Quaternion.identity;
        }
    }

    public void waitThenDisplay()
    {
        tick += Time.deltaTime;

        if (tick > .3)
        {
            holdText.SetActive(true);
        }
    }

    public void hide()
    {
        tick = 0;
        holdText.SetActive(false);
    }
}
