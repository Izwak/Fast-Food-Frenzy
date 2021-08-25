using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<string> name;
    public List<float> time;
    public List<int> score;

    public PlayerData()
    {
        name = GameManager.Instance.names;
        time = GameManager.Instance.times;
        score = GameManager.Instance.scores;
    }
}
