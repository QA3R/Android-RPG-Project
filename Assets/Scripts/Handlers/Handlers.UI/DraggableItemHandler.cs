using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItemHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private Vector3 originalPos;

    private bool IsDroppedOnValidTarget;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    //Set the object's opacity to semi visible and disable raycasting blocking..
    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDroppedOnValidTarget = false;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;

        originalPos = this.transform.position;
    }

    //Snap the object to the mouse position..
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //Enable raycast blocking, reset opacity settings, and handle any invalid drop situations..
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        if (!IsDroppedOnValidTarget)
        {
            HandleInvalidDrop();
        }
    }

    //Reset the object to its original pos..
    public void HandleInvalidDrop()
    {
        rectTransform.position = originalPos;
    }

    //set the IsDroppedOnValidTarget value when object lands on proper space..
    public void SetDroppedOnValidTarget(bool value)
    {
        IsDroppedOnValidTarget = value;
    }
}
