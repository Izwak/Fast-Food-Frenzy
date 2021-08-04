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

    public GameObject menuScreen;
    public GameObject overlayScreen;
    public GameObject orderScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject customers;

    public int scoreGoal = 12;

    public float countdown = 100;
    public static float score = 0; // i might be cheating here but 2 lazy 2 not make static

    // Start is called before the first frame update
    void Start()
    {
        if (menuOnStart)
        {
            menuScreen.SetActive(true);
            overlayScreen.SetActive(false);
            isRunning = false;
            Time.timeScale = 0;
        }
        else
        {
            menuScreen.SetActive(false);
            overlayScreen.SetActive(true);
            isRunning = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                overlayScreen.SetActive(false);
                isRunning = false;
                orderScreen.SetActive(false);

                if (score >= scoreGoal)
                {
                    winScreen.SetActive(true);
                }
                else
                {
                    loseScreen.SetActive(true);
                }
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
        menuScreen.SetActive(false);
        overlayScreen.SetActive(true);
        isRunning = true;
    }
}