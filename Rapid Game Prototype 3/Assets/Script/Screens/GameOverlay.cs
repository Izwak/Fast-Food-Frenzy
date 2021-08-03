using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverlay : MonoBehaviour
{
    public GameManager gameManager;

    public Slider scoreSlider;
    public Slider scoreNegative;

    public TextMeshProUGUI timerText;

    public int UrMum;

    // Start is called before the first frame update
    void Start()
    {
        scoreSlider.maxValue = gameManager.scoreGoal;
    }

    // Update is called once per frame
    void Update()
    {
        scoreSlider.value = GameManager.score;
        timerText.text = Mathf.Round(gameManager.countdown).ToString();
    }
}
