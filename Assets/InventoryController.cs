using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    public int maxStackItem = 10;
    public InventorySlot[] slotList;
    public GameObject ItemInventoryPrefab;

    int selectedSlot = -1;

    private void Start()
    {
        changeSelectedSlot(0);
    }

    private void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number); 
            if(isNumber && number > 0 && number < 10) 
            { 
                changeSelectedSlot(number - 1);
            }
        }
    }
    public void changeSelectedSlot(int slotIndex)
    {   
        if(selectedSlot >= 0)
            slotList[selectedSlot].deselectSlot();

        slotList[slotIndex].selectSlot();
        selectedSlot = slotIndex;
    }
    public bool AddItem(ItemData item)
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            InventorySlot slot = slotList[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.amount < maxStackItem &&
                itemInSlot.item.stackable == true)
            {
                itemInSlot.amount++;
                itemInSlot.refreshCount();
                return true;
            }
        }

        for (int i = 0; i < slotList.Length; i++) 
        { 
            InventorySlot slot = slotList[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if(itemInSlot == null)
            {
                SpawnItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnItem(ItemData item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(ItemInventoryPrefab, slot.transform);
        Item inventoryItem = newItem.GetComponent<Item>();
        inventoryItem.initiliaseItem(item);
    }

    public ItemData getSelectedItem(bool useItem)
    {
        InventorySlot slot = slotList[selectedSlot];
        Item itemInSlot = slot.GetComponentInChildren<Item>();
        if (itemInSlot != null)
        {
            ItemData item = itemInSlot.item;
            if (useItem == true)
            {
                itemInSlot.amount--;
                if(itemInSlot.amount <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }else
                {
                    itemInSlot.refreshCount();
                }
            }
            return item;
        }
        return null;
    }

   
}
