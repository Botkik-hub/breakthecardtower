using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Template for the NonPermanent cards
/// </summary>
[CreateAssetMenu(fileName = "New Non Permanent", menuName = "Cards/Non Permanent Template")]
public class NonPermanentTemplate : CardInfo
{
    [Serializable] public class UDictEffectInt : UDictionary<EEffectType, int> { }

    [Space]
    public EWhimsyType Type;
    public int Cost;
    public bool TargetsHex;

    [Space]
    [UDictionary.Split(80, 20)]
    public UDictEffectInt OnPlayEffects;

    NonPermanentTemplate()
    {
        cardType = ECardType.Whimsy;
    }
}