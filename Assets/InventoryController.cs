using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryObject;
    [SerializeField] InventoryModel inventoryModel;
    public List<SlotData> playerInventory = new List<SlotData>();
    public int maxInventorySlots = 16;


    // Update is called once per frame
    void Update()
    {
        inventoryModel.playerInventory = playerInventory;
        inventoryModel.maxInventorySlots = maxInventorySlots;
       
        toggleInventory();
       
        
    }

    void toggleInventory()
    {

        if (Input.GetKeyDown(KeyCode.I))
            inventoryObject.SetActive(!inventoryObject.activeSelf);
    }
}
