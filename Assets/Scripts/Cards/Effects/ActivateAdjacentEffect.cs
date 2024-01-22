using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAdjacentEffect : CardEffect
{
    List<HexActions> targettedHexes;
    int timesToActivate;
    public override void ExecuteEffect()
    {
        ReorderHexes();
        foreach (HexActions hex in targettedHexes)
        {
            if (hex.isOccupied == false) continue;
            for (int i = 0; i < timesToActivate; ++i)
            {
                hex.permanentCard.Activate();
            }
        }
    }

    public void Init(int numTimes)
    {
        timesToActivate = numTimes;
    }

    public void SetTargetHexes(List<HexActions> hexes)
    {
        targettedHexes = hexes;
    }
    void ReorderHexes()
    {
        List<HexActions> newList = new List<HexActions>();
        List<HexActions> conduitsList = new List<HexActions>();
        foreach(HexActions hex in targettedHexes)
        {
            if (!hex.isOccupied)
            {
                newList.Add(hex);
                continue;
            }
            if (hex.permanentCard.BaseInfo.cardName == "Conduit")
            {
                conduitsList.Add(hex);
                continue;
            }
            newList.Add(hex);
        }
        foreach (HexActions hex in conduitsList)
        {
            newList.Add(hex);
        }
        targettedHexes = newList;
    }
}
