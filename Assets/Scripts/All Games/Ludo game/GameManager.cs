using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int numberOfStepsToMove;
    public RollingDice RollingDice;
    public bool canPlayerMove = true;
    public List<PathPoint> PlayerOnPathList = new List<PathPoint>();
    public bool canDiceRoll = true;
    public bool transferDice = false;
    public bool selfDice = false;
    public int blueOutPlayer;
    public int greenOutPlayer;
    public int redOutPlayer;
    public int yellowOutPlayer;
    public RollingDice[] manageRollingDice;
    public int blueCompletePlayer;
    public int redCompletePlayer;
    public int greenCompletePlayer;
    public int yellowCompletePlayer;
    public PlayerPice[] bluePlayerPice;
    public PlayerPice[] redPlayerPice;
    public PlayerPice[] grenPlayerPice;
    public PlayerPice[] yellowPlayerPice;
    public int totalPlayerCanPlay;
    public int totalSix = 0;


    private void Awake()
    {
        gm = this;
    }

   public void addPathPoint(PathPoint pathPoint)
    {
        PlayerOnPathList.Add(pathPoint);
    }

    public void removePathPoint(PathPoint pathPoint)
    {
        if(PlayerOnPathList.Contains(pathPoint))
        {
            PlayerOnPathList.Remove(pathPoint);
        } else
        {
            Debug.Log("Path point is not ableable for remove");
        }
    }
    int nextDice;
    public void rollingDiceManager()
    {
       
        if (GameManager.gm.transferDice)
        {
           
            if (GameManager.gm.numberOfStepsToMove != 6)
            {
                shiftDice();
            }
         
            
            GameManager.gm.canDiceRoll = true;
        }
        else
        {
            if(GameManager.gm.selfDice)
            {
                GameManager.gm.selfDice = false;
                GameManager.gm.canDiceRoll = true;
                GameManager.gm.selfRoll();
            }
        }
    }

    public void selfRoll()
    {
        if(GameManager.gm.totalPlayerCanPlay == 1 && GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[2])
        {
            Invoke("roled", 0.6f);
        }
    }

    void roled()
    {
        GameManager.gm.manageRollingDice[2].mouseRoll();
    }

    void shiftDice()
    {
        if(GameManager.gm.totalPlayerCanPlay == 1)
        {
            if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[0])
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(false);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(true);
                passOut(0);
                GameManager.gm.manageRollingDice[2].mouseRoll();
            }
            else
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(true);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(false);
                passOut(2);
            }



        }
        else if (GameManager.gm.totalPlayerCanPlay == 2)
        {
            if(GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[0])
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(false);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(true);
                passOut(0);
            }
            else
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(true);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(false);
                passOut(2);
            }
        }
        else if (GameManager.gm.totalPlayerCanPlay == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 2) { nextDice = 0; } else { nextDice = i + 1; }
                i = passOut(i);
                if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[i])
                {
                    GameManager.gm.manageRollingDice[i].gameObject.SetActive(false);
                    GameManager.gm.manageRollingDice[nextDice].gameObject.SetActive(true);

                }
            }
        }
        else 
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 3) { nextDice = 0; } else { nextDice = i + 1; }
                i = passOut(i);
                if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[i])
                {
                    GameManager.gm.manageRollingDice[i].gameObject.SetActive(false);
                    GameManager.gm.manageRollingDice[nextDice].gameObject.SetActive(true);

                }
            }
        }
    }

    int passOut(int i)
    {
        if(i == 0)
        {
            if(GameManager.gm.blueCompletePlayer ==4)
            {
                return i + 1;
            }
        }
        else if (i == 1)
        {
            if (GameManager.gm.blueCompletePlayer == 4)
            {
                return i + 1;
            }
        }
        else if (i == 2)
        {
            if (GameManager.gm.blueCompletePlayer == 4)
            {
                return i + 1;
            }
        }
        else if (i == 3)
        {
            if (GameManager.gm.blueCompletePlayer == 4)
            {
                return i + 1;
            }
        }



        return i;
    }

}
