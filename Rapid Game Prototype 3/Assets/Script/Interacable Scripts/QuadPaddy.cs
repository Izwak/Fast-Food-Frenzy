using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadPaddy : MonoBehaviour
{
    public GameObject[] paddy;

    public int paddyCount = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakePaddy()
    {
        paddyCount--;

        if (paddyCount != 0)
        {
            Destroy(paddy[paddyCount - 1]);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
