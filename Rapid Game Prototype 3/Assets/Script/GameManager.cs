using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    TITLE,
    GAMEPLAY,
    MINIGAME
}

public class GameManager : MonoBehaviour
{
    public bool paused;//if freezing time doesnt work then we can just use this

    public GameState gameState;


    public GameObject menuScreen;

    public GameObject customerPrefab;

    //public PlayerBehaviours1 pb1;

    public

    //for testing, can remove
    float timer = 10;

    GameObject customer;
    // Start is called before the first frame update

    void Start()
    {
        paused = true;
        Time.timeScale = 0;// idk if this might break something, just here to stop most parts of the game from playing.
        //menuScreen.SetActive(true);
        //customer = Instantiate(customerPrefab, customerPrefab.GetComponent<CustomerController>().spawnPos.position, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
/*        if (gameState == GameState.TITLE)
        {
            pb1.isRunning = false;
        }
        if (gameState == GameState.GAMEPLAY)
        {
            pb1.isRunning = true;
        }
        if (gameState == GameState.MINIGAME)
        {
            pb1.isRunning = false;
        }*/
    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        menuScreen.SetActive(false);
        paused = false;
    }
}
