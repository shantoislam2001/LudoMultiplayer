using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayer : PlayerPice
{
    public RollingDice yellowRollingDice;
    // Start is called before the first frame update
    void Start()
    {
        yellowRollingDice = GetComponentInParent<YellowHome>().RollingDice;
    }

    private void OnMouseDown()
    {
        if (GameManager.gm.RollingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.RollingDice == yellowRollingDice && GameManager.gm.numberOfStepsToMove == 6)
                {
                    GameManager.gm.yellowOutPlayer += 1;
                    makePlayerReadyToMove(pathMain.yellowPath);
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.RollingDice == yellowRollingDice && isReady)
            {
                moveSteps(pathMain.yellowPath);
            }
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
