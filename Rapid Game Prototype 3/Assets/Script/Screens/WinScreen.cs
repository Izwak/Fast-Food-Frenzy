using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public GameObject confetti;

    // Start is called before the first frame update
    void Start()
    {
        confetti.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //confetti.SetActive(true);
    }
}
