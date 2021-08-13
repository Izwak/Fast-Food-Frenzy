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
        if (isEnteringName)
        {
            enterTab.SetActive(true);
            replayTab.SetActive(false);
            backTab.SetActive(false);
        }
        else
        {
            enterTab.SetActive(false);
            replayTab.SetActive(false);
            backTab.SetActive(true);
        }
    }

    public void DisplayScore()
    {
        if (gameManager.name.Count > 0)
        {
            for (int i = 0; i < gameManager.name.Count; i++)
            {
                GameObject score = Instantiate(scorePrefab, scoreTab.transform);
                score.transform.localPosition = new Vector3(0, (scoreTab.transform.childCount - 1) * -100, 0);

                Score scoreScore = score.GetComponent<Score>();
                scoreScore.posText.text = scoreTab.transform.childCount.ToString();
                scoreScore.nameText.text = gameManager.name[i];
                scoreScore.timeText.text = gameManager.time[i].ToString();
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

            gameManager.name.Add(inputField.text);
            gameManager.time.Add(Mathf.Round(gameManager.timer * 10) / 10.0f);

            isEnteringName = false;

            inputField.text = "";

            OrganiseScore();
            Saving.SaveData(gameManager);
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

                if (gameManager.time[i] < gameManager.time[i - 1])
                {
                    float test = gameManager.time[i];
                    gameManager.time[i] = gameManager.time[i - 1];
                    gameManager.time[i - 1] = test;

                    string test2 = gameManager.name[i];
                    gameManager.name[i] = gameManager.name[i - 1];
                    gameManager.name[i - 1] = test2;


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
}
