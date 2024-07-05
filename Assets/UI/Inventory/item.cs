using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Inventory.Model;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public ItemData item;
    public Image image;
    public Transform parentAfterDrag;
    public Transform CanvasTransform;

    [Header("Item Amount")]
    public int amount = 1;
    public TextMeshProUGUI countText;
    private void Start()
    {
        if(CanvasTransform == null)
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            CanvasTransform = canvas.transform;
        }
        initiliaseItem(item);
    }
    public void initiliaseItem(ItemData newItem)
    {
        item = newItem;
        image.sprite = newItem.ItemImage;
        refreshCount();

    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
        // Update UI or other necessary components
    }

    public void refreshCount()
    {
        countText.text = amount.ToString();
        bool textActive = amount > 1;
        countText.gameObject.SetActive(textActive);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        countText.raycastTarget = false;
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(CanvasTransform);
        transform.SetAsLastSibling();
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        countText.raycastTarget = true;
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
            
    }

    
}
