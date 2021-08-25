using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerMinigame : MonoBehaviour
{
    public GameManager gameManager;

    public List<Transform> DObjects;

    public bool[] correctPos;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 1; i < 6; i++)
        {
            correctPos[i] = false;


            // First point stays where it is
            // Starting rand pos
            DObjects[i].transform.localPosition = new Vector3(Random.Range(100, 1820) - Screen.width / 2, Random.Range(100, 920) - Screen.width / 2, 0);
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
        //  - obj1 cannot snap to a child of itself

        JustMoved();

        Snap();

        if (Won())
        {
            //print("Has Won");

            ResetBoard();
            gameManager.screen.touchUI.gameObject.SetActive(true);
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
            DragWindow obj1 = DObjects[i].GetComponent<DragWindow>();


            // Only runs on has drops
            if (obj1 != null && obj1.hasDropped)
            {
                obj1.hasDropped = false; // Immediately switch it off again


                float closestDis = 1f;
                int closestElemnt = -1;

                GameObject stackTop = obj1.gameObject;

                // Find the ingredient on the top of the stack
                while (stackTop.transform.childCount > 1)
                {
                    stackTop = stackTop.transform.GetChild(1).gameObject;
                }

                // Check Obj 2
                for (int j = 0; j < 7; j++)
                {
                    Stackable stackable2 = DObjects[j].GetComponent<Stackable>();

                    // check distances
                    if (stackable2 != null)
                    {
                        float distance = Vector3.Distance(DObjects[i].position, stackable2.snappingPoint.position);

                        // Dont try to stack on itself
                        if (distance < closestDis && i != j && stackable2.isActive1 && DObjects[j].gameObject != stackTop)
                        {
                            closestDis = distance;
                            closestElemnt = j;
                        }
                    }
                }

                // If a closest element candidate is found
                if (closestElemnt != -1)
                {
                    Stackable closeStack = DObjects[closestElemnt].GetComponent<Stackable>();

                    if (closeStack != null)
                    {

                        DObjects[i].SetParent(DObjects[closestElemnt]);
                        DObjects[i].position = closeStack.snappingPoint.position;

                        closeStack.isActive1 = false;
                    }

                    //print("elements " + i + " " + closestElemnt);

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
                DObjects[i].transform.localPosition = new Vector3(Random.Range(100, 1820) - Screen.width / 2, Random.Range(100, 920) - Screen.width / 2, 0);

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

        //gameManager.gameState = GameState.GAMEPLAY;
        gameManager.player1.isRunning = true;
        this.gameObject.SetActive(false);
    }
}
