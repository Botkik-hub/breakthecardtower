using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public static CardPlayer Instance;
    
    private GameObject _cardToPlay;

    private Transform _currentHexTransform;
    private HexActions _currentHex;
    [SerializeField] private GameObject artifactPrefab;
    [SerializeField] private GameObject nexusBaublePrefab;

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

    public bool PlayTerraHex(CombatData data, TerraHex card, HexActions hexData)
    {
        if (hexData.isUnlocked) return false; // check if the hexagon spot is unlocked. if unlocked then we can't use the card in this spot
        if (!hexData.CheckNeighbour()) return false;  // check if there is one unlock neighbour 
        if (data is PlayerCombatData && hexData.side == ESide.Opponent) { return false; }
        else if (data is EnemyCombatData && hexData.side == ESide.Player) { return false; }

        hexData.isUnlocked = true;
        TerraHexTemplate template = (TerraHexTemplate)card.gameObject.GetComponent<CardDisplay>().cardInfo;
        hexData.SetHexCard(template);
        hexData.hexType = template.HexElement;


        hexData.playUnlockTerraHex();
        hexData.StartCoroutine(hexData.ChangeTerraColor());

        data.hand.RemoveCard(card);
        Destroy(card.gameObject);
        if (data is PlayerCombatData)
        {
            GameMonitor.Instance.cardOnDragging = false;
        }

        return true;
    }

    public bool PlayPermanent(CombatData data, Permanent card, HexActions hexData)
    {
        if (!hexData.isUnlocked) { return false; }
        if (hexData.isOccupied) { return false; }
        if (data is PlayerCombatData && hexData.side == ESide.Opponent) { return false; }
        else if (data is EnemyCombatData && hexData.side == ESide.Player) { return false; }

        hexData.isOccupied = true;
        hexData.permanentCard = card;
        if (((PermanentTemplate)card.GetComponent<CardDisplay>().cardInfo).Type == EPermanentType.Artifact)
        {
            hexData.modelObject = Instantiate(artifactPrefab, hexData.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            //BoardAction.artifactAnimation.Add(hexData.modelObject);
        }
        else if (((PermanentTemplate)card.GetComponent<CardDisplay>().cardInfo).Type == EPermanentType.NexusBobble)
        {
            hexData.modelObject = Instantiate(nexusBaublePrefab, hexData.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            //BoardAction.nexusBubbleAnimation.Add(hexData.modelObject);
        }
        foreach (List<CardEffect> list in card.GetEffects())
        {
            foreach (CardEffect effect in list)
            {
                if (effect is ActivateAdjacentEffect)
                {
                    ActivateAdjacentEffect e = (ActivateAdjacentEffect)effect;
                    List<Hex> neighborHexes = (new Hex(hexData.q, hexData.r, hexData.s)).AllNeighbors();
                    List<HexActions> hexes = new List<HexActions>();
                    for (int i = 0; i < neighborHexes.Count; i++)
                    {
                        hexes.Add(BoardManager.FieldTiles.GetValueOrDefault(neighborHexes[i]).GetComponent<HexActions>());
                    }
                    e.SetTargetHexes(hexes);
                }
            }
        }
        OnBoardCards.Instance.AddPermanent(data, hexData.gameObject, card);

        data.hand.RemoveCard(card);
        if (data is PlayerCombatData)
        {
            GameMonitor.Instance.cardOnDragging = false;
        }

        return true;
    }

    public bool PlayWhimsy(CombatData data, NonPermanent card, HexActions hexData)
    {
        NonPermanentTemplate info = (NonPermanentTemplate)card.gameObject.GetComponent<CardDisplay>().cardInfo;
        if (info.Cost > data.Imagination) return false;
        if (info.TargetsHex)
        {
            if (hexData == null) { return false; }
            if(hexData.isOccupied == false) { return false; }
            card.SetEffectHexTargets(hexData);
            card.Play();
        }
        else
        {
            card.Play();
        }

        data.Imagination -= info.Cost;
        CombatManager.instance.UpdateImaginationText();
        data.DiscardCard(card);
        return true;
    }

    public bool PlayCard(CombatData data, Card card, HexActions hexData)
    {
        if(card is TerraHex)
        {
            if (hexData == null) { return false; }
            TerraHex cardToPlay = (TerraHex)card;
            PlayTerraHex(data, cardToPlay, hexData);
            return true;
        }

        if (card is Permanent)
        {
            if(hexData == null) { return false; }
            Permanent cardToPlay = (Permanent)card;
            PlayPermanent(data, cardToPlay, hexData);
            return true;
        }

        if (card is NonPermanent)
        {
            NonPermanent cardToPlay = (NonPermanent)card;
            PlayWhimsy(data, cardToPlay, hexData);
            return true;
        }

        return false;
    }

    private void Update()
    {
        UpdateCast();

        if (Input.GetMouseButtonUp(0))
        {
            if (!CombatManager.instance.canPlay) return;
            if (Input.mousePosition.y < 300) return;
            _cardToPlay = GameMonitor.Instance.cardToPlay;
            if (_cardToPlay == null) return;


            // Placing TerraHex
            if (_cardToPlay.GetComponent<CardDisplay>().cardInfo.cardType == ECardType.TerraHex)
            {
                TerraHex card = _cardToPlay.GetComponent<TerraHex>();
                Instance.PlayTerraHex(FindObjectOfType<PlayerCombatData>(), card, _currentHex);
            }
            // Placing Permanent
            else if (_cardToPlay.GetComponent<CardDisplay>().cardInfo.cardType == ECardType.Permanent)
            {
                Permanent card = _cardToPlay.GetComponent<Permanent>();
                Instance.PlayPermanent(FindObjectOfType<PlayerCombatData>(), card, _currentHex);
            }
            // Placing nonPermanent
            else if (_cardToPlay.GetComponent<CardDisplay>().cardInfo.cardType == ECardType.Whimsy)
            {
                NonPermanent card = _cardToPlay.GetComponent<NonPermanent>();
                Instance.PlayWhimsy(FindObjectOfType<PlayerCombatData>(), card, _currentHex);
            }
            _cardToPlay = null;
            GameMonitor.Instance.cardToPlay = null;
        }
    }

    private void UpdateCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            RayHitResponse(hit);
        }
        else
        {
            DisableActiveHex();
        }

    }
    
    private void RayHitResponse(RaycastHit hit)
    {
        if (hit.transform == _currentHexTransform) return;

        var hitHex = hit.transform.GetComponent<HexActions>();
        
        if (!hitHex) return;
        
        SwitchActiveHex(hitHex);
    }

    private void SwitchActiveHex(HexActions newHex)
    {
        DisableActiveHex();
        _currentHex = newHex;
        _currentHex.SetMouseOver(true);
    }

    private void DisableActiveHex()
    {
        if (_currentHex == null) return;
        
        _currentHex.SetMouseOver(false);
        _currentHex = null;
    }
    
}
