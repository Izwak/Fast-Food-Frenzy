using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

enum Page
{
    PAGE,
    ORDERS,
    TRAY,
    BURGERS,
    ICECREAMLOL,
}

public class HelpScreen : MonoBehaviour
{
    public GameObject page;
    public GameObject order;
    public GameObject tray;
    public GameObject burger;
    public GameObject iceCream;

    public GameObject[] buttons;

    public void SetPage(int pageNum)
    {
        Page pageType = (Page)pageNum;

        page.SetActive(false);
        order.SetActive(false);
        tray.SetActive(false);
        burger.SetActive(false);
        iceCream.SetActive(false);

        foreach (GameObject button in buttons)
        {
            //button.GetComponent<Image>().color = Color.gray;
            Debug.Log("Test");
        }

        buttons[pageNum].GetComponent<Image>().color = Color.white;
        EventSystem.current.SetSelectedGameObject(buttons[pageNum]);

        if (pageType == Page.PAGE)
        {
            page.SetActive(true);
        }
        else if (pageType == Page.ORDERS)
        {
            order.SetActive(true);
        }
        else if (pageType == Page.TRAY)
        {
            tray.SetActive(true);
        }
        else if (pageType == Page.BURGERS)
        {
            burger.SetActive(true);
        }
        else if (pageType == Page.ICECREAMLOL)
        {
            iceCream.SetActive(true);
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.screen.menu.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(GameManager.Instance.screen.menu.eventButtons[0]);
    }

    public void Open(int openingPage)
    {
        /*if (GameManager.state == GameState.MAIN_MENU)
        {
            this.gameObject.SetActive(true);
            GameManager.Instance.screen.menu.gameObject.SetActive(false);

            SetPage(openingPage);
        }
        else if (GameManager.state == GameState.GAMEPLAY)
        {
            this.gameObject.SetActive(true);
            GameManager.Instance.screen.menu.gameObject.SetActive(false);

            SetPage(openingPage);
        }*/

        this.gameObject.SetActive(true);
        GameManager.Instance.screen.menu.gameObject.SetActive(false);

        SetPage(openingPage);
    }
}
