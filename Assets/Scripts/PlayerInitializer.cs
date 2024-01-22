using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private DeckTemplate genericDeck;
    
    public void CreatePlayer()
    {
        var deckSaver = GetComponent<DeckSaver>();
        List<string> cardNames = new List<string>();
        foreach (var cardPair in genericDeck.cardsList)
        {
            var cardName = cardPair.Key;
            var count = cardPair.Value;
            for (int i = 0; i < count; i++)
            {
                cardNames.Add(cardName);
            }
        }
        deckSaver.SaveDeckAsString(cardNames.ToArray());
        var mapSaver = GetComponent<MapSaver>();
        mapSaver.SaveMap();
    }
}
