using UnityEngine;


/// <summary>
/// Base money system with no visual representation
/// </summary>
public class Wallet : MonoBehaviour
{
    private ushort _amount;

    public ushort GetAmount()
    {
        return _amount;
    }

    public void SetAmount(ushort amount)
    {
        _amount = amount;
    }

    public void AddMoney(ushort amount)
    {
        _amount += amount;
    }

    //Used to buy/spend money on smth, return true if successful, false if it impossible to buy 
    public bool TryBuy(ushort cost)
    {
        if (!EnoughMoney(cost)) return false;
        _amount -= cost;
        return true;
    }
    
    //Used to check if Wallet has enough money to buy stuff
    public bool EnoughMoney(ushort moneyToSpend)
    {
        return moneyToSpend >= _amount;
    }
    
    // Reset amount to 0 if amount to lose > then current amount
    public void LoseMoney(ushort amount)
    {
        if (_amount <= amount) 
            _amount = 0;
        else 
            _amount -= amount;
    }
}
