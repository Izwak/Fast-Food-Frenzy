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
    public bool isRunning = false;

    public GameState gameState;

    public PlayerBehaviours1 player1;

    public ScreenManager screen;

    public float timer = 100;
    public static float score = 0; // i might be cheating here but 2 lazy 2 not make static

    public new List<string> name;
    public List<float> time;

    // Start is called before the first frame update
    void Start()
    {
        if (menuOnStart)
        {
            screen.menu.gameObject.SetActive(true);
            screen.overlay.gameObject.SetActive(false);
            isRunning = false;
            player1.isRunning = false;
        }
        else
        {
            screen.menu.gameObject.SetActive(false);
            screen.overlay.gameObject.SetActive(true);
            isRunning = true;
            player1.isRunning = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= screen.overlay.scoreGoal)
        {
            isRunning = false;
            screen.overlay.gameObject.SetActive(false);
            screen.orders.gameObject.SetActive(false);
            screen.leaderboard.gameObject.SetActive(true);

            screen.leaderboard.isEnteringName = true;


            if (timer < 300)
            {
                screen.win.gameObject.SetActive(true);
            }
            else
            {
                screen.lose.gameObject.SetActive(true);
            }

        }
        else if (GameManager.score <= -screen.overlay.scoreMin)
        {
            isRunning = false;
            screen.overlay.gameObject.SetActive(false);
            screen.fired.gameObject.SetActive(true);

            screen.leaderboard.isEnteringName = true;
        }


        if (isRunning)
        {
            timer += Time.deltaTime;
        }
    }


    public void ResetToGameplay()
    {
        score = 0;
        menuOnStart = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player1.isRunning = true;
    }

    public void LoadGameScene()
    {
        screen.menu.gameObject.SetActive(false);
        screen.overlay.gameObject.SetActive(true);
        isRunning = true;
        player1.isRunning = true;
    }

    public void LoadLeaderboard(bool isEntering)
    {
        player1.isRunning = false; 

        print("It should be doing the thing");
        PlayerData data = Saving.LoadData();

        if (data != null)
        {
            name = data.name;
            time = data.time;
        }

        screen.leaderboard.DisplayScore();

        screen.menu.gameObject.SetActive(false);
        screen.leaderboard.gameObject.SetActive(true);

        screen.leaderboard.isEnteringName = isEntering;
    }

    public void ResetToMenu()
    {
        Saving.SaveData(this);

        score = 0;
        menuOnStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player1.isRunning = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        print("YOU QUIT HERE");
    }
}