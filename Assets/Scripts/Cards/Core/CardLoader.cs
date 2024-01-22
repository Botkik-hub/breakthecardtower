using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Loads all cards (ScriptableObjects) in the game
/// Made a singleton to prevent double loading cards
/// </summary>
public class CardLoader : MonoBehaviour
{
	////////////////////////////////////////////////////////////////////////////////////
	/// Singleton part
	private static CardLoader _instance;

	public static CardLoader Instance()
	{
		return _instance;
	}

	private void MakeSingleton()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this);
		}
		else
		{
			_instance = this;
		}
	}
	
	////////////////////////////////////////////////////////////////////////////////////

	private readonly List<CardInfo> _cards = new List<CardInfo>();
	
	private void Awake()
	{
		MakeSingleton();
		LoadCardInfoPrefabs();
	}
	private void LoadCardInfoPrefabs()
	{
		CardInfo[] gos = Resources.LoadAll<CardInfo>("CardObjects/");
		foreach (CardInfo go in gos)
		{
			_cards.Add(go);
		}
	}

	public List<CardInfo> GetAllLoadedCards()
	{
		return _cards;
	}

	public CardInfo GetCard(string cardName)
	{
		return _cards.Find(e => e.cardName == cardName);
	}
}
