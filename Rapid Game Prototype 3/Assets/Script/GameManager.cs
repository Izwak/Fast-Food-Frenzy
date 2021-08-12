using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    TITLE,
    GAMEPLAY,
    MINIGAME
}

public class GameManager : MonoBehaviour
{
    public static bool menuOnStart = true;
    public bool isRunning;

    public GameState gameState;

    public PlayerBehaviours1 player1;

    public ScreenManager screen;

    public float timer = 100;
    public static float score = 0; // i might be cheating here but 2 lazy 2 not make static

    public List<string> name;
    public List<string> time;

    // Start is called before the first frame update
    void Start()
    {
        if (menuOnStart)
        {
            screen.menu.gameObject.SetActive(true);
            screen.overlay.gameObject.SetActive(false);
            isRunning = false;
            Time.timeScale = 0;
        }
        else
        {
            screen.menu.gameObject.SetActive(false);
            screen.overlay.gameObject.SetActive(true);
            isRunning = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;

            if (score >= screen.overlay.scoreGoal)
            {
                screen.overlay.gameObject.SetActive(false);
                isRunning = false;
                screen.orders.gameObject.SetActive(false);

                if (timer < 300)
                {
                    screen.win.gameObject.SetActive(true);
                }
                else
                {
                    screen.lose.gameObject.SetActive(true);
                }

            }
            if (GameManager.score <= -screen.overlay.scoreMin)
            {
                screen.overlay.gameObject.SetActive(false);
                screen.fired.gameObject.SetActive(true);
                isRunning = false;
            }
        }
    }


    public void ResetToGameplay()
    {
        score = 0;
        menuOnStart = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        screen.menu.gameObject.SetActive(false);
        screen.overlay.gameObject.SetActive(true);
        isRunning = true;
    }

    public void LoadLeaderboard()
    {
        PlayerData data = Saving.LoadData();

        if (data != null)
        {
            name = data.name;
            time = data.time;
        }


        screen.menu.gameObject.SetActive(false);
        screen.leaderboard.gameObject.SetActive(true);

        screen.leaderboard.DisplayScore();
    }

    public void ResetToMenu()
    {
        Saving.SaveData(this);

        score = 0;
        menuOnStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}