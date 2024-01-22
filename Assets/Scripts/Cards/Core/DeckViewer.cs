using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to show all cards in the deck
/// </summary>
public class DeckViewer : MonoBehaviour
{
	private bool bViewingDeck = false;

	[SerializeField] private GameObject CardViewer;
	[SerializeField] private GameObject ViewerContent;
    [SerializeField] private PlayerCombatData playerCombatData;
	[SerializeField] private EnemyCombatData enemyCombatData;
	private List<Card> sortedList = new List<Card>();

	public GameObject GetViewer() { return CardViewer; }
	public GameObject GetViewerContent() { return ViewerContent; }

	private void ReloadCardList(Deck deck)
	{
		sortedList.Clear();
		foreach (Card card in deck.GetCards())
        {
            sortedList.Add(card);
		}
		sortedList.Sort((x,y)=> x.GetComponent<CardDisplay>().cardInfo.cardName.CompareTo(y.GetComponent<CardDisplay>().cardInfo.cardName));
	}

	public void ViewDeck()
	{
		if (bViewingDeck) { CloseViewer(); }

		CardViewer.SetActive(true);
		bViewingDeck = true;
		ReloadCardList(playerCombatData.deck);
		foreach (Card card in sortedList)
		{
			Card image = Instantiate(card);
			image.transform.SetParent(ViewerContent.transform);
			image.transform.localScale = Vector3.one;
		}
	}
	public void ViewDiscard(bool playerDiscard)
    {
        if (bViewingDeck) { CloseViewer(); }

        CardViewer.SetActive(true);
        bViewingDeck = true;

		if (playerDiscard) ReloadCardList(playerCombatData.discard);
		else ReloadCardList(enemyCombatData.discard);

        foreach (Card card in sortedList)
        {
            Card image = Instantiate(card);
            image.transform.SetParent(ViewerContent.transform);
            image.transform.localScale = Vector3.one;
        }
    }

	public void CloseViewer()
	{
		Transform tempParent = new GameObject().transform;
		for(int i = ViewerContent.transform.childCount; i > 0;--i)
		{
			ViewerContent.transform.GetChild(0).SetParent(tempParent);
		}
		Destroy(tempParent.gameObject);
		CardViewer.SetActive(false);
		bViewingDeck = false;
    }
}