using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Linker between hand/deck/discard
/// </summary>
public class CombatData : MonoBehaviour
{
	public Deck deck;
	public Deck discard;
	public Hand hand;
	public Scale scale;
    public int Imagination;
	public readonly int maximumImagination = 5;
	
    public AnimationScript_Scale scaleAction;

    void Awake()
    {
        scaleAction = GameObject.FindGameObjectWithTag("Scale").GetComponent<AnimationScript_Scale>();
    }
	public void DrawCards(int amt = 1)
	{
		for (int i = 0; i < amt; i++)
        {
            if (deck.IsEmpty())
            {
                Reshuffle();
            }
            if (deck.IsEmpty())
            {
                continue;
            }
            if (hand.CardsInHand() < hand.maxCards)
            {
                var drawCard = deck.DrawCard();
                hand.AddCard(drawCard);
            }
			else
			{
                MillCard(deck.TopCard());
			}
		}
	}

	public void Reshuffle()
	{
		for (int i = 0; i < discard.Size();)
		{
			deck.AddCard(discard[i]);
			discard.RemoveCard(discard[i]);
		}
		deck.Shuffle();
	}

	public void DiscardCard(Card card)
	{
		if(hand.GetCards().Contains(card))
        {
            hand.RemoveCard(card);
			discard.AddCard(card);
		}
	}

	// Possibly temporary function. Here to enable discarding through a Unity button.
	public void DiscardFirst()
	{
		if (hand.GetCards().Count <= 0) return;
		Card card = hand.GetCards()[0];
		hand.RemoveCard(card);
		discard.AddCard(card);
	}

	public void MillCard(Card card)
	{
		if (deck.GetCards().Contains(card))
		{
			deck.RemoveCard(card);
			discard.AddCard(card);
		}
	}
	public virtual void DealDamage(int amount) { }
}
