using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Deal damage to target
/// </summary>
public class DamageEffect : CardEffect
{
    public int damage;

    public void Init(int amount)
    {
        NeedsTarget = false;
        damage = amount;
    }

    public override void ExecuteEffect()
    {
        Data.DealDamage(damage);
    }
}
