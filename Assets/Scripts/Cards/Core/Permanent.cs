using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permanent card class
/// Not finished 
/// TODO: Add ability to play to the terra hexes
/// TODO: RemoveEffects
/// TODO: add ability to modify cards and represent it in display
/// </summary>
public class Permanent : Card
{
    private PermanentTemplate _baseInfo;
    private int _hexCost;
    private EHexType _elementType;
    private Material _fieldMaterial;
    
    private readonly List<CardEffect> _activateEffects = new List<CardEffect>();
    private readonly List<CardEffect> _auraEffects = new List<CardEffect>();
    private readonly List<CardEffect> _demolishEffects = new List<CardEffect>();

    public List<CardEffect>[] GetEffects()
    {
        List<CardEffect>[] effectLists = { _activateEffects, _auraEffects, _demolishEffects };
        return effectLists;
    }

    public override void SetUp(CombatData owner, CardInfo cardInfo)
    {
        // Cast 
        var permanentInfo = cardInfo as PermanentTemplate;

        if (permanentInfo == null) throw new ArgumentException("Wrong info");

        base.SetUp(owner, cardInfo);

        _baseInfo = permanentInfo;
        _hexCost = permanentInfo.HexCost;
        _elementType = permanentInfo.HexElement;

        foreach (var effect in permanentInfo.activateEffects)
        {
            AddEffect(effect, _activateEffects);
        }
        foreach (var effect in permanentInfo.auraEffects)
        {
            AddEffect(effect, _auraEffects);
        }
        foreach (var effect in permanentInfo.demolishEffects)
        {
            AddEffect(effect, _demolishEffects);
        }
    }

    public void AddEffect(KeyValuePair<EEffectType, int> effect, List<CardEffect> listToAddTo)
    {
        switch (effect.Key)
        {
            case EEffectType.Activate:
                {
                    ActivateEffect newEffect = ScriptableObject.CreateInstance("ActivateEffect") as ActivateEffect;
                    newEffect.Init(effect.Value);
                    listToAddTo.Add(newEffect);
                    break;
                }
            case EEffectType.ActivateAdjacent:
                {
                    ActivateAdjacentEffect newEffect = ScriptableObject.CreateInstance("ActivateAdjacentEffect") as ActivateAdjacentEffect;
                    newEffect.Init(effect.Value);
                    listToAddTo.Add(newEffect);
                    break;
                }
            case EEffectType.Damage:
                {
                    DamageEffect newEffect = ScriptableObject.CreateInstance("DamageEffect") as DamageEffect;
                    newEffect.Init(effect.Value);
                    listToAddTo.Add(newEffect);
                    break;
                }
            case EEffectType.Destroy:
                {
                    //DestroyEffect newEffect = ScriptableObject.CreateInstance("DestroyEffect") as DestroyEffect;
                    //newEffect.Init(effect.Value);
                    //listToAddTo.Add(newEffect);
                    break;
                }
            case EEffectType.Draw:
                {
                    DrawEffect newEffect = ScriptableObject.CreateInstance("DrawEffect") as DrawEffect;
                    newEffect.Init(effect.Value);
                    listToAddTo.Add(newEffect);
                    break;
                }
            default: break;
        }
        listToAddTo[^1].SetData(CardParent, this);
    }

    public int GetCost()
    {
        return _hexCost;
    }

    public EHexType GetHexType()
    {
        return _elementType;
    }

    public void ChangeCost(int newCost)
    {
        if (newCost < 0) throw new ArgumentException("Cost cannot be negative");
        _hexCost = newCost;
        
        // Change cost in display 
        //Display.cost;
    }

    public void Demolish()
    {
        foreach (var demolishEffect in _demolishEffects)
        {
            demolishEffect.ExecuteEffect();
        }
    }

    public void Activate()
    {
        if (!CombatManager.instance.ShouldCrashOnBreak())
        {
            ++CombatManager.instance.activations;
        }
        if (CombatManager.instance.activations > CombatManager.instance.maxActivations) return;
        foreach (var activeateEffect in _activateEffects)
        {
            activeateEffect.ExecuteEffect();
        }
    }
}