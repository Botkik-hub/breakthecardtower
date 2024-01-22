using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyCombatData _enemyCombatData;

    public bool TryPlayTerraHex()
    {
        Card terraHex = _enemyCombatData.hand.ContainsCardOfType(ECardType.TerraHex);
        if (terraHex == null) return false;

        HexActions emptyHex = _enemyCombatData.GetRandomAdjacentHex();
        if (emptyHex == null) return false;

        CardPlayer.Instance.PlayCard(_enemyCombatData, terraHex, emptyHex);
        _enemyCombatData.StartCoroutine(_enemyCombatData.ReloadHexes());

        return true;
    }

    public bool TryPlayPermanent()
    {
        Card permanent = _enemyCombatData.hand.ContainsCardOfType(ECardType.Permanent);
        if (permanent == null) return false;

        HexActions emptyHex = _enemyCombatData.GetRandomEmptyHex();
        if (emptyHex == null) return false;

        permanent.gameObject.GetComponent<CardDisplay>().isHidden = false;
        permanent.gameObject.GetComponent<CardDisplay>().UpdateDisplay();
        CardPlayer.Instance.PlayCard(_enemyCombatData, permanent, emptyHex);
        _enemyCombatData.StartCoroutine(_enemyCombatData.ReloadHexes());

        return true;
    }

}
