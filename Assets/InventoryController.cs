using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryObject, hotbarObject,chemicalList;
    public InventoryModel inventoryModel;
    public List<SlotData> playerInventory = new List<SlotData>();
    public int maxInventorySlots = 16;

    public ItemData testApple;

    public HotBarModel hotBarModel;

    private void Start()
    {
        inventoryModel.playerInventory = playerInventory;
        inventoryModel.maxInventorySlots = maxInventorySlots;

   
    
    }

    // Update is called once per frame
    void Update()
    {
        inventoryModel.playerInventory = playerInventory;

     

        toggleInventory();
       
        
    }

    void toggleInventory()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryObject.SetActive(!inventoryObject.activeSelf);
     
        }

        if (Input.GetKeyDown(KeyCode.C))
            chemicalList.SetActive(!chemicalList.activeSelf);


         if (Input.GetKeyDown(KeyCode.L))
            inventoryModel.Insert(testApple, 1);
        
        if(Input.GetKeyDown(KeyCode.K))
            inventoryModel.Remove(testApple,1);
    }

   
}
