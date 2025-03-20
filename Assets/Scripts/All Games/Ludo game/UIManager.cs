using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject gamePanel;
    
    public void game1()
    {
        GameManager.gm.totalPlayerCanPlay = 2;
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameSettings1();
    }

    public void game2()
    {
        GameManager.gm.totalPlayerCanPlay = 3;
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameSettings2();
    }

    public void game3()
    {
        GameManager.gm.totalPlayerCanPlay = 4;
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void game4()
    {
        GameManager.gm.totalPlayerCanPlay = 1;
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameSettings1();
    }

    void gameSettings1()
    {
        hidePlayer(GameManager.gm.redPlayerPice);
        hidePlayer(GameManager.gm.yellowPlayerPice);
    }

    void gameSettings2()
    {
        
        hidePlayer(GameManager.gm.redPlayerPice);
    }

    void hidePlayer(PlayerPice[] playerpice)
    {
        for(int i = 0; i < playerpice.Length; i++)
        {
            playerpice[i].gameObject.SetActive(false);
        }
    }
    

}
