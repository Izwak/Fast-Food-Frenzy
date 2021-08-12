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
            backTab.SetActive(false);
        }
        else
        {
            enterTab.SetActive(false);
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

    public void AddScore(float time)
    {

        if (inputField.text != "")
        {
            GameObject score = Instantiate(scorePrefab, scoreTab.transform);
            score.transform.localPosition = new Vector3(0, (scoreTab.transform.childCount - 1) * -100, 0);

            Score scoreScore = score.GetComponent<Score>();
            scoreScore.posText.text = scoreTab.transform.childCount.ToString();
            scoreScore.nameText.text = inputField.text;
            scoreScore.timeText.text = time.ToString();

            gameManager.name.Add(inputField.text);
            gameManager.time.Add(time);

            isEnteringName = false;

            inputField.text = "";
        }
    }
}
