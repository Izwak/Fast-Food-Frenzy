using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardScreen : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject scorePrefab;
    public GameObject scorePos;

    public TMP_InputField inputField;


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
        if (gameManager.name.Count > 0)
        {
            print("Display");
            for (int i = 0; i > gameManager.name.Count; i++)
            {
                print("Display 2");
                GameObject score = Instantiate(scorePrefab, scorePos.transform);
                score.transform.localPosition = new Vector3(0, (scorePos.transform.childCount - 1) * -100, 0);

                Score scoreScore = score.GetComponent<Score>();
                scoreScore.posText.text = scorePos.transform.childCount.ToString();
                scoreScore.nameText.text = gameManager.name[i];
            }
        }
    }

    public void AddScore()
    {

        if (inputField.text != "")
        {
            GameObject score = Instantiate(scorePrefab, scorePos.transform);
            score.transform.localPosition = new Vector3(0, (scorePos.transform.childCount - 1) * -100, 0);

            Score scoreScore = score.GetComponent<Score>();
            scoreScore.posText.text = scorePos.transform.childCount.ToString();
            scoreScore.nameText.text = inputField.text;

            gameManager.name.Add(inputField.text);

            inputField.text = "";
        }
    }
}
