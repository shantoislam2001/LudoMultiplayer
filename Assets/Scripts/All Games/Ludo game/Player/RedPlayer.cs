using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayer : PlayerPice
{
    public RollingDice redRollingDice;
    // Start is called before the first frame update
    void Start()
    {
        redRollingDice = GetComponentInParent<RedHome>().RollingDice;
    }

    private void OnMouseDown()
    {
        if (GameManager.gm.RollingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.RollingDice == redRollingDice && GameManager.gm.numberOfStepsToMove == 6)
                {
                    GameManager.gm.redOutPlayer += 1;
                    makePlayerReadyToMove(pathMain.redPath);
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.RollingDice == redRollingDice && isReady)
            {
                moveSteps(pathMain.redPath);
            }
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
