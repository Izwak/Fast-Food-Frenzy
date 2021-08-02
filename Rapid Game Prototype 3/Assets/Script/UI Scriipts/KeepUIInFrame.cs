using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUIInFrame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        RectTransform rect = GetComponent<RectTransform>();

        ///float clampX = Mathf.Clamp(rect.transform.localPosition.x, rect.rect.width / 2, Screen.width - rect.rect.width / 2);
        //float clampY = Mathf.Clamp(rect.transform.localPosition.y, rect.rect.height / 2, Screen.height - rect.rect.height / 2);

        float clampX = Mathf.Clamp(rect.transform.localPosition.x, - Screen.width / 2 + rect.rect.width / 2, Screen.width / 2 - rect.rect.width / 2);
        float clampY = Mathf.Clamp(rect.transform.localPosition.y, -Screen.height / 2 + rect.rect.height / 2, Screen.height / 2 - rect.rect.height / 2);

        rect.transform.localPosition = new Vector3(clampX, clampY, 0);
    }
}
