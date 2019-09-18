using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBlue : MachineManager // Machine that trunes the shape into Blue.
{
    int countdownValue = 3;

    IEnumerator timer()
    {
        while (countdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countdownValue--;
        }
        if (countdownValue == 0)
        {
            //produce the new model

            countdownValue = 3;
        }
    }

    public override bool Interact(PlayerControl player)
    {
        throw new System.NotImplementedException();
    }
}
