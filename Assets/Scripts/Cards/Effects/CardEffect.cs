using UnityEngine;


/// <summary>
///  Template for effects
///  Children should implement ExecuteEffect() and set NeedTarget in constructor
///  When assign to the card in code need to call SetData()
///  Cards use not effects itself, but the copies of this effects
/// </summary>
public abstract class CardEffect : ScriptableObject
{
    // You can give that the signature (parameters and return type) you want/need of course
    // Every type inherited from this HAS TO implement this method
    public bool NeedsTarget { get; protected set; }

    protected CombatData Data;
    protected Card Owner;
    
    public abstract void ExecuteEffect();

    public void SetData(CombatData data, Card card)
    {
        Data = data;
        Owner = card;
    }

}