using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Constants
    const int MAXSTACKSIZE = 10;

    // Events
    public delegate void OnInventoryChangedDelegate(List<SlotData> inventory);
    public event OnInventoryChangedDelegate OnInventoryChanged;

    // Variables
    public List<SlotData> inventory = new List<SlotData>();
    public Player player;

    public int maxInventorySize;

    // Ensure everything is assigned
    public void Init(Player player)
    {
        inventory = player.inventory;
        this.player = player;
        maxInventorySize = player.maxInventorySize;
    }

    // Insert item logic
   public void insert(ItemData droppedItem, int amount)
    {
        int overflow = amount;

        while (overflow > 0)
        {
            bool inserted = false;

            // Check existing inventory for the same item and add if there's available space
            for (int index = 0; index < inventory.Count; index++)
            {
                if (inventory[index] != null && inventory[index].item == droppedItem)
                {
                    if (inventory[index].amount < MAXSTACKSIZE)
                    {
                        int availableSpace = MAXSTACKSIZE - inventory[index].amount;
                        if (overflow <= availableSpace)
                        {
                            inventory[index].amount += overflow;
                            overflow = 0;
                            inserted = true;
                            break;
                        }
                        else
                        {
                            inventory[index].amount += availableSpace;
                            overflow -= availableSpace;
                            inserted = true;
                        }
                    }
                }
            }

            // If overflow still exists and there are empty slots, create new SlotData
            if (overflow > 0)
            {
                for (int index = 0; index < inventory.Count; index++)
                {
                    if (inventory[index] == null)
                    {
                        SlotData slotToBeAdded = ScriptableObject.CreateInstance<SlotData>();
                        slotToBeAdded.item = droppedItem;
                        if (overflow <= MAXSTACKSIZE)
                        {
                            slotToBeAdded.amount = overflow;
                            overflow = 0;
                        }
                        else
                        {
                            slotToBeAdded.amount = MAXSTACKSIZE;
                            overflow -= MAXSTACKSIZE;
                        }
                        inventory[index] = slotToBeAdded;
                        inserted = true;
                        break;
                    }
                }
            }

            // If there was no empty slot to insert the overflow, stop the loop to prevent an infinite loop
            if (!inserted)
            {
                break;
            }
        }

        // Communicate to other objects
        OnInventoryChanged?.Invoke(inventory);
    }

    /*public void Remove(GameMaterial selectedItem, int amount)
    {
        // Implement remove logic here if needed
    }

    public int GetSameItemCount(MaterialData item)
    {
        int count = 0;
        foreach (var slot in inventory)
        {
            if (slot != null && slot.item == item)
            {
                count += 1;
            }
        }
        return count;
    }

    private int GetItemIndex(MaterialData item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null && inventory[i].item == item)
            {
                return i;
            }
        }
        return -1;
    }*/
}