using System;
using UnityEngine;


/// <summary>
/// No implementation yet
/// </summary>
[CreateAssetMenu(fileName = "New Summon Effect", menuName = "Effect/Summon Effect")]
public class SummonEffect : CardEffect
{
    public SummonEffect()
    {
        NeedsTarget = false;
    }
    
    public override void ExecuteEffect()
    {
        throw new NotImplementedException();
    }
}