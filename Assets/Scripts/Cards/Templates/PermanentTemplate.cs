using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Template for all Permanent cards 
/// </summary>
[CreateAssetMenu(fileName = "New Permanent", menuName = "Cards/Permanent Template")]
public class PermanentTemplate : CardInfo
{
    [Serializable] public class UDictEffectInt : UDictionary<EEffectType, int> { }
    [Space]
	public EPermanentType Type;
	public int HexCost;
    public Sprite hexUI;
	public EHexType HexElement;
	public Material PlayedMaterial;

	[Space]
    [UDictionary.Split(80, 20)]
    public UDictEffectInt activateEffects;
    [Space]
    [UDictionary.Split(80, 20)]
    public UDictEffectInt auraEffects;
    [Space]
    [UDictionary.Split(80, 20)]
    public UDictEffectInt demolishEffects;
    PermanentTemplate() 
	{
		cardType = ECardType.Permanent;
	}
}