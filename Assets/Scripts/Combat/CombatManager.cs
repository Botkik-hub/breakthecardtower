using TMPro;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manage phases of the game
/// </summary>
public class CombatManager : MonoBehaviour
{
	public static CombatManager instance { get; private set; }
	bool bIsPlayersTurn = false;
	public TMP_Text TurnText;
	public TMP_Text PhaseText;
	public TMP_Text playerImaginationText;
	[HideInInspector] public uint activations;
	public uint maxActivations = 100;
	[HideInInspector] bool crashOnBreak;
	[HideInInspector] public bool canPlay { get; private set; }

	private EPhase _currentPhase = EPhase.End;
	private PlayerCombatData _playerData;
	private EnemyCombatData _enemyData;
	private OnBoardCards _hexBoard;
	private Scale _scale;

	private void Awake()
	{
		if (instance != null && instance != this) { Destroy(this); }
		else { instance = this; }
	}

	public PlayerCombatData GetPlayerData() { return _playerData; }

	public EnemyCombatData GetEnemyData() { return _enemyData; }

	private void Start()
	{
		_playerData = FindObjectOfType<PlayerCombatData>();
		_enemyData = FindObjectOfType<EnemyCombatData>();
		_hexBoard = FindObjectOfType<OnBoardCards>();
		_scale = FindObjectOfType<Scale>();
		StartCoroutine(StartPhases());
	}

	IEnumerator StartPhases()
	{
		yield return new WaitForSeconds(3);
		_playerData.DrawCards(5);
		_playerData.Imagination = 5;
		_enemyData.DrawCards(5);
		_enemyData.Imagination = 5;
		UpdateImaginationText();
		canPlay = true;
		Invoke("AdvancePhase", 1);
	}

	public void AdvancePhaseButton()
    {
		if(!canPlay) return;
        AdvancePhase();
    }

	public void AdvancePhase()
	{
		switch(_currentPhase)
		{
			case EPhase.Draw:
				_currentPhase = EPhase.Main;
				if (!bIsPlayersTurn)
				{
					_enemyData.EnemyAI.TryPlayTerraHex();
					_enemyData.EnemyAI.Invoke("TryPlayPermanent", 3);
					Invoke("AdvancePhase", 4);
				}
				else
				{
					canPlay = true;
				}
				break;
			case EPhase.Main:
				_currentPhase = EPhase.Activation;
				activations = 0;
				List<Permanent> permanents = new List<Permanent>();
				if (bIsPlayersTurn)
				{
					canPlay = false;

					//foreach (Permanent obj in _hexBoard.GetPlayerPermanents().Values)
					//{
					//	permanents.Add(obj);
					//}
                    foreach (KeyValuePair<GameObject, Permanent> pair in _hexBoard.GetPlayerPermanents())
                    {
                        permanents.Add(pair.Value);
                        pair.Key.GetComponent<HexActions>().modelObject.GetComponent<AnimationScript_Nexus>().anim.SetTrigger("IsActivated");

                    }
				}
				else
				{
					//foreach (Permanent obj in _hexBoard.GetEnemyPermanents().Values)
					//{
					//	permanents.Add(obj);
					//}
                    foreach (KeyValuePair<GameObject, Permanent> pair in _hexBoard.GetEnemyPermanents())
                    {
                        permanents.Add(pair.Value);
                        pair.Key.GetComponent<HexActions>().modelObject.GetComponent<AnimationScript_Nexus>().anim.SetTrigger("IsActivated");

                    }
				}
				ConduitsToBack(ref permanents);

				foreach(Permanent obj in permanents)
				{
					obj.Activate();
				}
				
				permanents = null;
				Invoke("AdvancePhase", 1);
				break;
			case EPhase.Activation:
				_currentPhase = EPhase.End;
				Invoke("AdvancePhase", 1);
				break;
			case EPhase.End:
				bIsPlayersTurn = !bIsPlayersTurn;
				_currentPhase = EPhase.Draw;
				if (bIsPlayersTurn)
				{
					if (_playerData.Imagination < _playerData.maximumImagination) _playerData.Imagination++;
					_playerData.DrawCards();
					UpdateImaginationText();
				}
				else
				{
					if(_enemyData.Imagination < _enemyData.maximumImagination) _enemyData.Imagination++;
					_enemyData.DrawCards();
				}
				Invoke("AdvancePhase", 1);
				break;

		}

		PhaseText.text = "Phase: " + _currentPhase.ToString();
		TurnText.text = "Turn: " + (bIsPlayersTurn ? "Player" : "Enemy");
	}

	public void SetCrashToggle(bool val)
	{
		crashOnBreak = val;
	}

	public bool ShouldCrashOnBreak() { return crashOnBreak; }

	public void UpdateImaginationText()
	{
		playerImaginationText.text = _playerData.Imagination.ToString() + "/" + _playerData.maximumImagination.ToString();
	}

	public void ConduitsToBack(ref List<Permanent> list)
	{
		List<Permanent> newList = new List<Permanent>();
		List<Permanent> conduitsList = new List<Permanent>();
		foreach (Permanent p in list)
		{
			if (p.BaseInfo.cardName == "Conduit")
			{
				conduitsList.Add(p);
				continue;
			}
			newList.Add(p);
		}
		foreach (Permanent p in conduitsList)
		{
			newList.Add(p);
		}
		list = newList;
	}
}
