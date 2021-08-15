using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardScreen : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject scorePrefab;
    public GameObject scoreTab;
    public GameObject enterTab;
    public GameObject replayTab;
    public GameObject backTab;

    public TMP_InputField inputField;

    public bool isEnteringName = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DisplayScore()
    {
        if (gameManager.names.Count > 0)
        {
            if (gameManager.names.Count == scoreTab.transform.childCount)
            {
                print("This should only fire when the score are alredy displayed");
                return;
            }

            for (int i = 0; i < gameManager.names.Count; i++)
            {
                GameObject score = Instantiate(scorePrefab, scoreTab.transform);
                score.transform.localPosition = new Vector3(0, (scoreTab.transform.childCount - 1) * -100, 0);

                Score scoreScore = score.GetComponent<Score>();
                scoreScore.posText.text = (i + 1).ToString();
                scoreScore.nameText.text = gameManager.names[i];
                scoreScore.timeText.text = gameManager.times[i].ToString();
                scoreScore.scoreText.text = gameManager.scores[i].ToString();
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
            scoreScore.timeText.text = (Mathf.Round(gameManager.timer * 10) / 10.0f).ToString();
            scoreScore.scoreText.text = gameManager.finalScore.ToString();

            gameManager.names.Add(inputField.text);
            gameManager.times.Add(Mathf.Round(gameManager.timer * 10) / 10.0f);
            gameManager.scores.Add(gameManager.finalScore);

            enterTab.SetActive(false);
            replayTab.SetActive(true);


            inputField.text = "";

            OrganiseScore();
            Saving.SaveData(gameManager);

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

                if (gameManager.scores[i] > gameManager.scores[i - 1])
                {
                    float test = gameManager.times[i];
                    gameManager.times[i] = gameManager.times[i - 1];
                    gameManager.times[i - 1] = test;

                    string test2 = gameManager.names[i];
                    gameManager.names[i] = gameManager.names[i - 1];
                    gameManager.names[i - 1] = test2;

                    int test3 = gameManager.scores[i];
                    gameManager.scores[i] = gameManager.scores[i - 1];
                    gameManager.scores[i - 1] = test3;


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

    public void SaveAmdReload()
    {
        Saving.SaveData(gameManager);

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
