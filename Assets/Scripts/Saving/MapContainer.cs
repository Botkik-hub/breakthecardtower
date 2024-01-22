using System;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Serializable container for player data
/// contains map position, map history, player money, cards in deck
/// </summary>
[Serializable]
public class MapContainer
{
	private byte _mapPosition;
	private byte[] _mapHistory;
	private int _seed;
	
	public MapContainer(MapPlayer player)
	{
		if (player == null)
		{
			_seed = new Random().Next();
			_mapHistory = new byte[] {};
			_mapPosition = 0;
			return;
		}
		_seed = player.GetSeed();
		_mapHistory = player.GetHistory();
		_mapPosition = player.GetCurrentNode();
	}

	public void RestorePlayer(GameObject player)
	{
		if (player.TryGetComponent(out MapPlayer map))
		{
			map.SetSeed(_seed);
			map.RestoreHistory(_mapHistory);
			map.RestorePosition(_mapPosition);
		}
	}
}
