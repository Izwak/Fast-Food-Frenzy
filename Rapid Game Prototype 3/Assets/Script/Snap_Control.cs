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
        }
    }

    private void Update()
    {
        if(won())
        {
            print("Has Won");

            ResetBoard();
        }

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

                        if (closestDis > distance)
                        {
                            closestDis = distance;
                            closestElemnt = j;
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

    int getDragElement(Drag_N_Drop drag)
    {
        int dragElememt = -1;

        for (int i = 0; i < 6; i++)
        {
            if (drag.transform == DObjects[i].transform)
            {
                dragElememt = i;
            }
        }

        return dragElememt;
    }


    bool elementsMatch(Drag_N_Drop drag, Transform snap)
    {
        int dragElememt = -1;
        int snapElememt = -1;

        for (int i = 0; i < 6; i++)
        {
            if (drag.transform == DObjects[i].transform)
            {
                dragElememt = i;
            }

            if (snap == SPoints[i].transform)
            {
                snapElememt = i;
            }
        }


        if (dragElememt == -1 || snapElememt == -1)
        {
            print("Lol error");
        }

        print("Element " + dragElememt + ", " + snapElememt);

        if (dragElememt == snapElememt)
        {
            return true;
        }

        return false;
    }

    bool won ()
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
            DObjects[i].transform.position = new Vector3(Random.RandomRange(100, 1820), Random.RandomRange(100, 920), 0);
            gameManager.gameState = GameState.GAMEPLAY;
            
            this.gameObject.SetActive(false);
        }


    }
}
