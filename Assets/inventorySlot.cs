using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Sprite selected, notSelect;

    private void Awake()
    {
        deselectSlot();
    }
    public void selectSlot()
    {
        image.sprite = selected;
    }

    public void deselectSlot()
    {
        image.sprite = notSelect;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            Item inventoryItem = eventData.pointerDrag.GetComponent<Item>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
