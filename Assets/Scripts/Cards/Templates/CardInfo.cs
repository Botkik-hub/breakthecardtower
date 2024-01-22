using UnityEngine;

/// <summary>
/// Common properties for all cards
/// </summary>
public abstract class CardInfo : ScriptableObject
{
    public string cardName;
    public ERarity rarity;
    public ECardType cardType;
    public string description;
    public Sprite artwork;
    public Sprite cardBackground;
}