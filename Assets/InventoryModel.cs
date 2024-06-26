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

    const int MAXSTACKSIZE = 10;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(playerInventory.Count);
        for (int i = 0; i < maxInventorySize; i++) 
        {
            InventorySlot slotToBeAdded = Instantiate(slot);

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

    public void InsertItem(ItemData item, int amount)
    {
        int overflow = amount;

        foreach (var slot in slotList)
        {
            if (slot.item == item && slot.amount < MAXSTACKSIZE)
            {
                int availableSpace = MAXSTACKSIZE - slot.amount;
                if (overflow <= availableSpace)
                {
                    slot.amount += overflow;
                    overflow = 0;
                    break;
                }
                else
                {
                    slot.amount += availableSpace;
                    overflow -= availableSpace;
                }

                slot.UpdateSlot();
            }
        }

        if (overflow > 0)
        {
            foreach (var slot in slotList)
            {
                if (slot.item == null)
                {
                    slot.item = item;
                    if (overflow <= MAXSTACKSIZE)
                    {
                        slot.amount = overflow;
                        overflow = 0;
                    }
                    else
                    {
                        slot.amount = MAXSTACKSIZE;
                        overflow -= MAXSTACKSIZE;
                    }

                    slot.UpdateSlot();
                    break;
                }
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

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("SLot data "+ playerInventory[0].item);
    }

  
}
