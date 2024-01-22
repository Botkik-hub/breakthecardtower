using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// Needed to draw scales on the test scale scene
/// </summary>
public class Scale3DTest : MonoBehaviour, IScalePresenter
{
    [SerializeField] private Transform scaleTop;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private TMP_Text differenceText;
    [Space]
    [SerializeField] private GameObject pointPrefab;
    
    private const float MaxRotation = 35.0f;

    private float _currentRotation;

    private int _winAmount;
    private int _loseAmount;

    private int _currentPlayerAmount = 0;
    private int _currentEnemyAmount = 0;

    private readonly List<GameObject> _pointsLeft = new List<GameObject>();
    private readonly List<GameObject> _pointsRight = new List<GameObject>();

    public void PlayerDamageAdded(int amount)
    {
        StartCoroutine(SpawnPoints(amount, rightSpawnPoint));

        _currentPlayerAmount += amount;
        Rotate();
    }

    public void EnemyDamageAdded(int amount)
    {
        StartCoroutine(SpawnPoints(amount, leftSpawnPoint));

        _currentEnemyAmount += amount;
        Rotate();
    }

    public void SetDifference(int difference)
    {
        differenceText.SetText(difference.ToString());
        if (difference == 0)
        {
            differenceText.color = Color.white;
        }
        else if (difference > 0)
        {
            differenceText.color = Color.green;
        }
        else
        {
            differenceText.color = Color.red;
        }
    }

    public void SetState(int playerAmount, int enemyAmount)
    {
        foreach (var pointLeft in _pointsLeft)
        {
            Destroy(pointLeft);
        }
        _pointsLeft.Clear();

        foreach (var pointLeft in _pointsRight)
        {
            Destroy(pointLeft);
        }
        _pointsRight.Clear();

        _currentEnemyAmount = 0;
        _currentPlayerAmount = 0;
        
        PlayerDamageAdded(playerAmount);
        EnemyDamageAdded(enemyAmount);

        Rotate();
    }

    public void SetWinAmount(int amount)
    {
        _winAmount = amount;
    }

    public void SetLoseAmount(int amount)
    {
        _loseAmount = amount;
    }

    private void Rotate()
    {
        var difference = _currentPlayerAmount - _currentEnemyAmount;
        if (difference == 0)
        {
            RotateToMiddle();
            return;
        }

        if (difference > 0)
        {
            RotatePlayerSide();
        }
        else
        {
            RotateEnemySide();
        }
    }

    private void RotatePlayerSide()
    {
        var difference = _currentPlayerAmount - _currentEnemyAmount;

        var percent = (float)difference / (float)_winAmount;

        var addedRotation = percent * MaxRotation;
 
        addedRotation = Math.Clamp(addedRotation, -MaxRotation, MaxRotation);
        scaleTop.localRotation = Quaternion.Euler(90 - addedRotation, 0, 0);
    }

    private void RotateEnemySide()
    {
        var difference = _currentPlayerAmount - _currentEnemyAmount;

        var percent = (float)difference / (float)_loseAmount;

        var addedRotation = percent * MaxRotation;
        
        addedRotation = Math.Clamp(addedRotation, -MaxRotation, MaxRotation);
        scaleTop.localRotation = Quaternion.Euler(90 + addedRotation, 0, 0);
    }

    private void RotateToMiddle()
    {
        scaleTop.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private IEnumerator SpawnPoints(int amount, Transform position)
    {
        for (int i = 0; i < amount; i++)
        {
            _pointsLeft.Add(Instantiate(pointPrefab, position));
            yield return new WaitForSeconds(0.5f);
        }
    }
}
