using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle Details");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.Play("Display Details");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("Hide Details");
    }
}
