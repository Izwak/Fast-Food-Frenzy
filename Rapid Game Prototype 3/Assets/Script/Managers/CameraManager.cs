using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState
{
    MAINMENU,
    GAMELIST,
    GAMEPLAY,
    ZOOMIN
}

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform[] cameraPoints;
    public Transform rig;

    public CameraState stage;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera.transform.SetParent(cameraPoints[((int)stage)]);
        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {

        if (stage == CameraState.MAINMENU)
        {
            mainCamera.transform.SetParent(cameraPoints[0]);
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, Vector3.zero, 0.01f);
            mainCamera.transform.localRotation = Quaternion.Lerp(mainCamera.transform.localRotation, Quaternion.identity, 0.01f);
        }
        else if (stage == CameraState.GAMELIST)
        {
            mainCamera.transform.SetParent(cameraPoints[1]);
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, Vector3.zero, 0.01f);
            mainCamera.transform.localRotation = Quaternion.Lerp(mainCamera.transform.localRotation, Quaternion.identity, 0.01f);
        }
        else if (stage == CameraState.ZOOMIN)
        {
            mainCamera.transform.SetParent(cameraPoints[2]);
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, Vector3.zero, 0.01f);
            mainCamera.transform.localRotation = Quaternion.Lerp(mainCamera.transform.localRotation, Quaternion.identity, 0.01f);
        }
        else if (stage == CameraState.GAMEPLAY)
        {
            mainCamera.transform.SetParent(rig);
            mainCamera.transform.localPosition = Vector3.zero;
            mainCamera.transform.localRotation = Quaternion.identity;
        }
    }
}
