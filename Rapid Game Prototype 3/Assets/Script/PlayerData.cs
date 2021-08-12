using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<string> name;
    public List<string> time;

    public PlayerData(GameManager gameManager)
    {
        name = gameManager.name;
        time = gameManager.time;
    }
}
