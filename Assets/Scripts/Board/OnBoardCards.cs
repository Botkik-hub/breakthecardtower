using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container for cards on board
/// </summary>
public class OnBoardCards : MonoBehaviour
{
    public static OnBoardCards Instance;

    private List<GameObject> cardsOnBoard;

    private Dictionary<GameObject, Permanent> playerPermanentsOnBoard = new Dictionary<GameObject, Permanent>();
    private Dictionary<GameObject, Permanent> enemyPermanentsOnBoard = new Dictionary<GameObject, Permanent>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Dictionary<GameObject, Permanent> GetPlayerPermanents() { return playerPermanentsOnBoard; }
    public Dictionary<GameObject, Permanent> GetEnemyPermanents() { return enemyPermanentsOnBoard; }
    public bool AddPermanent(CombatData data, GameObject hex, Permanent permanent)
    {
        if (data is PlayerCombatData && !playerPermanentsOnBoard.ContainsKey(hex))
        {
            playerPermanentsOnBoard.Add(hex, permanent);
            permanent.gameObject.transform.SetParent(hex.transform);
            return true;
        }
        if (data is EnemyCombatData && !enemyPermanentsOnBoard.ContainsKey(hex))
        {
            enemyPermanentsOnBoard.Add(hex, permanent);
            permanent.gameObject.transform.SetParent(hex.transform);
            return true;
        }
        return false;
    }

    public bool RemovePermanent(GameObject hex)
    {
        if (enemyPermanentsOnBoard.ContainsKey(hex))
        {
            CombatManager.instance.GetEnemyData().discard.AddCard(enemyPermanentsOnBoard.GetValueOrDefault(hex));
            enemyPermanentsOnBoard.Remove(hex);
            return true;
        }
        if (playerPermanentsOnBoard.ContainsKey(hex))
        {
            CombatManager.instance.GetPlayerData().discard.AddCard(playerPermanentsOnBoard.GetValueOrDefault(hex));
            playerPermanentsOnBoard.Remove(hex);
            return true;
        }

        return false;
    }

}

//public struct permanentCardStruct
//{
//    public Hex hex;
//    public PermanentTemplate permanentCard;
//}