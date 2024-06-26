using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryObject, hotbarObject;

    [Header("Inventory MVC")]
    public InventoryManager inventoryManager;
    public InventoryView inventoryView;

    [Header("Inventory Settings")]
    public List<SlotData> playerInventory = new List<SlotData>();
    public int maxInventorySlots = 16;

    public ItemData testApple;

    public HotBarModel hotBarModel;

    private void Start()
    {
        inventoryView.inventoryManager = inventoryManager;  
        inventoryView.playerInventory = inventoryManager.inventory;
        inventoryView.maxInventorySlots = inventoryManager.maxInventorySize;

        //hotBarModel.playerInventory = playerInventory;
    
    }

    // Update is called once per frame
    void Update()
    {
        inventoryView.playerInventory = playerInventory;

        //hotBarModel.playerInventory = playerInventory;
        //hotBarModel.maxInventorySlots = maxInventorySlots;

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
            inventoryManager.insert(testApple, 1);
    }

   
}
