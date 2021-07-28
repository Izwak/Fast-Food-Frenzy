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
    public bool isPaused;//if freezing time doesnt work then we can just use this

    public GameState gameState;

    public GameObject menuScreen;
    public PlayerBehaviours1 player;

    public Transform OrderMenu;

    int tick = 0;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = true;
        Time.timeScale = 0;// idk if this might break something, just here to stop most parts of the game from playing.
        //menuScreen.SetActive(true);
        //customer = Instantiate(customerPrefab, customerPrefab.GetComponent<CustomerController>().spawnPos.position, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

            tick++;
        }
    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        menuScreen.SetActive(false);
        isPaused = false;
    }
}
