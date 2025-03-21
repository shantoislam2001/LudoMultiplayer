using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    public Sprite[] number;
    public SpriteRenderer SpriteRenderer;
    public int numberGot;
    public SpriteRenderer diceAanimation;
    Coroutine generateRandomNumber;
    public int outPicese;
    PathMain pathMain;
    PlayerPice[] currentPlayerPice;
    PathPoint[] pathPointToMoveOn;
    public Coroutine movePlayerPice;
    PlayerPice outPlayerPice;
    int maxNumber = 6;

    private void Awake()
    {
        pathMain = FindObjectOfType<PathMain>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        generateRandomNumber = StartCoroutine(rolling());
    }

    public void mouseRoll()
    {
        generateRandomNumber = StartCoroutine(rolling());
    }


    IEnumerator rolling()
    {
        GameManager.gm.transferDice = false;
        yield return new WaitForEndOfFrame();

        if(GameManager.gm.canDiceRoll)
        {
            
            GameManager.gm.canDiceRoll =false;
            SpriteRenderer.gameObject.SetActive(false);
            diceAanimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            if(GameManager.gm.totalSix == 2)
            {
                maxNumber = 5;
            }else
            {
                maxNumber = 6;
            }
            numberGot = Random.Range(0, maxNumber);
            if(numberGot == 6)
            {
                GameManager.gm.totalSix++;
            }
            else
            {
                GameManager.gm.totalSix = 0;
            }
            SpriteRenderer.sprite = number[numberGot];
            numberGot += 1;
            GameManager.gm.numberOfStepsToMove = numberGot;
            GameManager.gm.RollingDice = this;

            SpriteRenderer.gameObject.SetActive(true);
            diceAanimation.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();

            int numbergot = GameManager.gm.numberOfStepsToMove;
            if (playerCannotMove())
            {
                
                yield return new WaitForSeconds(0.5f);
                if (numbergot != 6)
                {
                    GameManager.gm.transferDice = true;
                    
                }
                else
                {
                    GameManager.gm.selfDice = true;
                    
                }
               
            }
            else
            {

                if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[0])
                {
                    outPicese = GameManager.gm.blueOutPlayer;
                }
                else
                if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[1])
                {
                    outPicese = GameManager.gm.yellowOutPlayer;
                }
                else
                if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[2])
                {
                    outPicese = GameManager.gm.greenOutPlayer;
                }
                else
                if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[3])
                {
                    outPicese = GameManager.gm.redOutPlayer;
                }


                if(outPicese == 0 && numbergot != 6)
                {
                    yield return new WaitForSeconds(0.5f);
                    GameManager.gm.transferDice = true;
                }else
                {
                    if(outPicese == 0 && numbergot == 6)
                    {
                        makePlayerReadyToMove(0);
                    }else if(outPicese == 1 && numbergot != 6 && GameManager.gm.canPlayerMove)
                    {
                        int playerPicePosition = chackoutPlayer();
                        if(playerPicePosition >= 0)
                        {
                            GameManager.gm.canPlayerMove = false;
                            movePlayerPice = StartCoroutine(moveStep(playerPicePosition));
                        }else
                        {
                            yield return new WaitForSeconds(0.5f);
                            if (numbergot != 6)
                            {
                                GameManager.gm.transferDice = true;

                            }
                            else
                            {
                                GameManager.gm.selfDice = true;
                                
                            }
                        }
                      
                    }else if (GameManager.gm.totalPlayerCanPlay == 1 && GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[2])
                    {
                        if(numbergot == 6 && outPicese < 4)
                        {
                            makePlayerReadyToMove(outPlayerToMove());
                        }
                        else
                        {
                            int playerPicePosition = chackoutPlayer();
                            if (playerPicePosition >= 0)
                            {
                                GameManager.gm.canPlayerMove = false;
                                movePlayerPice = StartCoroutine(moveStep(playerPicePosition));
                            }
                            else
                            {
                                yield return new WaitForSeconds(0.5f);
                                if (numbergot != 6)
                                {
                                    GameManager.gm.transferDice = true;

                                }
                                else
                                {
                                    GameManager.gm.selfDice = true;

                                }
                            }
                        }
                    }
                    else
                    {
                        if(chackoutPlayer() < 0)
                        {
                            yield return new WaitForSeconds(0.5f);
                            if (numbergot != 6)
                            {
                                GameManager.gm.transferDice = true;

                            }
                            else
                            {
                                GameManager.gm.selfDice = true;

                            }
                        }
                    }
                  
                    

                    
                }


            }
            GameManager.gm.rollingDiceManager();

            if (generateRandomNumber != null)
            {
                StopCoroutine(rolling());
            }
        }

       
    }


    int outPlayerToMove()
    {
        for (int i = 0;i<4; i++)
        {
            if (!GameManager.gm.grenPlayerPice[i].isReady)
            {
                return i;
            }
        }
        return 0;
    }

    public bool playerCannotMove()
    {
        if(outPicese > 0)
        {
            bool canNotMove = false;

            if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[0])
            {
                currentPlayerPice = GameManager.gm.bluePlayerPice;
                pathPointToMoveOn = pathMain.bluePath;
            }
            else
           if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[1])
            {
                currentPlayerPice = GameManager.gm.yellowPlayerPice;
                pathPointToMoveOn = pathMain.yellowPath;
            }
            else
           if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[2])
            {
                currentPlayerPice = GameManager.gm.grenPlayerPice;
                pathPointToMoveOn = pathMain.greenPath;
            }
            else
           if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[3])
            {
                currentPlayerPice = GameManager.gm.redPlayerPice;
                pathPointToMoveOn = pathMain.redPath;
            }

            for (int i = 0; i < currentPlayerPice.Length; i++)
            {
                if (currentPlayerPice[i].isReady)
                {
                    if(isPathPointAbleableToMove(GameManager.gm.numberOfStepsToMove, currentPlayerPice[i].numberOfStepToAlreadyMoved, pathPointToMoveOn))
                    {
                        return false;
                    }
                }
                else
                {
                    if(!canNotMove)
                    {
                        canNotMove = true;  
                    }
                }
            }
            if(canNotMove)
            {
                return true;
            }

        }
        return false;
    }

    public bool isPathPointAbleableToMove(int numberOfStepToMove, int numberOfStepToAlreadyMove, PathPoint[] pathPointToMove)
    {
        if (numberOfStepToMove == 0)
        {
            return false;
        }

        int leftNumberOfPath = pathPointToMove.Length - numberOfStepToAlreadyMove;

        if (leftNumberOfPath >= numberOfStepToMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void makePlayerReadyToMove(int outPlayer)
    {
        if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[0])
        {
            outPlayerPice = GameManager.gm.bluePlayerPice[outPlayer];
            pathPointToMoveOn = pathMain.bluePath;
            GameManager.gm.blueOutPlayer += 1;
        }
        else
              if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[1])
        {
            outPlayerPice = GameManager.gm.yellowPlayerPice[outPlayer];
            pathPointToMoveOn = pathMain.yellowPath;
            GameManager.gm.yellowOutPlayer += 1;
        }
        else
              if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[2])
        {
            outPlayerPice = GameManager.gm.grenPlayerPice[outPlayer];
            pathPointToMoveOn = pathMain.greenPath;
            GameManager.gm.greenOutPlayer += 1;
        }
        else
              if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[3])
        {
            outPlayerPice = GameManager.gm.redPlayerPice[outPlayer];
            pathPointToMoveOn = pathMain.redPath;
            GameManager.gm.redOutPlayer += 1;
        }


        outPlayerPice.isReady = true;
        outPlayerPice.transform.position = pathPointToMoveOn[0].transform.position;
        outPlayerPice.numberOfStepToAlreadyMoved = 1;

        outPlayerPice.previusPathPoint = pathPointToMoveOn[0];
        outPlayerPice.currentPathPoint = pathPointToMoveOn[0];
        outPlayerPice.currentPathPoint.addPlayerPice(outPlayerPice);
        GameManager.gm.addPathPoint(outPlayerPice.currentPathPoint);
        GameManager.gm.canDiceRoll = true;
        GameManager.gm.selfDice = true;
        GameManager.gm.transferDice = false;
        GameManager.gm.numberOfStepsToMove = 0;
        
    }

    IEnumerator MoveToPosition(Transform piece, Vector3 target, float duration)
    {
        Vector3 start = piece.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            piece.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        piece.position = target;
    }



    IEnumerator moveStep(int movePlayer)
    {
        if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[0])
        {
            outPlayerPice = GameManager.gm.bluePlayerPice[movePlayer];
            pathPointToMoveOn = pathMain.bluePath;
            
        }
        else
            if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[1])
        {
            outPlayerPice = GameManager.gm.yellowPlayerPice[movePlayer];
            pathPointToMoveOn = pathMain.yellowPath;
            
        }
        else
            if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[2])
        {
            outPlayerPice = GameManager.gm.grenPlayerPice[movePlayer];
            pathPointToMoveOn = pathMain.greenPath;
            
        }
        else
            if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[3])
        {
            outPlayerPice = GameManager.gm.redPlayerPice[movePlayer];
            pathPointToMoveOn = pathMain.redPath;
            
        }

        GameManager.gm.transferDice = false;
        yield return new WaitForSeconds(0.25f);
        int numberOfStepToMove = GameManager.gm.numberOfStepsToMove;
       
        int stepsToMove = GameManager.gm.numberOfStepsToMove;

        for (int i = 1; i <= stepsToMove; i++)
        {
            int nextIndex = outPlayerPice.numberOfStepToAlreadyMoved + i - 1;
           
            if (isPathPointAbleableToMove(stepsToMove, outPlayerPice.numberOfStepToAlreadyMoved, pathPointToMoveOn))
            {
                Vector3 nextPosition = pathPointToMoveOn[nextIndex].transform.position;
                yield return StartCoroutine(MoveToPosition(outPlayerPice.transform, nextPosition, 0.3f));
            }
        }
        outPlayerPice.currentPathPoint.rescaleAndPostionAllPlayerPice();
        if (isPathPointAbleableToMove(numberOfStepToMove, outPlayerPice.numberOfStepToAlreadyMoved, pathPointToMoveOn))
        {

            outPlayerPice.numberOfStepToAlreadyMoved += numberOfStepToMove;

            GameManager.gm.removePathPoint(outPlayerPice.previusPathPoint);
            outPlayerPice.previusPathPoint.removePlayerPice(outPlayerPice);
            outPlayerPice.currentPathPoint = pathPointToMoveOn[outPlayerPice.numberOfStepToAlreadyMoved - 1];
            if (outPlayerPice.currentPathPoint.addPlayerPice(outPlayerPice))
            {
                if (outPlayerPice.numberOfStepToAlreadyMoved == 57)
                {
                    GameManager.gm.selfDice = true;
                    
                }
                else
                {
                    if (GameManager.gm.numberOfStepsToMove != 6)
                    {
                        // GameManager.gm.selfDice = false;
                        GameManager.gm.transferDice = true;
                    }
                    else
                    {
                        GameManager.gm.selfDice = true;
                        
                        // GameManager.gm.transferDice = false;
                    }
                }
            }
            else
            {
                GameManager.gm.selfDice = true;
                
            }
            GameManager.gm.addPathPoint(outPlayerPice.currentPathPoint);
            outPlayerPice.previusPathPoint = outPlayerPice.currentPathPoint;


            GameManager.gm.numberOfStepsToMove = 0;

        }
        GameManager.gm.canPlayerMove = true;

        GameManager.gm.rollingDiceManager();
        if (movePlayerPice != null)
        {
            StopCoroutine("moveStep");
        }

    }

    int chackoutPlayer()
    {
        if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[0])
        {
            currentPlayerPice = GameManager.gm.bluePlayerPice;
            pathPointToMoveOn = pathMain.bluePath;
        }
        else
         if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[1])
        {
            currentPlayerPice = GameManager.gm.yellowPlayerPice;
            pathPointToMoveOn = pathMain.yellowPath;
        }
        else
         if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[2])
        {
            currentPlayerPice = GameManager.gm.grenPlayerPice;
            pathPointToMoveOn = pathMain.greenPath;
        }
        else
         if (GameManager.gm.RollingDice == GameManager.gm.manageRollingDice[3])
        {
            currentPlayerPice = GameManager.gm.redPlayerPice;
            pathPointToMoveOn = pathMain.redPath;
        }

        for (int i = 0; i < currentPlayerPice.Length; i++)
        {
            if (currentPlayerPice[i].isReady && isPathPointAbleableToMove(GameManager.gm.numberOfStepsToMove, currentPlayerPice[i].numberOfStepToAlreadyMoved, pathPointToMoveOn))
            {
                return i;
            }
        }
        return -1;
    }

}
