using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropAreaHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] private RectTransform correctRectTransform;
    [SerializeField] private bool canCarryMultiple;
    public void OnDrop(PointerEventData eventData)
    {
        if (!canCarryMultiple)
        {
            if (eventData.pointerDrag != null && this.transform.childCount < 1 && eventData.pointerDrag.GetComponent<DraggableItemHandler>() != null)
            {
                RectTransform draggedTransform = eventData.pointerDrag.GetComponent<RectTransform>();

                draggedTransform.anchoredPosition = correctRectTransform.anchoredPosition;

                draggedTransform.SetParent(transform);

                draggedTransform.localPosition = Vector3.zero;


                DraggableItemHandler draggableItemHandler = eventData.pointerDrag.GetComponent<DraggableItemHandler>();

                if (draggableItemHandler != null)
                {
                    draggableItemHandler.SetDroppedOnValidTarget(true);
                }
            }
        }
        else
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableItemHandler>() != null)
            {
                RectTransform draggedTransform = eventData.pointerDrag.GetComponent<RectTransform>();

                draggedTransform.anchoredPosition = correctRectTransform.anchoredPosition;

                draggedTransform.SetParent(transform);

                draggedTransform.localPosition = Vector3.zero;


                DraggableItemHandler draggableItemHandler = eventData.pointerDrag.GetComponent<DraggableItemHandler>();

                if (draggableItemHandler != null)
                {
                    draggableItemHandler.SetDroppedOnValidTarget(true);
                }
            }
        }
    } 
}
