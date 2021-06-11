using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Transform startSlot;
    private ItemData inventoryItem
    {
        get{
            return GetComponentInChildren<ItemVisual>().Item;
        }
    }

    public void Init(ItemData item)
    {
        GetComponentInChildren<ItemVisual>().Init(item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<InventoryController>().ShowItemInfo(inventoryItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<InventoryController>().ShowItemInfo(null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(transform.parent.name);
        GetComponentInParent<InventoryController>().DevClick(inventoryItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
        startSlot = transform;
        GetComponentInParent<InventoryController>().DragStart(inventoryItem);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(transform.name);
        GetComponentInParent<InventoryController>().DragStop(inventoryItem, eventData, startSlot);
    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
