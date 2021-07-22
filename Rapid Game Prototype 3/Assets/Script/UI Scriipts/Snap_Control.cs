using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap_Control : MonoBehaviour
{
    public GameManager gameManager;
    public List<Transform> SPoints;
    public List<Transform> DObjects;
    public bool[] correctPos = new bool[6];

    public float SRange = 0.5f;


    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 6; i++) 
        {
            correctPos[i] = false;

            // Starting rand pos
            DObjects[i].transform.position = new Vector3(Random.Range(100, 1820), Random.Range(100, 920), 0);
        }
    }

    private void Update()
    {
        if(Won())
        {
            print("Has Won");

            ResetBoard();
        }

        DoTheThing();
    }

    void DoTheThing2()
    {
        for (int i = 0; i < 6; i++)
        {
            DragWindow dragRect = DObjects[i].GetComponent<DragWindow>();
            Stackable stackable = DObjects[i].GetComponent<Stackable>();

            if (dragRect != null && !dragRect.beingDragged)
            {

                float closestDis = 50;
                int closestElemnt = -1;

                for (int j = 0; j < 6; j++)
                {
                    DragWindow dragRect2 = DObjects[j].GetComponent<DragWindow>();

                    float distance = Vector3.Distance(DObjects[i].transform.position, SPoints[j].transform.position);

                     if (dragRect2 != null && !dragRect2.beingDragged)
                    {
                        if (stackable != null && i + 1 != j)
                        {
                            if (closestDis > distance && stackable.isActive1)
                            {
                                closestDis = distance;
                                closestElemnt = j;
                            }
                        }
                    }
                    

                    if (closestElemnt != -1)
                    {
                        DObjects[i].transform.position = SPoints[closestElemnt].transform.position;
                    }
                }
            }
        }
    }

    void DoTheThing()
    {
        for (int i = 0; i < 6; i++)
        {
            DragWindow dragWin = DObjects[i].GetComponent<DragWindow>();

            // If not being dragged
            if (dragWin != null)
            {
                // Check whether over points
                if (!dragWin.beingDragged)
                {
                    float closestDis = 50;
                    int closestElemnt = -1;

                    for (int j = 0; j < 6; j++)
                    {
                        float distance = Vector3.Distance(DObjects[i].transform.position, SPoints[j].transform.position);

                        Stackable stackable = DObjects[i].GetComponent<Stackable>();

                        if (stackable != null && i + 1 != j)
                        {
                            if (closestDis > distance && stackable.isActive1)
                            {
                                closestDis = distance;
                                closestElemnt = j;
                            }
                        }
                    }

                    if (closestElemnt != -1)
                    {
                        DObjects[i].transform.position = SPoints[closestElemnt].transform.position;

                        if (i == closestElemnt)
                        {
                            correctPos[i] = true;
                        }
                    }
                }
                else
                {
                    correctPos[i] = false;
                }
            }
        }
    }

    bool Won ()
    {
        bool result = true;

        for (int i = 0; i < 6; i++)
        {
            if (!correctPos[i])
            {
                result = false;
            }
        }

        return result;
    }

    void ResetBoard()
    {

        for (int i = 0; i < 6; i++)
        {
            correctPos[i] = false;
            DObjects[i].transform.position = new Vector3(Random.Range(100, 1820), Random.Range(100, 920), 0);
            gameManager.gameState = GameState.GAMEPLAY;
            gameManager.pb1.isRunning = true;
            this.gameObject.SetActive(false);
        }


    }
}
