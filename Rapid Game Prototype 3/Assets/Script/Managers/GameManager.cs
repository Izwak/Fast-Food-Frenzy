using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

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
    DEAD,
    QUIT,
    GOLD
}

public enum GameMode
{
    BABY,
    NORMAL,
    ENDLESS
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static GameMode Mode = GameMode.BABY;
    public static GameState State = GameState.MAIN_MENU;
    public static bool IceCreamMachineWorking = false; // lol as if
    public static bool IsUsingController = false;

    public GameEnding ending = GameEnding.NONE;


    public bool isRunning = false;

    public PlayerBehaviours1 player1;

    public ScreenManager screen;
    public CameraManager camerBoi;
    public AudioManager audioBoi;

    public float timer = 301;
    public int finalScore;
    public float score = 0; // This is the overlay bar
    public int numCustomersServed;
    public int numCustomersPissed;
    public int numFoodWasted;

    public List<string> names;
    public List<float> times;
    public List<int> scores;

    public Collider buildingHitBox;

    public Material[] skyBoxes;

    public Transform[] sunFacing;

    public Light sun;

    [HideInInspector]
    public int customerSpawnRate;
    [HideInInspector]
    public int orderTimer;
    [HideInInspector]
    public int registerTimer;
    [HideInInspector]
    public int burnTime;
    [HideInInspector]
    public int maxCustomers;

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        screen.leaderboard.gameObject.SetActive(false);
        screen.win.gameObject.SetActive(false);
        screen.win.confetti.SetActive(false);

        if (State == GameState.MAIN_MENU)
        {
            screen.menu.gameObject.SetActive(true);
            screen.overlay.gameObject.SetActive(false);
            isRunning = false;
            player1.isRunning = false;
            audioBoi.Play("Main Menu Theme");
        }
        else if (State == GameState.GAMEPLAY || State == GameState.MENU)
        {
            screen.menu.gameObject.SetActive(false);
            screen.overlay.gameObject.SetActive(true);
            isRunning = true;
            player1.isRunning = true;
            State = GameState.GAMEPLAY;
            audioBoi.Play("Savvy Server");
            camerBoi.stage = CameraState.GAMEPLAY;
            screen.touchUI.gameObject.SetActive(true);
        }
        SetGameMode(Mode);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.GAMEPLAY)
        {
            if (isRunning)
            {
                timer -= Time.deltaTime;

                // Start panicing once u get to 15 seconds
                if (timer < 17.2 && !audioBoi.IsPlaying("Panic"))
                {
                    audioBoi.Play("Panic");
                    print("Number of times this is being run");
                    audioBoi.FadeOut("Savvy Server", 7f);
                }

                // Take you to the Score Screen
                if (timer < 0 || score >= screen.overlay.scoreGoal)
                {
                    isRunning = false;
                    screen.overlay.gameObject.SetActive(false);
                    screen.orders.gameObject.SetActive(false);

                    screen.leaderboard.isEnteringName = true;

                    screen.win.gameObject.SetActive(true);

                    State = GameState.MENU;
                    audioBoi.Stop("Panic");
                    audioBoi.Stop("Savvy Server");

                    audioBoi.Play("YAAAY");
                }

                // Take you to the Fired screen
                else if (score <= -screen.overlay.scoreMin)
                {
                    isRunning = false;
                    screen.overlay.gameObject.SetActive(false);
                    screen.fired.gameObject.SetActive(true);

                    screen.leaderboard.isEnteringName = true;

                    State = GameState.MENU;
                    audioBoi.Stop("Panic");
                    audioBoi.Stop("Savvy Server");


                    audioBoi.Play("YAAAY");
                }

                else if (ending == GameEnding.QUIT)
                {
                    isRunning = false;
                    screen.overlay.gameObject.SetActive(false);
                    screen.orders.gameObject.SetActive(false);
                    screen.quit.gameObject.SetActive(true);

                    State = GameState.MENU;
                    audioBoi.Stop("Panic");
                    audioBoi.Stop("Savvy Server");


                    audioBoi.Play("YAAAY");
                }

                else if (ending == GameEnding.GOLD)
                {
                    isRunning = false;
                    screen.quit.gameObject.SetActive(false);
                    screen.overlay.gameObject.SetActive(false);
                    screen.orders.gameObject.SetActive(false);
                    screen.golden.gameObject.SetActive(true);

                    State = GameState.MENU;
                    audioBoi.Stop("Panic");
                    audioBoi.Stop("Savvy Server");


                    audioBoi.Play("YAAAY");
                }
            }
        }
        else if (State == GameState.MENU)
        {
            if (!audioBoi.IsPlaying("Shift Over"))
            {
                audioBoi.FadeIn("Shift Over", 3);
            }
        }

        if (ending == GameEnding.DEAD)
        {
            isRunning = false;
            screen.overlay.gameObject.SetActive(false);
            screen.orders.gameObject.SetActive(false);
            screen.win.gameObject.SetActive(false);
            screen.fired.gameObject.SetActive(false);
            screen.golden.gameObject.SetActive(false);
            screen.quit.gameObject.SetActive(false);
            screen.touchUI.gameObject.SetActive(false);
            screen.dead.gameObject.SetActive(true);

            State = GameState.MENU;
            audioBoi.Stop("Panic");
            audioBoi.Stop("Savvy Server");


            audioBoi.Play("YAAAY");
        }

        if (Input.touchCount == 4)
        {
            timer = 1;
        }

    }

    public void ResetToGameplay()
    {
        State = GameState.MENU;
        SetGameMode(Mode);
        print(State + " BEFORE THE CHANGE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetToMenu()
    {
        State = GameState.MAIN_MENU;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player1.isRunning = false;
    }

    public void LoadGameScene(int mode)
    {
        SetGameMode((GameMode)mode);
        screen.menu.gameObject.SetActive(false);
        screen.overlay.gameObject.SetActive(true);
        isRunning = true;
        player1.isRunning = true;
        State = GameState.GAMEPLAY;
        camerBoi.stage = CameraState.GAMEPLAY;

        audioBoi.Stop("Main Menu Theme");
        audioBoi.Play("Savvy Server");
        screen.touchUI.gameObject.SetActive(true);
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
            screen.touchUI.gameObject.SetActive(false);
        }
        else
        {
            screen.leaderboard.backTab.SetActive(true);
            screen.leaderboard.enterTab.SetActive(false);
            screen.leaderboard.replayTab.SetActive(false);
        }
    }

    public void LoadToMenu()
    {
        // This function is to be used when you want to go from a menu to main menu without resetting the scene
        screen.menu.gameObject.SetActive(true);
        screen.overlay.gameObject.SetActive(false);
        screen.leaderboard.gameObject.SetActive(false);

        if (IsUsingController)
            EventSystem.current.SetSelectedGameObject(screen.menu.eventButton);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("YOU QUIT HERE");
    }

    public void SetGameMode(GameMode newMode)
    {
        // This is the STATIC varible you idiot don't delete this line its IMPORTANT
        Mode = newMode;

        if (newMode == GameMode.BABY)
        {
            customerSpawnRate = 600;
            orderTimer = 200;
            registerTimer = 60;
            burnTime = 20;
            maxCustomers = 10;

            RenderSettings.skybox = skyBoxes[0];
            sun.transform.rotation = sunFacing[0].rotation;
            sun.color = new Color(255.0f / 255.0f, 244.0f / 255.0f, 214.0f / 255.0f);
            sun.intensity = 0.8f;
        }
        else if (newMode == GameMode.NORMAL)
        {
            customerSpawnRate = 400;
            orderTimer = 100;
            registerTimer = 25;
            burnTime = 10;
            maxCustomers = 15;
            RenderSettings.skybox = skyBoxes[1];
            sun.transform.rotation = sunFacing[1].rotation;
            sun.color = new Color(255.0f / 255.0f, 61.0f / 255.0f, 0);
            sun.intensity = 2f;
        }
    }

    public void LoadPauseMenu()
    {
        screen.pause.gameObject.SetActive(true);
        screen.touchUI.gameObject.SetActive(false);
        screen.overlay.pauseButton.gameObject.SetActive(false);
        audioBoi.GetSound("Savvy Server").source.volume /= 5;
    }

    public void UnloadPauseMenu()
    {
        screen.pause.gameObject.SetActive(false);
        screen.touchUI.gameObject.SetActive(true);
        screen.overlay.pauseButton.gameObject.SetActive(true);
        audioBoi.GetSound("Savvy Server").source.volume *= 5;
    }
}