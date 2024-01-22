using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.Play("Hover On");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("Hover Off");
    }

    public void SelectionAnim()
    {
        //animator.Play("Select");
        //GetComponent<HoverAnimation>().enabled = false; //This is to stop the function (OnPointerEnter)
    }
}
