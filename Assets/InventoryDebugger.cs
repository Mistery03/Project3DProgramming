using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryDebugger : MonoBehaviour
{
    public Player player;
    public InventoryController inventoryManager;
    public ItemData[] itemsToPickup;
    
    
    public void pickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);

        if (result == true) 
        {
            Debug.Log("Item succesfully addeed");
        }else if(result == false)
        {
            Debug.Log("Inventory Max Capacity Reached");
        }
    }

    public void getSelectedItem()
    {
        ItemData receivedItem = inventoryManager.getSelectedItem(false);
        if(receivedItem != null) 
        {
            Debug.Log($"Currently holding: {receivedItem}");
           
        }else
        {
            Debug.Log($"Currently holding: nothing");
        }
    }

    public void useSelectedItem()
    {
        ItemData receivedItem = inventoryManager.getSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log($"Using: {receivedItem}");

        }
        else
        {
            Debug.Log($"Using: Delulu");
        }
    }

    public void getItemIDList()
    {
        int[] itemIDList= inventoryManager.getAllItemID();

        for(int i=0;i<itemIDList.Length;i++)
        {
            if (itemIDList[i] > -1)
                Debug.Log($"Slot {i+1}: itemID {itemIDList[i]}");
            else
                Debug.Log($"Slot {i + 1}: empty");
        }
    }

    public void saveInventory()
    {
      SaveSystem.saveInventory(inventoryManager);
    }

    public void loadInventory()
    {
        InventoryData data = SaveSystem.loadInventory();

        inventoryManager.itemIDList = data.itemIDList;
        inventoryManager.itemAmountList = data.itemAmountList;
        inventoryManager.itemExistList = data.itemSlotExist;

        inventoryManager.addItemViaItemID();

        for (int i = 0; i < data.itemIDList.Length; i++)
        {
            Debug.Log($"Slot no. {i+1}");
            Debug.Log($"ItemID: {data.itemIDList[i]}");
            Debug.Log($"Item Amount: {data.itemAmountList[i]}");
            Debug.Log($"Exist?: {data.itemSlotExist[i]}");
        }

            
    }
}
