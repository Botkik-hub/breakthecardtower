using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

/// <summary>
/// Container for cards, should not contain other functionality
/// TODO Remove view function
/// </summary>
public class Deck : MonoBehaviour
{
	private readonly List<Card> _cards = new List<Card>();
	private DeckViewer _deckViewer;
	private GameObject _cardImage;
	
	private void Start()
	{
		_deckViewer = GetComponent<DeckViewer>();
		_cardImage = Resources.Load<GameObject>("Prefabs/CardImage");
		if(_cardImage == null)
		{
			Debug.Log("CardImage is null");
		}
	}

	public void Shuffle() // Fisher-Yates Shuffle
	{
		for (int i = _cards.Count - 1; i > 0; i--)
		{
			int index = Random.Range(0, i);
			// Easier swap in c#
			(_cards[i], _cards[index]) = (_cards[index], _cards[i]);
		}
	}

	public int Size()
	{
		return _cards.Count;
	}
	
	public bool IsEmpty()
	{
		return !_cards.Any();
	}
	public Card this[int index]
	{
		get { return _cards[index]; }
	}

	public void RemoveCard(Card card)
	{
		_cards.Remove(card);
	}
    public void RemoveTop()
    {
		Card top = _cards[0];
        _cards.Remove(top);
    }

	public Card TopCard()
	{
		return _cards[0];
	}

    /// <summary>
    /// Return top card and remove it from the deck
    /// </summary>
    public Card DrawCard()
    {
	    if (IsEmpty()) throw new DataException("No cards in the deck");
	    
	    var temp = _cards[0];
	    RemoveTop();
	    return temp;
    }
    
    
    public void AddCard(Card card)
	{
		_cards.Add(card);
        card.AlignTransform(transform);
    }
	public void AddCard(Card card, int amt = 1)
	{
		if (amt < 1) throw new ArgumentException("Cannot add less then 1 card");
		for (int i = 0; i < amt; i++)
		{
			_cards.Add(card);
            card.AlignTransform(transform);
        }
	}

	public void ClearDeck()
	{
		_cards.Clear();
	}
	
	public string[] GetCardsNames()
	{
		var cards = new List<string>();
		foreach (var card in this._cards)
		{
			cards.Add(card.BaseInfo.cardName);
		}
		
		return cards.ToArray();
	}

	public List<Card> GetCards() { return _cards; }

    public void SetCardImage(Sprite image)
    {
        GetComponent<SpriteRenderer>().sprite = image;
    }

}
