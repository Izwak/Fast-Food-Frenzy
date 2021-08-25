using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardScreen : MonoBehaviour
{
    public GameObject scorePrefab;
    public GameObject scoreTab;
    public GameObject enterTab;
    public GameObject replayTab;
    public GameObject backTab;

    public TMP_InputField inputField;

    public bool isEnteringName = false;

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayScore()
    {
        if (GameManager.Instance.names.Count > 0)
        {
            if (GameManager.Instance.names.Count == scoreTab.transform.childCount)
            {
                print("This should only fire when the score are alredy displayed");
                return;
            }

            for (int i = 0; i < GameManager.Instance.names.Count; i++)
            {
                GameObject score = Instantiate(scorePrefab, scoreTab.transform);
                score.transform.localPosition = new Vector3(0, (scoreTab.transform.childCount - 1) * -100, 0);

                Score scoreScore = score.GetComponent<Score>();
                scoreScore.posText.text = (i + 1).ToString();
                scoreScore.nameText.text = GameManager.Instance.names[i];
                scoreScore.timeText.text = GameManager.Instance.times[i].ToString();
                scoreScore.scoreText.text = GameManager.Instance.scores[i].ToString();
            }
        }
    }

    public void AddScore()
    {

        if (inputField.text != "")
        {
            GameObject score = Instantiate(scorePrefab, scoreTab.transform);
            score.transform.localPosition = new Vector3(0, (scoreTab.transform.childCount - 1) * -100, 0);

            Score scoreScore = score.GetComponent<Score>();
            scoreScore.posText.text = scoreTab.transform.childCount.ToString();
            scoreScore.nameText.text = inputField.text;
            scoreScore.timeText.text = (Mathf.Round(GameManager.Instance.timer * 10) / 10.0f).ToString();
            scoreScore.scoreText.text = GameManager.Instance.finalScore.ToString();

            GameManager.Instance.names.Add(inputField.text);
            GameManager.Instance.times.Add(Mathf.Round(GameManager.Instance.timer * 10) / 10.0f);
            GameManager.Instance.scores.Add(GameManager.Instance.finalScore);

            enterTab.SetActive(false);
            replayTab.SetActive(true);


            inputField.text = "";

            OrganiseScore();
            Saving.SaveData();

            enterTab.SetActive(false);
            replayTab.SetActive(true);
        }
    }

    public void OrganiseScore()
    {
        if (scoreTab.transform.childCount > 1)
        {
            int infiniteLoopCheck = 0;

            for (int i = 1; i < scoreTab.transform.childCount; i++)
            {
                infiniteLoopCheck++;
                if (infiniteLoopCheck > 1000)
                {
                    Debug.LogError("U SUCC AT CODE U DUMBASS");
                    break;
                }

                GameObject score = scoreTab.transform.GetChild(i).gameObject;

                if (GameManager.Instance.scores[i] > GameManager.Instance.scores[i - 1])
                {
                    float test = GameManager.Instance.times[i];
                    GameManager.Instance.times[i] = GameManager.Instance.times[i - 1];
                    GameManager.Instance.times[i - 1] = test;

                    string test2 = GameManager.Instance.names[i];
                    GameManager.Instance.names[i] = GameManager.Instance.names[i - 1];
                    GameManager.Instance.names[i - 1] = test2;

                    int test3 = GameManager.Instance.scores[i];
                    GameManager.Instance.scores[i] = GameManager.Instance.scores[i - 1];
                    GameManager.Instance.scores[i - 1] = test3;


                    score.transform.SetSiblingIndex(i - 1);

                    if (i > 1)
                    {
                       i -= 2;
                    }
                }

            }

            for (int i = 0; i < scoreTab.transform.childCount; i++)
            {
                Score score = scoreTab.transform.GetChild(i).GetComponent<Score>();

                score.posText.text = (i + 1).ToString();
            }
        }
    }

    public void SkipScoring()
    {
        enterTab.SetActive(false);
        replayTab.SetActive(true);
    }
    public void SaveAmdReload()
    {
        Saving.SaveData();

        if (scoreTab.transform.childCount > 0)
        {
            for (int i = scoreTab.transform.childCount - 1; i > - 1; i--)
            {
                Destroy(scoreTab.transform.GetChild(i).gameObject);
            }
        }

        DisplayScore();
    }
}
