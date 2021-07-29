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
    public bool isPaused;//if freezing time doesnt work then we can just use this

    public GameState gameState;

    public PlayerBehaviours1 player;

    public GameObject menuScreen;
    public GameObject gameOverlay;
    public Transform OrderMenu;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject customers;

    public TextMeshProUGUI countdownText;
    public Slider slider;

    int tick = 0;

    public float countdown = 100;
    public static float score = 0;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = true;
        Time.timeScale = 0;// idk if this might break something, just here to stop most parts of the game from playing.
        //menuScreen.SetActive(true);
        //customer = Instantiate(customerPrefab, customerPrefab.GetComponent<CustomerController>().spawnPos.position, Quaternion.identity);
        slider.maxValue = 15;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

            tick++;

            slider.value = score;
            countdown -= Time.deltaTime;
            countdownText.text = Mathf.Round(countdown).ToString();

            if (countdown <= 0)
            {
                gameOverlay.SetActive(false);

                if (score >= slider.maxValue)
                {
                    print("You Win");
                    winScreen.SetActive(true);
                }
                else
                {
                    print("You Lose");
                    loseScreen.SetActive(true);
                }
            }


        }
    }

    public void Reset()
    {
        score = 0;
        countdown = 100;
        gameOverlay.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        /*for (int i = 0; i < customers.transform.childCount; i++)
        {

            Destroy(customers.transform.GetChild(i));
        }*/
    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        menuScreen.SetActive(false);
        gameOverlay.SetActive(true);
        isPaused = false; 
    }
}
