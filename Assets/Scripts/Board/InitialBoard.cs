using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBoard : MonoBehaviour
{
    public Vector3Int[] hexV;
    public TerraHexTemplate[] card;

    void Start()
    {
        StartCoroutine(UnlockHexagon());
    }


    void Update()
    {
        
    }

    private IEnumerator UnlockHexagon()
    {
        yield return new WaitForSeconds(1);
        
        GameObject tempObject;
        int randomCardNum;
        
        for(int i = 0; i < hexV.Length; i++)
        {
            BoardManager.FieldTiles.TryGetValue(new Hex(hexV[i].x, hexV[i].y, hexV[i].z), out tempObject);
            randomCardNum = Random.Range(0, card.Length);

            tempObject.GetComponent<HexActions>().isUnlocked = true;
            tempObject.GetComponent<HexActions>().HexCard = card[randomCardNum];
            tempObject.GetComponent<HexActions>().playUnlockTerraHex();
            StartCoroutine(tempObject.GetComponent<HexActions>().ChangeTerraColor());


        }

    }
}
