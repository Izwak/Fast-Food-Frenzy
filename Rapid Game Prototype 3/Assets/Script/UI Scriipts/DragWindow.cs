using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    public bool isBeingDragged;

    public bool hasDropped = false;
    public bool justMoved = false;

    public RectTransform dragRectTrans;

    public Transform border;

    public void OnBeginDrag(PointerEventData eventData)
    {
        BurgerMinigame.selected = this;

        isBeingDragged = true;
        justMoved = true;

        border.GetComponent<Image>().color = new Color(0, 1, 0, 1);
        border.SetParent(transform);
        border.SetAsFirstSibling();

        #region
        DragWindow ingredient = transform.GetChild(transform.childCount - 1).GetComponent<DragWindow>();

        if (transform.childCount > 0 && ingredient != null)
        {
            ingredient.border.GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }

        Transform nextIngredient = transform.GetChild(transform.childCount - 1);

        do
        {
            DragWindow dragNext = nextIngredient.GetComponent<DragWindow>();

            if (dragNext == null)
            {
                break;
            }

            dragNext.border.GetComponent<Image>().color = new Color(0, 1, 0, 1);
            dragNext.border.SetParent(transform);
            dragNext.border.SetAsFirstSibling();

            nextIngredient = nextIngredient.GetChild(nextIngredient.childCount - 1);

        } while (true);
        #endregion
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTrans.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isBeingDragged = false;
        hasDropped = true;

        border.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        // Turn off borders for stacked ingredients
        DragWindow nextIngredient = transform.GetChild(transform.childCount - 1).GetComponent<DragWindow>();

        while (nextIngredient != null)
        {
            nextIngredient.border.GetComponent<Image>().color = new Color(1, 1, 1, 0);


            nextIngredient = nextIngredient.transform.GetChild(nextIngredient.transform.childCount - 1).GetComponent<DragWindow>();
        }
/*
        // Turn off borders
        for (int i = 0; i < transform.childCount; i++)
        {
            FollowTheBoi border = transform.GetChild(i).GetComponent<FollowTheBoi>();
            Image image = transform.GetChild(i).GetComponent<Image>();

            if (border != null)
                image.color = new Color(1, 1, 1, 0); // Sets it to invisible
        }*/
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTrans.SetAsLastSibling();
    }
}
