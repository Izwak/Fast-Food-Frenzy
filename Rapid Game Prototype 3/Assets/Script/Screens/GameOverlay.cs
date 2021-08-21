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

        timerText.text = FloatToMinutes(gameManager.timer);

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

    public IEnumerator waitThenDisplay2(string text, float wait)
    {
        yield return new WaitForSeconds(wait);

        tipText.gameObject.SetActive(true);
        tipText.text = text;
        tipText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
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

    string FloatToMinutes(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int millisec = Mathf.FloorToInt((time * 10) % 10);

        if (time > 60)
            return string.Format("{0}:{1:00}", minutes, seconds);
        //else if (time > 0)
        else
            return string.Format("{0}.{1}", seconds, millisec);
        //else
        //    return "0.0";
    }
}
