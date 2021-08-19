using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuScreen : MonoBehaviour
{
    public GameManager gameManager;
    public Transform title;
    public CanvasGroup blackScreen;
    public Transform buttonBoard;
    public Transform LevelSelect;

    public Transform[] points;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen.alpha = 1;
        StartCoroutine(fadeInIn(5));
        buttonBoard.position = points[2].position;
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

        while (timer < 2)
        {
            timer += Time.deltaTime;

            title.position = Vector3.Lerp(points[0].position, points[1].position, timer / 2);
            title.localScale = Vector3.Lerp(points[0].localScale, points[1].localScale, timer / 2);


            buttonBoard.position = Vector3.Lerp(points[2].position, points[3].position, timer / 2);

            yield return null;
        }

        //StartCoroutine(transitionToLevelSelect());
    }

    IEnumerator transitionToLevelSelect()
    {
        gameManager.camera.stage = CameraState.GAMELIST;

        float time = 3;
        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;
            title.position = Vector3.Lerp(points[1].position, points[4].position, timer / 2);
            buttonBoard.position = Vector3.Lerp(points[3].position, points[2].position, timer / 2);
            yield return null;
        }

        LevelSelect.gameObject.SetActive(true);
    }

    public void LoadLevelMenu()
    {
        StartCoroutine(transitionToLevelSelect());
    }

    public void CallZoomIn()
    {
        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        float time = .5f;
        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;
            gameManager.camera.stage = CameraState.ZOOMIN;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 5, timer / time);
            blackScreen.alpha = Mathf.Lerp(0, 1, timer / time);
            yield return null;
        }


        timer = 0;
        gameManager.camera.stage = CameraState.GAMEPLAY;

        while (timer < time)
        {
            timer += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(1, 0, timer / time);
            yield return null;
        }

        blackScreen.alpha = 0;
        gameManager.LoadGameScene();
    }

}
