using System;

[Serializable]
public class DeckContainer
{
    private string[] _cards;

    public DeckContainer(PlayerCombatData data)
    {
        if (data == null)
        {
            _cards = new string[] {};
            return;
        }
        _cards = data.deck.GetCardsNames();
    }
    
    public DeckContainer(string[] cardNames)
    {
        _cards = cardNames;
    }
    
    public void RestoreDeck(PlayerCombatData data)
    {
        data.CreateDeck(_cards);
    }
    
    public string[] GetCardsNames()
    {
        return _cards;
    }
}
    
