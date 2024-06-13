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

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SLot data "+ playerInventory[0].item);
    }
}
