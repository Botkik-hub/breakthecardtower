
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NonPermanent class
/// Not finished 
/// TODO: Add ability to play
/// TODO: RemoveEffects
/// TODO: add ability to modify cards and represent it in display
/// </summary>
public class NonPermanent : Card
{
    private NonPermanentTemplate _baseNonPermanent;

    private int _cost;
    private readonly List<CardEffect> _playEffects = new List<CardEffect>();


    public override void SetUp(CombatData owner, CardInfo cardInfo)
    {
        var nonPermanentInfo = cardInfo as NonPermanentTemplate;

        if (nonPermanentInfo == null) throw new ArgumentException("Wrong info");

        base.SetUp(owner, cardInfo);
        _baseNonPermanent = nonPermanentInfo;
        _cost = nonPermanentInfo.Cost;
        foreach (var playEffect in nonPermanentInfo.OnPlayEffects)
        {
            AddEffect(playEffect);
        }
    }

    public void AddEffect(KeyValuePair<EEffectType, int> effect)
    {
        switch (effect.Key)
        {
            case EEffectType.Damage:
                {
                    DamageEffect newEffect = ScriptableObject.CreateInstance("DamageEffect") as DamageEffect;
                    newEffect.Init(effect.Value);
                    _playEffects.Add(newEffect);
                    break;
                }
            case EEffectType.Destroy:
                {
                    DestroyEffect newEffect = ScriptableObject.CreateInstance("DestroyEffect") as DestroyEffect;
                    _playEffects.Add(newEffect);
                    break;
                }
            case EEffectType.Draw:
                {
                    DrawEffect newEffect = ScriptableObject.CreateInstance("DrawEffect") as DrawEffect;
                    newEffect.Init(effect.Value);
                    _playEffects.Add(newEffect);
                    break;
                }
            default: break;
        }
        _playEffects[^1].SetData(CardParent, this);
    }

    public void Play()
    {
        foreach (var playEffect in _playEffects)
        {
            playEffect.ExecuteEffect();
        }
    }

    public void SetEffectHexTargets(HexActions hex)
    {
        foreach (var playEffect in _playEffects)
        {
            if (playEffect is DestroyEffect)
            {
                var effect = (DestroyEffect)playEffect;
                effect.SetTargetHex(hex);
            }
        }
    }
}