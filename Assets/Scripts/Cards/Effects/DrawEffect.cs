using UnityEngine;


/// <summary>
/// Draw card for owner, dont need a target
/// </summary>
public class DrawEffect : CardEffect
{
    public int drawAmount;

    public void Init(int amount)
    {
        NeedsTarget = false;
        drawAmount = amount;
    }
    
    public override void ExecuteEffect()
    {
        Data?.DrawCards(drawAmount);
    }
}