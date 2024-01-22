using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Template for all TeraHexes 
/// </summary>
[CreateAssetMenu(fileName = "New Terra Hex", menuName = "Cards/Terra Hex Template")]
public class TerraHexTemplate : CardInfo
{
	[Space]
	public EHexType HexElement;
	public int MaxTimer;
    public Material material;
    public Material texture;
    public Sprite hexUI;
	[Space] public List<CardEffect> effects;

    TerraHexTemplate() 
	{
		cardType = ECardType.TerraHex;
	}
}