using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HotBarModel : MonoBehaviour
{
    [SerializeField] InventorySlot slot;
    [SerializeField] int maxInventorySize;
    public List<InventorySlot> slotList = new List<InventorySlot>();
    // Start is called before the first frame update

    public List<SlotData> playerInventory = new List<SlotData>();
    public int maxInventorySlots = 16;
    const int MAXSTACKSIZE = 10;

    void Start()
    {
        for (int i = 0; i < maxInventorySlots; i++)
        {
            InventorySlot slotToBeAdded = Instantiate(slot);

            slotToBeAdded.transform.SetParent(this.transform, false);
            slotList.Add(slotToBeAdded);
        }

       
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
}
