using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEffect : CardEffect
{
    HexActions targettedHex;
    int timesToActivate;
    public override void ExecuteEffect()
    {
        if (targettedHex.isOccupied == false) return;
        for (int i = 0; i < timesToActivate; ++i)
        {
            targettedHex.permanentCard.Activate();
        }
    }

    public void SetTargetHex(HexActions hex)
    {
        targettedHex = hex;
    }

    public void Init(int numTimes)
    {
        timesToActivate = numTimes;
    }
}
