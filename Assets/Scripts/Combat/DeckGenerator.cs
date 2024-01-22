using UnityEngine;
/// <summary>
/// Static class, used to generate deck from string[] or DeckType, or other sources 
/// Used to be a deck functionality 
/// </summary>
public class DeckGenerator
{
    // TODO finish this function
    public static void GenerateDeck(Deck deck, DeckTemplate template, CombatData owner)
    {
        CardLoader cardLoader = CardLoader.Instance();
        if(cardLoader == null)
        {
            Debug.Log("No CardManager");
            return;
        }
        deck.ClearDeck();
        foreach (var entry in template.cardsList)
        {
            for(int i = 0; i < entry.Value; ++i)
            {
                var card = CardFactory.CreateCard(owner, cardLoader.GetCard(entry.Key));
                deck.AddCard(card);
            }
        }
    }
    
    public static void GenerateDeck(Deck deck, string[] newCards, CombatData owner)
    {
        CardLoader cardLoader = CardLoader.Instance();
        if (cardLoader == null)
        {
            Debug.Log("No CardManager");
            return;
        } 
        deck.ClearDeck();
        foreach (var cardName in newCards)
        {
            var card = CardFactory.CreateCard(owner, cardName);
            deck.AddCard(card);
            card.transform.SetParent(deck.transform);
        }
    }

}