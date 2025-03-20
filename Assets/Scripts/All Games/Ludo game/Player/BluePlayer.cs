using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayer : PlayerPice
{
    public RollingDice blueRollingDice;
    // Start is called before the first frame update
    void Start()
    {
        blueRollingDice = GetComponentInParent<BlueHome>().RollingDice;
    }

    private void OnMouseDown()
    {
        if (GameManager.gm.RollingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.RollingDice == blueRollingDice && GameManager.gm.numberOfStepsToMove == 6 )
                {
                    GameManager.gm.blueOutPlayer += 1;
                    makePlayerReadyToMove(pathMain.bluePath);
                    GameManager.gm.numberOfStepsToMove = 0;
                    return;
                }

            }

            if (GameManager.gm.RollingDice == blueRollingDice && isReady && GameManager.gm.canPlayerMove)
            {
                GameManager.gm.canPlayerMove = false;
                moveSteps(pathMain.bluePath);
            }

        }
       
    }

   

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
