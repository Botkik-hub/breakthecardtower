
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles drag logic of the card
/// TODO: separate display functionality (probably move to the CardDisplay)
/// </summary>
public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Transform _parentAfterDrag;
    private Card _card;
    private int siblingIndex;
    public bool isDraggable = true;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _card = GetComponent<Card>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable) { return; }
        _parentAfterDrag = transform.parent;
        siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.4f;
        //CardPlayer.Instance.StartCardDrag(_card);
        GameMonitor.Instance.cardToPlay = gameObject;
        GameMonitor.Instance.cardOnDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable) { return; }
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable) { return; }
        transform.SetParent(_parentAfterDrag);
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1.0f;
        transform.SetSiblingIndex(siblingIndex);
        GameMonitor.Instance.cardOnDragging = false;
        StartCoroutine(ResetCardToPlay());
    }

    IEnumerator ResetCardToPlay()
    {
        yield return new WaitForSeconds(0.1f);
        GameMonitor.Instance.cardToPlay = null;
    }
    
}