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

public enum GameEnding
{ 
    NONE,
    SCORING,
    FIRED,
    DEAD
}

public class GameManager : MonoBehaviour
{
    public static GameState state;
    public GameEnding ending = GameEnding.NONE;
    public GameState test;

    public bool isRunning = false;

    public PlayerBehaviours1 player1;

    public ScreenManager screen;
    public new CameraManager camera;
    public new AudioManager audio;

    public float timer = 301;
    public int finalScore;
    public static float score = 0; // i might be cheating here but 2 lazy 2 not make static
    public int numCustomersServed;
    public int numCustomersPissed;
    public int numFoodWasted;

    public List<string> names;
    public List<float> times;
    public List<int> scores;

    public Collider buildingHitBox;

    // Start is called before the first frame update
    void Start()
    {
        screen.leaderboard.gameObject.SetActive(false);
        screen.win.gameObject.SetActive(false);
        screen.win.confetti.SetActive(false);

        if (state == GameState.MAIN_MENU)
        {
            screen.menu.gameObject.SetActive(true);
            screen.overlay.gameObject.SetActive(false);
            isRunning = false;
            player1.isRunning = false;
            audio.Play("Main Menu Theme");
        }
        else if (state == GameState.GAMEPLAY || state == GameState.MENU)
        {
            screen.menu.gameObject.SetActive(false);
            screen.overlay.gameObject.SetActive(true);
            isRunning = true;
            player1.isRunning = true;
            state = GameState.GAMEPLAY;
            audio.Play("Savvy Server");
            camera.stage = CameraState.GAMEPLAY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        test = state;

        if (state == GameState.GAMEPLAY)
        {
            if (isRunning)
            {
                timer -= Time.deltaTime;

                // Start panicing once u get to 15 seconds
                if (timer < 17.2 && !audio.IsPlaying("Panic"))
                {
                    audio.Play("Panic");
                    print("Number of times this is being run");
                    audio.FadeOut("Savvy Server", 7f);
                }

                // Take you to the Score Screen
                if (timer < 0 || GameManager.score >= screen.overlay.scoreGoal)
                {
                    isRunning = false;
                    screen.overlay.gameObject.SetActive(false);
                    screen.orders.gameObject.SetActive(false);

                    screen.leaderboard.isEnteringName = true;

                    screen.win.gameObject.SetActive(true);

                    state = GameState.MENU;
                    audio.Stop("Panic");
                    audio.Stop("Savvy Server");

                    audio.Play("YAAAY");
                }

                // Take you to the Fired screen
                else if (GameManager.score <= -screen.overlay.scoreMin)
                {
                    isRunning = false;
                    screen.overlay.gameObject.SetActive(false);
                    screen.fired.gameObject.SetActive(true);

                    screen.leaderboard.isEnteringName = true;

                    state = GameState.MENU;
                    audio.Stop("Panic");
                    audio.Stop("Savvy Server");


                    audio.Play("YAAAY");
                }

                else if (ending == GameEnding.DEAD)
                {
                    isRunning = false;
                    screen.overlay.gameObject.SetActive(false);
                    screen.overlay.gameObject.SetActive(false);
                    screen.dead.gameObject.SetActive(true);

                    state = GameState.MENU;
                    audio.Stop("Panic");
                    audio.Stop("Savvy Server");


                    audio.Play("YAAAY");
                }
            }
        }
        else if (state == GameState.MENU)
        {
            if (!audio.IsPlaying("Shift Over"))
            {
                audio.FadeIn("Shift Over", 3);
            }
        }
    }

    public void ResetToGameplay()
    {
        score = 0;
        state = GameState.MENU;
        print(state + " BEFORE THE CHANGE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadGameScene()
    {
        screen.menu.gameObject.SetActive(false);
        screen.overlay.gameObject.SetActive(true);
        isRunning = true;
        player1.isRunning = true;
        state = GameState.GAMEPLAY;
        camera.stage = CameraState.GAMEPLAY;

        audio.Stop("Main Menu Theme");
        audio.Play("Savvy Server");
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
            names = data.name;
            times = data.time;
            scores = data.score;
        }

        screen.leaderboard.DisplayScore();
        screen.leaderboard.OrganiseScore();
        screen.leaderboard.gameObject.SetActive(true);

        screen.leaderboard.isEnteringName = isEntering;

        if (isEntering)
        {
            screen.leaderboard.backTab.SetActive(false);
            screen.leaderboard.enterTab.SetActive(true);
            screen.leaderboard.replayTab.SetActive(false);
        }
        else
        {
            screen.leaderboard.backTab.SetActive(true);
            screen.leaderboard.enterTab.SetActive(false);
            screen.leaderboard.replayTab.SetActive(false);
        }
    }

    public void ResetToMenu()
    {
        score = 0;
        state = GameState.MAIN_MENU;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player1.isRunning = false;
    }

    public void LoadToMenu()
    {
        // This function is to be used when you want to go from a menu to main menu without resetting the scene
        screen.menu.gameObject.SetActive(true);
        screen.overlay.gameObject.SetActive(false);
        screen.leaderboard.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("YOU QUIT HERE");
    }
}