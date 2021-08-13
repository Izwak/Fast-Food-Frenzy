using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    MAIN_MENU,
    MENU,
    GAMEPLAY,
}

public class GameManager : MonoBehaviour
{
    public static GameState gameState;
    public GameState test;

    public bool isRunning = false;

    public PlayerBehaviours1 player1;

    public ScreenManager screen;

    public float timer = 100;
    public static float score = 0; // i might be cheating here but 2 lazy 2 not make static

    public new List<string> name;
    public List<float> time;

    // Start is called before the first frame update
    void Start()
    {
        screen.leaderboard.gameObject.SetActive(false);

        if (gameState == GameState.MAIN_MENU)
        {
            screen.menu.gameObject.SetActive(true);
            screen.overlay.gameObject.SetActive(false);
            isRunning = false;
            player1.isRunning = false;
            //gameState = GameState.MENU;
        }
        else if (gameState == GameState.GAMEPLAY || gameState == GameState.MENU)
        {
            screen.menu.gameObject.SetActive(false);
            screen.overlay.gameObject.SetActive(true);
            isRunning = true;
            player1.isRunning = true;
            gameState = GameState.GAMEPLAY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        test = gameState;

        if (gameState == GameState.GAMEPLAY)
        {
            if (isRunning)
            {
                timer += Time.deltaTime;
            }

            if (score >= screen.overlay.scoreGoal)
            {
                isRunning = false;
                screen.overlay.gameObject.SetActive(false);
                screen.orders.gameObject.SetActive(false);

                screen.leaderboard.isEnteringName = true;

                if (timer < 300)
                {
                    screen.win.gameObject.SetActive(true);
                }
                else
                {
                    screen.lose.gameObject.SetActive(true);
                }

                gameState = GameState.MENU;
            }
            else if (GameManager.score <= -screen.overlay.scoreMin)
            {
                isRunning = false;
                screen.overlay.gameObject.SetActive(false);
                screen.fired.gameObject.SetActive(true);

                screen.leaderboard.isEnteringName = true;

                gameState = GameState.MENU;
            }
        }
        else if (gameState == GameState.MENU)
        {
            print("Menu");
        }
    }

    public void ResetToGameplay()
    {
        score = 0;
        gameState = GameState.MENU;
        print(gameState + " BEFORE THE CHANGE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadGameScene()
    {
        screen.menu.gameObject.SetActive(false);
        screen.overlay.gameObject.SetActive(true);
        isRunning = true;
        player1.isRunning = true;
        gameState = GameState.GAMEPLAY;
    }

    public void LoadLeaderboard(bool isEntering)
    {
        screen.menu.gameObject.SetActive(false);
        screen.fired.gameObject.SetActive(false);
        screen.lose.gameObject.SetActive(false);
        screen.win.gameObject.SetActive(false);



        player1.isRunning = false; 

        PlayerData data = Saving.LoadData();

        if (data != null)
        {
            name = data.name;
            time = data.time;
        }

        screen.leaderboard.DisplayScore();
        screen.leaderboard.OrganiseScore();
        screen.leaderboard.gameObject.SetActive(true);

        screen.leaderboard.isEnteringName = isEntering;
    }

    public void ResetToMenu()
    {
        //Saving.SaveData(this);

        score = 0;
        gameState = GameState.MAIN_MENU;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player1.isRunning = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        print("YOU QUIT HERE");
    }
}