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

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDroppedOnValidTarget = false;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;

        originalPos = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        if (!IsDroppedOnValidTarget)
        {
            HandleInvalidDrop();
        }
    }

    public void HandleInvalidDrop()
    {
        rectTransform.position = originalPos;
    }

    public void SetDroppedOnValidTarget(bool value)
    {
        IsDroppedOnValidTarget = value;
    }
}
