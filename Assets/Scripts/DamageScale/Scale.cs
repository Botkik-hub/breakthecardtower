using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base damage system, invoke Events in case of win/lose
/// Has no representation at the scene,
/// if needed to be drawn, attach component with IScalePresenter implemented
/// </summary>
public class Scale : MonoBehaviour
{
    [SerializeField] private int winDifference;
    [SerializeField] private int loseDifference;
    [Space]
    [SerializeField] private int startDifference;

    private IScalePresenter _presenter;
    
    private int _difference;
    private int _playerAmount;
    private int _enemyAmount;

    public UnityEvent winEvent;
    public UnityEvent loseEvent;
    

    private void Start()
    {
        //GameObject.FindGameObjectsWithTag("Scale").
        _presenter = GetComponent<IScalePresenter>();
        _presenter?.SetWinAmount(winDifference);
        _presenter?.SetLoseAmount(loseDifference);
       
        SetDifference(startDifference);
    }

    public void ResetScale(bool useStartDifference = false)
    {
        if (useStartDifference)
        {
            SetDifference(startDifference);
        }
        else
        {
            NullifyDifference();
        }
    }

    public void PlayerDealsDamage(int amount)
    {
        if (amount < 0) throw new ArgumentException("Damage cannot be negative");
        _playerAmount += amount;
        _presenter?.PlayerDamageAdded(amount);
        CheckDifference();
    }

    public void EnemyDealsDamage(int amount)
    {
        if (amount < 0) throw new ArgumentException("Damage cannot be negative");
        _enemyAmount += amount;
        _presenter?.EnemyDamageAdded(amount);
        CheckDifference();
    }

    private void CheckDifference()
    {
        var newDifference = _playerAmount - _enemyAmount;
        
        if (_difference == newDifference) return;

        _difference = newDifference;
        
        _presenter?.SetDifference(_difference);
        
        if (_difference >= winDifference)
        {
            winEvent.Invoke();
            return;
        }

        if (_difference <= loseDifference)
        {
            loseEvent.Invoke();
            return;
        }
    }

    private void SetDifference(int difference)
    {
        if (difference == 0)
        {
            NullifyDifference();
            return;
        }
        if (startDifference > 0)
        {
            _playerAmount += startDifference;
            _enemyAmount = 0;
        }
        else if (startDifference < 0)
        {
            _enemyAmount += startDifference;
            _playerAmount = 0;
        }
        _presenter?.SetState(_playerAmount, _enemyAmount);
        CheckDifference();
    }

    private void NullifyDifference()
    {
        _playerAmount = 0;
        _enemyAmount = 0;
        CheckDifference();
    }

    public int GetDifference()
    {
        return _difference;
    }
}
