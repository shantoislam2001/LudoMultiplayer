using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayer : PlayerPice
{
    public RollingDice greenRollingDice;
    // Start is called before the first frame update
    void Start()
    {
        greenRollingDice = GetComponentInParent<GreenHome>().RollingDice;
    }

    private void OnMouseDown()
    {
        if (GameManager.gm.RollingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.RollingDice == greenRollingDice && GameManager.gm.numberOfStepsToMove == 6)
                {
                    GameManager.gm.greenOutPlayer += 1;
                    makePlayerReadyToMove(pathMain.greenPath);
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.RollingDice == greenRollingDice && isReady)
            {
                  moveSteps(pathMain.greenPath);
            }
            
        }
           
    }

    public void selfClick()
    {
        if (GameManager.gm.RollingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.RollingDice == greenRollingDice && GameManager.gm.numberOfStepsToMove == 6)
                {
                    GameManager.gm.greenOutPlayer += 1;
                    makePlayerReadyToMove(pathMain.greenPath);
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.RollingDice == greenRollingDice && isReady)
            {
                moveSteps(pathMain.greenPath);
            }

        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
