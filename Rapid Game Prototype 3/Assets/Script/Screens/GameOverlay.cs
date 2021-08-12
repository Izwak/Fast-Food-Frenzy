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


    public int scoreGoal;
    public int scoreMin;


    float tick;

    // Start is called before the first frame update
    void Start()
    {
        scoreSlider.maxValue = scoreGoal;
        scoreNegative.maxValue = scoreMin;
    }

    // Update is called once per frame
    void Update()
    {
        scoreSlider.value = GameManager.score;
        scoreNegative.value = -GameManager.score;

        timerText.text = Mathf.Round(gameManager.timer).ToString();

        if (GameManager.score >= 0)
        {
            scoreSlider.gameObject.SetActive(true);
            scoreNegative.gameObject.SetActive(false);
        }
        else if (GameManager.score < 0)
        {
            scoreSlider.gameObject.SetActive(false);
            scoreNegative.gameObject.SetActive(true);
        }
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
