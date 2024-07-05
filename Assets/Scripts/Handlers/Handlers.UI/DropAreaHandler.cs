using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropAreaHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] private RectTransform correctRectTransform;
    [SerializeField] private bool canCarryMultiple;

    //Settle object into proper space when dropped on container
    public void OnDrop(PointerEventData eventData)
    {
        //Logic for when dropping on a single holding contatiners..
        if (!canCarryMultiple)
        {
            //Set the anchored pos and its parent transform..
            if (eventData.pointerDrag != null && this.transform.childCount < 1 && eventData.pointerDrag.GetComponent<DraggableItemHandler>() != null)
            {
                RectTransform draggedTransform = eventData.pointerDrag.GetComponent<RectTransform>();

                draggedTransform.anchoredPosition = correctRectTransform.anchoredPosition;

                draggedTransform.SetParent(transform);

                draggedTransform.localPosition = Vector3.zero;


                DraggableItemHandler draggableItemHandler = eventData.pointerDrag.GetComponent<DraggableItemHandler>();

                //Toggle IsDroppedOnValidTarget of the DraggableItemHandler..
                if (draggableItemHandler != null)
                {
                    draggableItemHandler.SetDroppedOnValidTarget(true);
                }
            }
        }
        //Logic for multi holding contrainers..
        else
        {
            //Set the anchored pos and its parent transform..
            if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DraggableItemHandler>() != null)
            {
                RectTransform draggedTransform = eventData.pointerDrag.GetComponent<RectTransform>();

                draggedTransform.anchoredPosition = correctRectTransform.anchoredPosition;

                draggedTransform.SetParent(transform);

                draggedTransform.localPosition = Vector3.zero;


                DraggableItemHandler draggableItemHandler = eventData.pointerDrag.GetComponent<DraggableItemHandler>();

                //Toggle IsDroppedOnValidTarget of the DraggableItemHandler..
                if (draggableItemHandler != null)
                {
                    draggableItemHandler.SetDroppedOnValidTarget(true);
                }
            }
        }
    } 
}
