using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container for cards in hand
/// </summary>
public class Hand : MonoBehaviour
{
    private readonly List<Card> _cardsInHand = new List<Card>();
    public int maxCards = 8;

    public virtual void AddCard(Card card)
    {
        if(_cardsInHand.Count < maxCards)
        {
            _cardsInHand.Add(card);
            card.AlignTransform(transform);
        }
    }

    public virtual void RemoveCard(Card card)
    {
        _cardsInHand.Remove(card);
    }

    public List<Card> GetCards()
    {
        return _cardsInHand;
    }

    public int CardsInHand() { return _cardsInHand.Count; }

    public Card ContainsCardOfType(ECardType type)
    {
        foreach (Card card in _cardsInHand)
        {
            if(card.BaseInfo.cardType== type) return card;
        }
        return null;
    }
}
