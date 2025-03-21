using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPice : MonoBehaviour
{
    public bool moveNow;
    public bool isReady;
    public int numberOfStepToMove;
    public int numberOfStepToAlreadyMoved;
    public PathMain pathMain;
    public Coroutine movePlayerPice;
    public PathPoint previusPathPoint;
    public PathPoint currentPathPoint;
    private void Awake()
    {
        pathMain = FindObjectOfType<PathMain>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void makePlayerReadyToMove(PathPoint[] pathPointToMoveOn)
    {
        isReady = true;
        transform.position = pathPointToMoveOn[0].transform.position;
        numberOfStepToAlreadyMoved = 1;

        previusPathPoint = pathPointToMoveOn[0];
        currentPathPoint = pathPointToMoveOn[0];
        currentPathPoint.addPlayerPice(this);
        GameManager.gm.addPathPoint(currentPathPoint);
        GameManager.gm.canDiceRoll = true;
        GameManager.gm.selfDice = true;
        GameManager.gm.transferDice = false;
    }

    public void moveSteps(PathPoint[] pathPointToMoveOn)
    {
       movePlayerPice = StartCoroutine(moveStep(pathPointToMoveOn));
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


    IEnumerator moveStep(PathPoint[] pathPointToMoveOn)
    {
        GameManager.gm.transferDice = false;
        yield return new WaitForSeconds(0.25f);
        numberOfStepToMove = GameManager.gm.numberOfStepsToMove;
       
        int stepsToMove = GameManager.gm.numberOfStepsToMove;

        for (int i = 1; i <= stepsToMove; i++)
        {
            int nextIndex = numberOfStepToAlreadyMoved + i - 1;
           
            if (isPathPointAbleableToMove(stepsToMove, numberOfStepToAlreadyMoved, pathPointToMoveOn))
            {
                Vector3 nextPosition = pathPointToMoveOn[nextIndex].transform.position;
                yield return StartCoroutine(MoveToPosition(transform, nextPosition, 0.3f));
            }
        }
        currentPathPoint.rescaleAndPostionAllPlayerPice();

        if (isPathPointAbleableToMove(numberOfStepToMove, numberOfStepToAlreadyMoved, pathPointToMoveOn))
        {
          
            numberOfStepToAlreadyMoved += numberOfStepToMove;
           
            GameManager.gm.removePathPoint(previusPathPoint);
            previusPathPoint.removePlayerPice(this);
            currentPathPoint = pathPointToMoveOn[numberOfStepToAlreadyMoved - 1];
            if(currentPathPoint.addPlayerPice(this))
            {
                if(numberOfStepToAlreadyMoved == 57)
                {
                    GameManager.gm.selfDice = true;
                }else
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
            }else
            {
                GameManager.gm.selfDice = true;
            }
            GameManager.gm.addPathPoint(currentPathPoint);
            previusPathPoint = currentPathPoint;

         
            GameManager.gm.numberOfStepsToMove = 0;

        }
        GameManager.gm.canPlayerMove = true;
       
        GameManager.gm.rollingDiceManager();
        if (movePlayerPice != null)
        {
            StopCoroutine("moveStep");
        }

    }

    public bool isPathPointAbleableToMove(int numberOfStepToMove,int numberOfStepToAlreadyMove, PathPoint[] pathPointToMove) 
    {
        if(numberOfStepToMove == 0)
        {
            return false;
        }

        int leftNumberOfPath = pathPointToMove.Length - numberOfStepToAlreadyMove;

        if(leftNumberOfPath >= numberOfStepToMove)
        {
            return true;
        }else
        {
            return false;
        }
    }
   
}
