using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexActions : MonoBehaviour
{

    // FUTURE NOTE: Definitely get rid of this and use a MouseManager that will make RayCasts instead
    // video for ref: https://www.youtube.com/watch?v=p2_X_klweBw&list=PLbghT7MmckI4HEp8z_ngZvCV3sHWaAYN3&index=3
    // (this was for quick testing purposes only)
    // problem will be detecting when the mouse *stops* hovering over a hex
    // (i.e. OnMouseExit)
    
    private Material highlightMaterial;
    [SerializeField] private Material neutralMaterial;
    private Renderer objectRenderer;

    [HideInInspector] public TerraHexTemplate HexCard;
    [HideInInspector] public Permanent permanentCard;
    public MeshRenderer cardRenderer;
    private Material cardMaterial;

    // REMOVE PLAYING CARD FROM HEX ACTION
    [HideInInspector] public int q;
    [HideInInspector] public int r;
    [HideInInspector] public int s;
    [HideInInspector] public string layer;
    [HideInInspector] public ESide side;
    [HideInInspector] public bool isUnlocked = false;
    [HideInInspector] public bool isOccupied = false;
    [HideInInspector] public EHexType hexType;
    public GameObject unlockTerraAnimationPrefab;
    public GameObject modelObject;

    void Start()
    {
        cardRenderer.enabled = false;
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = neutralMaterial;
        cardMaterial = neutralMaterial;
    }

    public void SetHexCard(TerraHexTemplate terraHex)
    {
        HexCard = terraHex;
    }
    public Material GetMaterial()
    {
        return cardMaterial;
    }

    public void playUnlockTerraHex()
    {
        GameObject animation = Instantiate(unlockTerraAnimationPrefab);
        animation.transform.position = transform.position;
        Destroy(animation, 2.0f);
    }

    public IEnumerator ChangeTerraColor()
    {
        yield return new WaitForSeconds(2);
        cardMaterial = HexCard.material;
        GetComponent<Renderer>().material = cardMaterial;
        cardRenderer.material = HexCard.texture;
        cardRenderer.enabled = true;

        if (side == ESide.Opponent)
        {
            cardRenderer.transform.localEulerAngles = Vector3.forward * 180;
        }
    }

    void OnMouseExit()
    {
        if (!isUnlocked)
        {
            cardRenderer.material = cardMaterial;
        }
        if(permanentCard != null)
        {
            GameMonitor.Instance.hoverDisplay.GetComponent<DisplayHand>().RemoveCard(permanentCard);
            permanentCard.AlignTransform(transform);
        }
    }

    private void OnMouseEnter()
    {
        if (!isUnlocked) return;
        if (permanentCard == null) return;
        permanentCard.AlignTransform(transform);
        GameMonitor.Instance.hoverDisplay.GetComponent<DisplayHand>().AddCard(permanentCard);
        permanentCard.transform.localScale = 1.5f * Vector3.one;
    }

    public void SetMouseOver(bool isOver)
    {
        GetComponent<Renderer>().material = isOver ? highlightMaterial : cardMaterial;
    }

    public bool CheckNeighbour()
    {
        GameObject x;

        Hex tempHex = new Hex(q, r, s);
        List<Hex> neighbourHexs = tempHex.AllNeighbors();

        if (layer == "Field")
        {
            for (int i = 0; i < neighbourHexs.Count; i++)
            {
                BoardManager.FieldTiles.TryGetValue(neighbourHexs[i], out x);
                if (x.GetComponent<HexActions>().isUnlocked)
                    return true;
            }
        }

        /*if (layer == "Caverns")
        {
            for (int i = 0; i < neighbourHexs.Count; i++)
            {
                BoardManager.CavernsTiles.TryGetValue(neighbourHexs[i], out x);
                if (x.GetComponent<HexActions>().isUnlocked)
                    return true;
            }
        }*/
        return false;
    }

    public void RemovePermanent()
    {
        if(permanentCard == null) return;

        GameMonitor.Instance.hoverDisplay.GetComponent<DisplayHand>().RemoveCard(permanentCard);
        permanentCard.AlignTransform(transform);

        permanentCard = null;
        modelObject.GetComponent<AnimationScript_Nexus>().anim.SetBool("Destroy", true);
        Destroy(modelObject, 2.0f);
        isOccupied= false;
    }
}
