using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PathMain pathMain;
    public List<PlayerPice> playerPiceList = new List<PlayerPice>();
    public PathPoint[] pathPointToMoveOn;
    // Start is called before the first frame update
    void Start()
    {
        pathMain = GetComponentInParent<PathMain>();
    }

    public bool addPlayerPice(PlayerPice playerPice)
    {
        if(this.name == "CenterPath") { completed(playerPice); }
        if (this.name != "PathPoint" && this.name != "PathPoint (8)" && this.name != "PathPoint (13)" &&
            this.name != "PathPoint (21)" && this.name != "PathPoint (26)" && this.name != "PathPoint (34)" &&
            this.name != "PathPoint (39)" && this.name != "PathPoint (47)" && this.name != "CenterPath") 
        {
            if (playerPiceList.Count == 1)
            {
                string prevPlayerPiceName = playerPiceList[0].name;
                string currentPlayerPiceName = playerPice.name;
                currentPlayerPiceName = currentPlayerPiceName.Substring(0, currentPlayerPiceName.Length - 4);
                if (!prevPlayerPiceName.Contains(currentPlayerPiceName))
                {
                    playerPiceList[0].isReady = false;
                    StartCoroutine(revertOnStart(playerPiceList[0]));

                    playerPiceList[0].numberOfStepToAlreadyMoved = 0;
                    removePlayerPice(playerPiceList[0]);
                    playerPiceList.Add(playerPice);
                    return false;
                }
            }
        }

    addPlayer(playerPice);
        return true;
    }

    public void addPlayer(PlayerPice playerPice)
    {
        playerPiceList.Add(playerPice);
        rescaleAndPostionAllPlayerPice();
    }

    public void removePlayerPice(PlayerPice playerPice)
    {
        if(playerPiceList.Contains(playerPice))
        {
            playerPiceList.Remove(playerPice);
            rescaleAndPostionAllPlayerPice();
        }
    }

    public void rescaleAndPostionAllPlayerPice()
    {
        int piceCount = playerPiceList.Count;
        bool isOdd = (piceCount % 2) == 0?false:true;

        int extent = piceCount / 2;
        int counter = 0;
        int spriteLeayer = 1;

        if(isOdd)
        {
            for(int i = -extent; i <= extent; i++)
            {
                playerPiceList[counter].transform.localScale = new Vector3(pathMain.scale[piceCount - 1], pathMain.scale[piceCount - 1], 1f);
                playerPiceList[counter].transform.position = new Vector3(transform.position.x + (i * pathMain.positionDiffrence[piceCount - 1]), transform.position.y, transform.position.z);
                // playerPiceList[counter].transform.position = new Vector3(transform.position.x, transform.position.y + (i * pathMain.positionDiffrence[piceCount - 1]), transform.position.z);
                counter++;
            }
        }else
        {
            for (int i = -extent; i < extent; i++)
            {
                playerPiceList[counter].transform.localScale = new Vector3(pathMain.scale[piceCount - 1], pathMain.scale[piceCount - 1], 1f);
                  playerPiceList[counter].transform.position = new Vector3(transform.position.x + (i * pathMain.positionDiffrence[piceCount - 1]), transform.position.y,  transform.position.z);
               // playerPiceList[counter].transform.position = new Vector3(transform.position.x , transform.position.y + (i * pathMain.positionDiffrence[piceCount - 1]), transform.position.z);
                counter++;
            }
        }
        for (int i = 0; i < playerPiceList.Count; i++)
        {
            playerPiceList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLeayer;
            spriteLeayer++;
        }
    }

    IEnumerator revertOnStart(PlayerPice playerPice)
    {
        if (playerPice.name.Contains("Blue")) { GameManager.gm.blueOutPlayer -= 1; pathPointToMoveOn = pathMain.bluePath; }
        else if (playerPice.name.Contains("Red")) { GameManager.gm.redOutPlayer -= 1; pathPointToMoveOn = pathMain.redPath; }
        else if (playerPice.name.Contains("Green")) { GameManager.gm.greenOutPlayer -= 1; pathPointToMoveOn = pathMain.greenPath; }
        else if (playerPice.name.Contains("Yellow")) { GameManager.gm.yellowOutPlayer -= 1; pathPointToMoveOn = pathMain.yellowPath; }

        for(int i = playerPice.numberOfStepToAlreadyMoved; i>=0; i--)
        {
            playerPice.transform.position = pathPointToMoveOn[i].transform.position;
            yield return new WaitForSeconds(0.02f);
        }

        playerPice.transform.position = pathMain.BasePath[basePointPosition(playerPice.name)].transform.position;
    }

    int basePointPosition(string name)
    {
       
        for (int i = 0; i < pathMain.BasePath.Length; i++)
        {
            if (pathMain.BasePath[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }

    void completed(PlayerPice playerPice)
    {
        if (playerPice.name.Contains("Blue")) { GameManager.gm.blueCompletePlayer += 1; GameManager.gm.blueOutPlayer -= 1; if (GameManager.gm.blueCompletePlayer == 4) { showSelibration();  } }
        else if (playerPice.name.Contains("Red")) { GameManager.gm.redCompletePlayer += 1; GameManager.gm.redOutPlayer -= 1; if (GameManager.gm.redCompletePlayer == 4) { showSelibration();} }
        else if (playerPice.name.Contains("Green")) { GameManager.gm.greenCompletePlayer += 1; GameManager.gm.greenOutPlayer -= 1; if (GameManager.gm.greenCompletePlayer == 4) { showSelibration();} }
        else if (playerPice.name.Contains("Yellow")) { GameManager.gm.yellowCompletePlayer += 1; GameManager.gm.yellowOutPlayer -= 1; if (GameManager.gm.yellowCompletePlayer == 4) { showSelibration();} }
    }


    void showSelibration()
    {

    }

}
