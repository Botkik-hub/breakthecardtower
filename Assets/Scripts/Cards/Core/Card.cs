using System;
using UnityEngine;


/// <summary>
/// This class combines all card scripts:
/// CardInfo - card prefab
/// CardDisplay - display of the card in the scene
/// CombatData - who owns the card
/// CardDrag - script to drag card
///
/// ...
/// can be added more in the future
/// </summary>
public abstract class Card : MonoBehaviour
{
    public CardInfo BaseInfo { get; private set; }
    public CombatData CardParent { get; private set; }
    
    public CardDisplay Display { get; private set; }
    public CardDrag CardDrag { get; private set; }

    public virtual void SetUp(CombatData owner, CardInfo cardInfo)
    {
        Display = GetComponent<CardDisplay>();
        CardDrag = GetComponent<CardDrag>();

        CardParent = owner;
        BaseInfo = cardInfo;

        Display.SetUp(BaseInfo);
    }

    public void AlignTransform(Transform other)
    {
        transform.SetParent(other);
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }
}