using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUIInFrame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        RectTransform rect = GetComponent<RectTransform>();

        float clampX = Mathf.Clamp(rect.transform.position.x, rect.rect.width / 2, Screen.width - rect.rect.width / 2);
        float clampY = Mathf.Clamp(rect.transform.position.y, rect.rect.height / 2, Screen.height - rect.rect.height / 2);    

        rect.transform.position = new Vector3(clampX, clampY, 0);
    }
}
