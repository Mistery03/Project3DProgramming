using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HotBarModel : MonoBehaviour
{
    [SerializeField] HotSlot slot;
    [SerializeField] int maxInventorySize;
    public List<HotSlot> slotList = new List<HotSlot>();
    // Start is called before the first frame update

    public List<SlotData> playerInventory = new List<SlotData>();
    public int maxInventorySlots = 16;
    const int MAXSTACKSIZE = 10;

    ItemData currItem = null;
    int currItemAmount = 0;

    public Transform playerPos;


    void Start()
    {

        for (int i = 0; i < maxInventorySlots; i++)
        {
            HotSlot slotToBeAdded = Instantiate(slot);
            slotToBeAdded.hotBarModel = this;
            slotToBeAdded.transform.SetParent(this.transform, false);
            slotList.Add(slotToBeAdded);
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }
    }

    void SelectSlot(int index)
    {
        // Deselect all slots
        for (int i = 0; i < 4; i++)
        {
            slotList[i].isSelected = false;
        }

        // Select the specified slot
        if (index >= 0 && index < 4)
        {
            slotList[index].isSelected = true;
            currItem = slotList[index].item;
            currItemAmount = slotList[index].amount;
            Debug.Log(currItem);
        }
    }

    public ItemData getCurrentItem()
    {
        return currItem;
    }

    public int getCurrentItemAmount()
    {
        return currItemAmount;
    }

    public void UpdateSlots()
    {
       

        for (int i = 0; i < maxInventorySlots; i++)
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

    public bool InsertItem(ItemData item, int amount)
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
                    return true;
                    //break;
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
                    return true;
                    //break;

                }
            }
        }
        return false;
    }

    public void RemoveItem(ItemData item, int amount, bool isRemoveToWorld = true)
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
                        if(isRemoveToWorld)
                            Instantiate(slot.item.instance, playerPos.position, Quaternion.identity);
                        slot.ClearSlot();
                    }
                    slot.UpdateSlot();
                    return;
                }
                else
                {
                    if (isRemoveToWorld)
                        Instantiate(slot.item.instance, playerPos.position, Quaternion.identity);
                    amount -= slot.amount;
                    slot.ClearSlot();
                    slot.UpdateSlot();
                }
            }
        }

        
        

    }
}
