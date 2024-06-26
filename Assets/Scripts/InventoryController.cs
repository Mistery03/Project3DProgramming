using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryObject, hotbarObject;

    public InventoryController inventoryController;

    public InventoryView inventoryModel;
    public List<SlotData> playerInventory = new List<SlotData>();
    public int maxInventorySlots = 16;

    public ItemData testApple;

    public HotBarModel hotBarModel;

    private void Start()
    {
        inventoryModel.playerInventory = playerInventory;
        inventoryModel.maxInventorySlots = maxInventorySlots;

        //hotBarModel.playerInventory = playerInventory;
    
    }

    // Update is called once per frame
    void Update()
    {
        inventoryModel.playerInventory = playerInventory;
        inventoryModel.maxInventorySlots = maxInventorySlots;

        hotBarModel.playerInventory = playerInventory;
        hotBarModel.maxInventorySlots = maxInventorySlots;

        toggleInventory();
       
        
    }

    void toggleInventory()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryObject.SetActive(!inventoryObject.activeSelf);
            hotbarObject.SetActive(!hotbarObject.activeSelf);
        }
            

        if (Input.GetKeyDown(KeyCode.L))
            inventoryModel.InsertItem(testApple, 1);
    }

   
}
