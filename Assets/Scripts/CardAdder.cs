using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardAdder : MonoBehaviour
{
    [SerializeField] private DisplayHand displayHand;

    [SerializeField] private List<string> alwaysSuggestedCards = new List<string>();
    [SerializeField] private List<string> suggestedCards = new List<string>();
    
    private readonly string[] _suggestedCards = new string[3];

    private readonly List<string> _cardNames = new List<string>();

    private bool _cardAdded = false;
    
    private void Start()
    {
        GetDeckFromSave();
        RandomCardsToAdd();
        foreach (var suggestedCard in _suggestedCards)
        {
            var card = (CardFactory.CreateCard(null, suggestedCard));
            card.GetComponent<CardDrag>().isDraggable = false;
            displayHand.AddCard(card);
        }
    }

    private void GetDeckFromSave()
    {
        var cardNames = GetComponent<DeckSaver>().LoadDeckAsString();
        foreach (var cardName in cardNames)
        {
            _cardNames.Add(cardName);
        } 
    }

    private void RandomCardsToAdd()
    {
        int nextCardIndex = 0;
        foreach (var card in alwaysSuggestedCards)
        {
            _suggestedCards[nextCardIndex++] = card;
        }
        
        while (nextCardIndex < 3)
        {
            var randomCard = suggestedCards[UnityEngine.Random.Range(0, suggestedCards.Count)];
            if (Array.IndexOf(_suggestedCards, randomCard) != -1) continue;
            _suggestedCards[nextCardIndex++] = randomCard;
        }
        
        //Shuffle
        for (int i = 0; i < _suggestedCards.Length; i++)
        {
            var randomIndex = UnityEngine.Random.Range(i, _suggestedCards.Length);
            (_suggestedCards[i], _suggestedCards[randomIndex]) = (_suggestedCards[randomIndex], _suggestedCards[i]);
        }
    }
    
    public void ChooseCard(int cardIndex)
    {
        if (_cardAdded) return;
        _cardAdded = true;
        
        if (cardIndex >= 3 || cardIndex < 0) throw new ArgumentException("Card index must be between 0 and 2");
        
        _cardNames.Add(_suggestedCards[cardIndex]);
        StartCoroutine(SwitchScene());
    }
    
    private void OnValidate()
    {
        if (alwaysSuggestedCards.Count > 3)
        {
            var elementsToRemove = alwaysSuggestedCards.Count - 3;
            alwaysSuggestedCards.RemoveRange(3, elementsToRemove);
        }
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<DeckSaver>().SaveDeckAsString(_cardNames.ToArray());
        SceneManager.LoadScene("MapScene");
    }
}