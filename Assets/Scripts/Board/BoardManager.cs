using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // we are referring to hexagons not as hexagons, but rather the 2D silhouettes of cubes
    // Therefore they are using a cube coordinate system
    public GameObject hexTilePrefab;
    public GameObject Field;
    //public GameObject Caverns;

    // Dictionaries of Hex gameObjects
    // the Key is the Hex struct representation of the Hex at a given (q,r,s)
    // the Value is the gameObject reference to the Hex represented at those cubic coordinates in world-space
    public static Dictionary<Hex, GameObject> FieldTiles = new Dictionary<Hex, GameObject>();
    //public static Dictionary<Hex, GameObject> CavernsTiles = new Dictionary<Hex, GameObject>();
    //public float cavernsOffset = 1.0f;
    
    void Start()
    {
        // initialize the boards!
        InitializeBoards(5, 5, 5);
    }

    void Update()
    {
        
    }

    // gets a Hex at cube coordinate (q,r,s) on a specified layer
    // will return null if there is no hex in-scene with those coordinates
    public GameObject GetHex(int q, int r, int s, string layer) {
        GameObject result;

        switch (layer) {
            case "Field":
                if (!FieldTiles.TryGetValue(new Hex(q,r,s), out result)) {
                    return null;
                }
                return result;
            //case "Caverns":
            //    if (!CavernsTiles.TryGetValue(new Hex(q,r,s), out result)) {
            //        return null;
            //    }
            //    return result;
            default:
                return null;
        }
    }

    // intializes a board given bounds for q, r, and s
    // if you want a "circular" board, then qMax = rMax = sMax
    void InitializeBoards(int qMax, int rMax, int sMax) {
        // create the boards!
        FieldTiles.Clear();
        for(int q = -qMax+1; q < qMax; q++) {
            for(int r = -rMax+1; r < rMax; r++) {
                for(int s = -sMax+1; s < sMax; s++) {
                    if(q + r + s == 0) {

                        // upper layer (Field)
                        // making a new Hex struct here takes care of the cube-to-world coordinate conversion
                        Hex h = new Hex(q, r, s);
                        GameObject tempHex = Instantiate(hexTilePrefab, h.GetWorldCoords(), Quaternion.identity);
                        // currently have to rotate 90 degrees because my test hexagon model is oriented wrong lol
                        // will not be necessary later when we dont have my useless ass modelling
                        tempHex.transform.Rotate(Vector3.right, 90);
                        SetTileInfo(tempHex, q, r, s, "Field");
                        //UpperTiles.Add(tempTile);
                        FieldTiles.Add(h, tempHex);


                        //// lower layer (Caverns)
                        //h = new Hex(q, r, s);
                        //tempHex = Instantiate(hexTilePrefab, h.GetWorldCoords() - new Vector3(0, cavernsOffset, 0), Quaternion.identity);
                        //tempHex.transform.Rotate(Vector3.right, 90);
                        //SetTileInfo(tempHex, q, r, s, "Caverns");
                        ////LowerTiles.Add(tempTile);
                        //CavernsTiles.Add(h, tempHex);
                    }
                }
            }
        }
    }

    // mostly overhead, neater hierarchy sorting, naming, etc
    void SetTileInfo(GameObject tempObj, int q, int r, int s, string layer) {
        // assign tile to specific layer for nicer easier-to-read overhead
        switch(layer) {
            case "Field":
                tempObj.transform.parent = Field.transform;
                tempObj.GetComponent<HexActions>().layer = layer;
                break;
            //case "Caverns":
            //    tempObj.transform.parent = Caverns.transform;
            //    tempObj.GetComponent<HexActions>().layer = layer;
            //    break;
        }

        tempObj.name = "Hex(" + q.ToString() + "," + r.ToString() + "," + s.ToString() + ")";
        tempObj.GetComponent<HexActions>().q = q;
        tempObj.GetComponent<HexActions>().r = r;
        tempObj.GetComponent<HexActions>().s = s;

        if (r == 0)
            tempObj.GetComponent<HexActions>().side = ESide.Common;
        if (r < 0)
            tempObj.GetComponent<HexActions>().side = ESide.Player;
        if (r > 0)
            tempObj.GetComponent<HexActions>().side = ESide.Opponent;
    }
}
