using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDebugger : MonoBehaviour
{
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
}
