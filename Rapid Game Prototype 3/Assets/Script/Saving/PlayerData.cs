using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<string> name;
    public List<float> time;
    public List<int> score;

    public PlayerData(GameManager gameManager)
    {
        name = gameManager.names;
        time = gameManager.times;
        score = gameManager.scores;
    }
}
