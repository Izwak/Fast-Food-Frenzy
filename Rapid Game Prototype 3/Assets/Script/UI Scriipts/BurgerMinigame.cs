using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerMinigame : MonoBehaviour
{
    public GameManager gameManager;

    public List<Transform> DObjects;

    public bool[] correctPos;

    public float SRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 1; i < 6; i++)
        {
            correctPos[i] = false;


            // First point stays where it is
            // Starting rand pos
            DObjects[i + 1].transform.position = new Vector3(Random.Range(100, 1820), Random.Range(100, 920), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When we snap it needs to be
        //  - Obj 1 just dropped 
        //  - connect obj1 to nearest obj2's snapping point within closestDis
        //  - obj 1 is connecting 2 obj2's snapping point
        //  - obj 1 is becomes child of obj 2
        //      - this is so when move obj2 obj1 moves 2
        //      - this is can be broken by moving obj 1
        //  - obj2 can only have 1 child
        //  - obj1 can still have child
        //  - obj2's snapping point becomes obj1's 1
        //      - this can be broken by moving obj1
        //      - if obj1 has no snapping point then so does obj2

        JustMoved();

        Snap();

        if (Won())
        {
            print("Has Won");

            ResetBoard();
        }
    }

    void JustMoved()
    {
        for (int i = 0; i < 7; i++)
        {
            Transform obj1 = DObjects[i];
            DragWindow dragRect1 = DObjects[i].GetComponent<DragWindow>();

            if (obj1 != null && dragRect1 != null && dragRect1.justMoved)
            {
                //print(obj1.name);

                dragRect1.justMoved = false;
                correctPos[i - 1] = false;

                Stackable stackable = obj1.parent.GetComponent<Stackable>();

                if (stackable != null)
                {
                    //print(obj1.parent.name);
                    stackable.isActive1 = true;
                }

                obj1.SetParent(this.transform);
            }
        }
    }


    void Snap()
    {
        // Check for has drops
        for (int i = 0; i < 7; i++)
        {
            DragWindow dragRect1 = DObjects[i].GetComponent<DragWindow>();


            // Only runs on has drops
            if (dragRect1 != null && dragRect1.hasDropped)
            {
                dragRect1.hasDropped = false; // Immediately switch it off again


                float closestDis = 100;
                int closestElemnt = -1;

                // Check Obj 2
                for (int j = 0; j < 7; j++)
                {
                    Stackable stackable2 = DObjects[j].GetComponent<Stackable>();

                    // check distances
                    if (stackable2 != null)
                    {
                        float distance = Vector3.Distance(DObjects[i].position, stackable2.snappingPoint.position);


                        if (distance < closestDis && i != j && stackable2.isActive1)
                        {
                            closestDis = distance;
                            closestElemnt = j;
                        }
                    }
                }

                if (closestElemnt != -1)
                {

                    Stackable closeStack = DObjects[closestElemnt].GetComponent<Stackable>();

                    if (closeStack != null)
                    {

                        DObjects[i].SetParent(DObjects[closestElemnt]);
                        DObjects[i].position = closeStack.snappingPoint.position;

                        closeStack.isActive1 = false;
                    }

                    print("elements " + i + " " + closestElemnt);

                    if (i == closestElemnt + 1)
                    {
                        correctPos[i - 1] = true;
                    }
                }

            }
        }
    }

    bool Won()
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

        for (int i = 0; i < 7; i++)
        {
            DObjects[i].SetParent(this.transform);

            if (i > 0) // Dont Rand first object
                DObjects[i].transform.position = new Vector3(Random.Range(100, 1820), Random.Range(100, 920), 0);

            Stackable stackable = DObjects[i].GetComponent<Stackable>();

            if (stackable != null)
            {
                stackable.isActive1 = true;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            correctPos[i] = false;
        }

        gameManager.gameState = GameState.GAMEPLAY;
        gameManager.pb1.isRunning = true;
        this.gameObject.SetActive(false);
    }
}
