using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool paused;//if freezing time doesnt work then we can just use this

    public GameObject menuScreen;

    public GameObject customerPrefab;

    //for testing, can remove
    float timer = 10;

    GameObject customer;
    // Start is called before the first frame update
    void Start()
    {
        paused = true;
        Time.timeScale = 0;// idk if this might break something, just here to stop most parts of the game from playing.
        menuScreen.SetActive(true);
        //customer = Instantiate(customerPrefab, customerPrefab.GetComponent<CustomerController>().spawnPos.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        menuScreen.SetActive(false);
        paused = false;
    }
}
