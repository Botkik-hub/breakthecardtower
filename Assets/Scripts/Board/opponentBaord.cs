using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opponentBaord : MonoBehaviour
{
    private List<HexActions> HexOpponent = new List<HexActions>();
    public GameObject cardPrefab;

    public BoardManager boardManager;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(initializeOppopentSide());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator initializeOppopentSide()
    {
        yield return new WaitForSeconds(2);
        foreach (var other in BoardManager.FieldTiles.Values)
        {
            if (other.gameObject.GetComponent<HexActions>().r > 0)
            {
                HexOpponent.Add(other.GetComponent<HexActions>());
            }
        }
        PlaceRandomCards();
    }

    void PlaceRandomCards()
    {
        
        GameObject tem = boardManager.GetHex(0, 4, -4, "Field");
        //tem.GetComponent<HexActions>().AddCardOpponent();
        
        tem = boardManager.GetHex(-2, 4, -2, "Field");
        //tem.GetComponent<HexActions>().AddCardOpponent();
    }
}
