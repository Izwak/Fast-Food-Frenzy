using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameManager gameManager;
    public GameObject page;
    public GameObject order;
    public GameObject tray;
    public GameObject burger;
    public GameObject iceCream;

    public Image[] images;


    public void SetPage(int pageNum)
    {
        Page pageType = (Page)pageNum;

        page.SetActive(false);
        order.SetActive(false);
        tray.SetActive(false);
        burger.SetActive(false);
        iceCream.SetActive(false);

        foreach (Image image in images)
        {
            image.color = Color.gray;
        }

        if (pageType == Page.PAGE)
        {
            page.SetActive(true);
            images[0].color = Color.white;
        }
        else if (pageType == Page.ORDERS)
        {
            order.SetActive(true);
            images[1].color = Color.white;
        }
        else if (pageType == Page.TRAY)
        {
            tray.SetActive(true);
            images[2].color = Color.white;
        }
        else if (pageType == Page.BURGERS)
        {
            burger.SetActive(true);
            images[3].color = Color.white;
        }
        else if (pageType == Page.ICECREAMLOL)
        {
            iceCream.SetActive(true);
            images[4].color = Color.white;
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        gameManager.screen.menu.gameObject.SetActive(true);
    }

    public void Open(int openingPage)
    {
        if (GameManager.state == GameState.MAIN_MENU)
        {
            this.gameObject.SetActive(true);
            gameManager.screen.menu.gameObject.SetActive(false);

            SetPage(openingPage);
        }
        else if (GameManager.state == GameState.GAMEPLAY)
        {
            this.gameObject.SetActive(true);
            gameManager.screen.menu.gameObject.SetActive(false);

            SetPage(openingPage);
        }
    }
}
