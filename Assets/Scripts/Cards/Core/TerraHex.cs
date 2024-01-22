using System;
using System.Collections.Generic;

/// <summary>
/// TerraHex class
/// Not finished 
/// TODO: Add ability to play to the field
/// </summary>
public class TerraHex : Card
{
	private TerraHexTemplate _baseHex;
	private int _maxTimer;
	private EHexType _hexType;
	
	//private readonly List<CardEffect> _cardEffects = new List<CardEffect>();

	public override void SetUp(CombatData owner, CardInfo cardInfo)
	{
		// Cast 
		var hexInfo = cardInfo as TerraHexTemplate;
		
		if (hexInfo == null) throw new ArgumentException("Wrong info");
		
		base.SetUp(owner, cardInfo);

		_baseHex = hexInfo;
		_maxTimer = hexInfo.MaxTimer;
		_hexType = hexInfo.HexElement;

		//foreach (var effect in _cardEffects)
		//{
		//    AddEffect(effect);
		//}
	}

	//public void AddEffect(CardEffect effect)
	//{
	//    _cardEffects.Add(effect.GetCopy());
	//    _cardEffects[^1].SetData(CardParent);
	//}
}
