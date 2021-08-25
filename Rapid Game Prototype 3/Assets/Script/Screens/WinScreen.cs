using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public GameObject confetti;

    public GameObject button;
    public TextMeshProUGUI rating;

    public GameObject[] wordTexts;
    public TextMeshProUGUI[] numberTexts;

    bool doneTheThingYet = false;

    // Start is called before the first frame update
    void Start()
    {
        confetti.SetActive(false);
        button.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            wordTexts[i].SetActive(false);
            numberTexts[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!doneTheThingYet) // Yes i am a amazing programmer
        {
            StartCoroutine(slowlyDisplayText());
        }
    }

    void SetText(TextMeshProUGUI backText, string text)
    {
        TextMeshProUGUI frontText = backText.gameObject.GetComponent<CopyText>().targetText;

        frontText.text = text;
        backText.text = text;
    }

    string SetLetterGrade(int score)
    {
        if (score >= 2500)
        {
            return "A+";
        }
        else if (score >= 2000)
        {
            return "A";
        }
        else if (score >= 1500)
        {
            return "B";
        }
        else if (score >= 1000)
        {
            return "C";
        }
        else if (score >= 500)
        {
            return "D";
        }
        else if (score >= 0)
        {
            return "F";
        }
        else if (score >= -200)
        {
            return "Bad";
        }
        else
        {
            return "Really Bad";
        }
    }

    IEnumerator slowlyDisplayText()
    {
        doneTheThingYet = true;

        // Do the calculations
        int served = GameManager.Instance.numCustomersServed;
        int pissed = GameManager.Instance.numCustomersPissed;
        int wasted = GameManager.Instance.numFoodWasted;
        float timer = GameManager.Instance.timer;

        SetText(numberTexts[0], served + " x 100");
        SetText(numberTexts[1], pissed + " x -50");
        SetText(numberTexts[2], wasted + " x -25");

        if (timer <= 0)
            timer = 0;

        int seconds = Mathf.FloorToInt(timer);
        int millisec = Mathf.FloorToInt((timer * 10) % 10);

        if (timer >= 60)
            SetText(numberTexts[3], string.Format("{0}.{1} x 10", seconds, millisec));
        else
            SetText(numberTexts[3], string.Format("{0}.{1} x 10", seconds, millisec));

        float score = served * 100 - pissed * 50 - wasted * 25 + timer * 10;

        SetText(numberTexts[4], "0");
        SetText(rating, SetLetterGrade((int)score));

        GameManager.Instance.finalScore = (int)score;


        // Display the actions slowly
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 5; i++)
        {
            wordTexts[i].SetActive(true);
            numberTexts[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(.5f);
        }

        int finalScore = 0;

        if (GameManager.Instance.finalScore > 0)
        {
            print("Positive");
            while (finalScore < GameManager.Instance.finalScore)
            {
                finalScore += 5;

                if (finalScore > GameManager.Instance.finalScore)
                    finalScore = GameManager.Instance.finalScore;

                int thousands = Mathf.FloorToInt(finalScore / 1000);
                int hundreds = Mathf.FloorToInt(finalScore % 1000);

                if (finalScore >= 1000)
                    SetText(numberTexts[4], string.Format("{0},{1:000}", thousands, hundreds));
                else
                    SetText(numberTexts[4], hundreds.ToString());

                yield return null;
            }
        }
        else if (GameManager.Instance.finalScore < 0)
        {
            print("Negative");
            while (finalScore > GameManager.Instance.finalScore)
            {
                finalScore -= 5;

                if (finalScore < GameManager.Instance.finalScore)
                    finalScore = GameManager.Instance.finalScore;

                int thousands = Mathf.FloorToInt(finalScore / 1000);
                int hundreds = Mathf.FloorToInt(finalScore % 1000);

                if (finalScore <= -1000)
                    SetText(numberTexts[4], string.Format("{0},{1:000}", thousands, hundreds));
                else
                    SetText(numberTexts[4], hundreds.ToString());

                yield return null;
            } 
        }

        yield return new WaitForSeconds(.1f);

        button.SetActive(true);
        confetti.SetActive(true);
        rating.gameObject.SetActive(true);
    }

}
