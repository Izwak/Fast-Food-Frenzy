using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMenuScreen : MonoBehaviour
{
    public static bool dofadeAtBegining = true;

    public GameManager gameManager;
    public Transform title;
    public CanvasGroup blackScreen;
    public Transform buttonBoard;
    public Transform LevelSelect;

    public GameObject eventButton;

    public Transform[] points;

    // Start is called before the first frame update
    void Start()
    {
        if (dofadeAtBegining)
        {
            blackScreen.alpha = 1;
            StartCoroutine(fadeInIn(5));
            buttonBoard.position = points[2].position;
            dofadeAtBegining = false;
        }
        else
        {
            blackScreen.alpha = 0;
            title.position = points[1].position;
            title.localScale = points[1].localScale;
            buttonBoard.position = points[3].position;

            if (GameManager.isUsingController)
                EventSystem.current.SetSelectedGameObject(eventButton);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator fadeInIn(float time)
    {
        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(1, 0, timer / time);
            yield return null;
        }

        blackScreen.alpha = 0; // Just 2 b sure;

        timer = 0;

        while (timer < time / 2)
        {
            timer += Time.deltaTime;

            title.position = Vector3.Lerp(points[0].position, points[1].position, timer / (time / 2));
            title.localScale = Vector3.Lerp(points[0].localScale, points[1].localScale, timer / (time / 2));


            buttonBoard.position = Vector3.Lerp(points[2].position, points[3].position, timer / (time / 2));

            yield return null;
        }

        EventSystem.current.SetSelectedGameObject(eventButton);

        //StartCoroutine(transitionToLevelSelect());
    }

    public void LoadLevelMenu()
    {
        StopAllCoroutines();
        StartCoroutine(transitionToLevelSelect());
    }

    IEnumerator transitionToLevelSelect()
    {
        gameManager.camerBoi.stage = CameraState.GAMELIST;

        float time = 3;
        float timer = 0;

        Vector3 titleStart = title.position;
        Vector3 buttonBoardStart = buttonBoard.position;

        while (timer < time)
        {
            timer += Time.deltaTime;
            title.position = Vector3.Lerp(titleStart, points[4].position, timer / 2);
            buttonBoard.position = Vector3.Lerp(buttonBoard.position, points[2].position, timer / 2);
            yield return null;
        }

        LevelSelect.gameObject.SetActive(true);
    }

    public void CallZoomIn(int mode)
    {
        StartCoroutine(ZoomIn(mode));
    }

    IEnumerator ZoomIn(int mode)
    {
        float time = .5f;
        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;
            gameManager.camerBoi.stage = CameraState.ZOOMIN;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 5, timer / time);
            blackScreen.alpha = Mathf.Lerp(0, 1, timer / time);
            yield return null;
        }


        timer = 0;
        gameManager.camerBoi.stage = CameraState.GAMEPLAY;

        while (timer < time)
        {
            timer += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(1, 0, timer / time);
            yield return null;
        }

        blackScreen.alpha = 0;
        gameManager.LoadGameScene(mode);
    }

}
