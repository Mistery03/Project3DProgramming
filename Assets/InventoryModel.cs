using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class InventoryModel : MonoBehaviour
{

    [SerializeField] InventorySlot slot;
    [SerializeField] int maxInventorySize;

    public List<InventorySlot> slotList = new List<InventorySlot>();
    public int maxInventorySlots = 16;
    public List<SlotData> playerInventory = new List<SlotData>();

    public HotBarModel hotBarModel;

    public Transform playerPos;

    const int MAXSTACKSIZE = 10;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(playerInventory.Count);
        for (int i = 0; i < maxInventorySize; i++) 
        {
           InventorySlot slotToBeAdded = Instantiate(slot);

            slotToBeAdded.inventoryModel = this;
            slotToBeAdded.hotBarModel = hotBarModel;

           slotToBeAdded.transform.SetParent(this.transform,false);
           slotList.Add(slotToBeAdded);
        }

        UpdateSlots();

    }

    public void UpdateSlots()
    {
        for (int i = 0; i < maxInventorySize; i++)
        {
            if (playerInventory[i] != null)
            {
                slotList[i].item = playerInventory[i].item;
                slotList[i].amount = playerInventory[i].amount; // This should be adjusted as needed
            }
            else
            {
                slotList[i].item = null;
            }

        
        }
    }

   

    public void RemoveItem(ItemData item, int amount)
    {
        foreach (var slot in slotList)
        {
            if (slot.item == item)
            {
                if (slot.amount >= amount)
                {
                    slot.amount -= amount;
                    if (slot.amount == 0)
                    {
                        slot.ClearSlot();
                    }
                    slot.UpdateSlot();
                    return;
                }
                else
                {
                    amount -= slot.amount;
                    slot.ClearSlot();
                    slot.UpdateSlot();
                }
            }
        }
    }

    // Insert item logic
    public void Insert(ItemData droppedItem, int amount)
    {
        int overflow = amount;

        while (overflow > 0)
        {
            bool inserted = false;

            // Check existing inventory for the same item and add if there's available space
            for (int index = 0; index < playerInventory.Count; index++)
            {
                if (playerInventory[index] != null && playerInventory[index].item == droppedItem)
                {
                    if (playerInventory[index].amount < MAXSTACKSIZE)
                    {
                        int availableSpace = MAXSTACKSIZE - playerInventory[index].amount;
                        if (overflow <= availableSpace)
                        {
                            playerInventory[index].amount += overflow;
                            overflow = 0;
                            inserted = true;
                            break;
                        }
                        else
                        {
                            playerInventory[index].amount += availableSpace;
                            overflow -= availableSpace;
                            inserted = true;
                        }
                    }
                }
            }

            // If overflow still exists and there are empty slots, create new SlotData
            if (overflow > 0)
            {
                for (int index = 0; index < playerInventory.Count; index++)
                {
                    if (playerInventory[index] == null)
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
                        playerInventory[index] = slotToBeAdded;
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
        UpdateSlots();
     
    }

    public void Remove(ItemData selectedItem, int amount,bool isRemovingItemToWorld = true)
    {
        int remainingToRemove = amount;

        while (remainingToRemove > 0)
        {
            bool removed = false;

            // Check existing inventory for the item and remove from slots
            for (int index = 0; index < playerInventory.Count; index++)
            {
                if (playerInventory[index] != null && playerInventory[index].item == selectedItem)
                {
                    if (playerInventory[index].amount >= remainingToRemove)
                    {
                        playerInventory[index].amount -= remainingToRemove;
                        remainingToRemove = 0;

                        // Clear the slot if amount becomes zero
                        if (playerInventory[index].amount == 0)
                        {
                           
                            playerInventory[index] = null;
                        }

                        removed = true;
                       
                        break;
                    }
                    else
                    {
                        

                        remainingToRemove -= playerInventory[index].amount;
                        
                        playerInventory[index] = null;
                        removed = true;
                       
                    }
                }
            }

            // If there was no slot containing the item, stop the loop to prevent an infinite loop
            if (!removed)
            {
                break;
            }
        }
        if(isRemovingItemToWorld)
            Instantiate(selectedItem.instance, playerPos.position, Quaternion.identity);
        // Communicate to other objects
        UpdateSlots();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("SLot data "+ playerInventory[0].item);
    }

  
}
