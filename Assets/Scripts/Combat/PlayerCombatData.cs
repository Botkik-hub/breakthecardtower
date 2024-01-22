using UnityEngine;
/// <summary>
/// Combat data suitable for player
/// </summary>
public class PlayerCombatData : CombatData
{
    [SerializeField] string deckName;
    // Start is called before the first frame update
    private void Start()
    {
        //Hand = GameObject.Find("Hand");

        // Should restore saved deck or create generic if first battle
        DeckGenerator.GenerateDeck(deck, DeckLoader.Instance().GetDeck(deckName), this);
        deck.Shuffle();
    }

    public void CreateDeck(string[] cardNames)
    {
        if (cardNames == null || cardNames.Length == 0)
        {
            DeckGenerator.GenerateDeck(deck, DeckLoader.Instance().GetDeck("Generic Deck"), this);
            deck.Shuffle();
            return;
        }
        
        DeckGenerator.GenerateDeck(deck, cardNames, this);
        deck.Shuffle();
    }
    
    public override void DealDamage(int amount)
    {
        scale.PlayerDealsDamage(amount);
    }
}
