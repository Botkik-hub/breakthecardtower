using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Create card, setup connections
/// </summary>
public class CardFactory
{
	public static Card CreateCard(CombatData owner, string name)
	{
		var cardLoader = CardLoader.Instance();
		var cardInfo = cardLoader.GetCard(name);
		
		if (cardInfo == null) throw new ArgumentException("No card with this name");

		return CreateCard(owner, cardInfo);
	}

	public static Card CreateCard(CombatData owner, CardInfo cardInfo)
	{
		GameObject prefab = Resources.Load<GameObject>("Prefabs/CardPrefab");

		GameObject newCard = GameObject.Instantiate(prefab);

        if (cardInfo.cardName == "Wireframe Explosive")
        {
            newCard.transform.GetChild(0).GetChild(2).GetComponent<Text>().fontSize = 10;
        }

		switch (cardInfo.cardType)
		{
			case ECardType.Permanent:
                newCard.AddComponent<Permanent>();
				break;
			case ECardType.Whimsy:
                newCard.AddComponent<NonPermanent>();
				break;
			case ECardType.TerraHex:
                newCard.AddComponent<TerraHex>();
				break;
		}

		Card card = newCard.GetComponent<Card>();

		card.SetUp(owner, cardInfo);

		return card;
	}
}
