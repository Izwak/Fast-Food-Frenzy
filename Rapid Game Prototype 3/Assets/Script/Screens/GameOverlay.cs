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
    public TextMeshProUGUI tipText;

    public int UrMum;

    float tick;


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
    public void waitThenDisplay(string text)
    {
        tick += Time.deltaTime;

        if (tick > .3)
        {
            tipText.gameObject.SetActive(true);
            tipText.text = text;
            tipText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    public void hideTip()
    {
        tick = 0;
        tipText.gameObject.SetActive(false);
    }
}
