using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : CardEffect
{
    public int damage;

    HexActions targettedHex;

    public override void ExecuteEffect()
    {
        if (targettedHex.isOccupied == false) return;
        OnBoardCards.Instance.RemovePermanent(targettedHex.gameObject);
        targettedHex.RemovePermanent();
    }

    public void SetTargetHex(HexActions hex)
    {
        targettedHex = hex;
    }
}
