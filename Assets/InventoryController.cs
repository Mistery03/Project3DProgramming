using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{

    public int maxStackItem = 10;
    public InventorySlot[] slotList;
    public GameObject ItemInventoryPrefab, inventoryParent, inventoryBtn, debugMenu, debugChemicalMenu, debugTaskMenu, taskUI;
    public int[] itemIDList, itemAmountList, itemExistList;
    public ChemicalList chemicalList;

    public ItemData dummyData;

    [System.Serializable]
    public struct itemLookUp
    {
        public int itemID;
        public ItemData item;
    }

    public itemLookUp[] itemLookUpList;
    

    int selectedSlot = -1;

    private void Start()
    {
        changeSelectedSlot(0);
    }

    private void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number); 
            if(isNumber && number > 0 && number < 10) 
            { 
                changeSelectedSlot(number - 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {

            inventoryParent.SetActive(!inventoryParent.activeSelf);
            inventoryBtn.SetActive(!inventoryBtn.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && inventoryParent.activeSelf == true) 
        { 
 
            inventoryParent.SetActive(!inventoryParent.activeSelf);
            inventoryBtn.SetActive(!inventoryBtn.activeSelf);
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            debugMenu.SetActive(!debugMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            debugChemicalMenu.SetActive(!debugChemicalMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            debugTaskMenu.SetActive(!debugTaskMenu.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (taskUI != null)
            {
                taskUI.SetActive(!taskUI.activeSelf);


            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && taskUI.activeSelf)
        {
            taskUI.SetActive(!taskUI.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            chemicalList.gameObject.SetActive(!chemicalList.gameObject.activeSelf);

        }
        if (Input.GetKeyDown(KeyCode.Escape) && chemicalList.gameObject.activeSelf)
        {
            chemicalList.gameObject.SetActive(!chemicalList.gameObject.activeSelf);

        }
    }
    public void changeSelectedSlot(int slotIndex)
    {   
        if(selectedSlot >= 0)
            slotList[selectedSlot].deselectSlot();

        slotList[slotIndex].selectSlot();
        selectedSlot = slotIndex;
    }

    public bool searchItem(ItemData item)
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            InventorySlot slot = slotList[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if (itemInSlot != null &&
                itemInSlot.item == item)
            {
                return true;
            }
        }
        return false;
    }
    public bool AddItem(ItemData item)
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            InventorySlot slot = slotList[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.amount < maxStackItem &&
                itemInSlot.item.stackable == true)
            {
                itemInSlot.amount++;
                itemInSlot.refreshCount();
                return true;
            }
        }

        for (int i = 0; i < slotList.Length; i++) 
        { 
            InventorySlot slot = slotList[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if(itemInSlot == null)
            {
                SpawnItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnItem(ItemData item, InventorySlot slot, int amount = 1)
    {
        GameObject newItem = Instantiate(ItemInventoryPrefab, slot.transform);
        Item inventoryItem = newItem.GetComponent<Item>();
        inventoryItem.SetAmount(amount);
        inventoryItem.initiliaseItem(item);
    }

    public ItemData getSelectedItem(bool useItem)
    {
        InventorySlot slot = slotList[selectedSlot];
        Item itemInSlot = slot.GetComponentInChildren<Item>();
        if (itemInSlot != null)
        {
            ItemData item = itemInSlot.item;
            if (useItem == true)
            {
                itemInSlot.amount--;
                if(itemInSlot.amount <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }else
                {
                    itemInSlot.refreshCount();
                }
            }
            return item;
        }
        return null;
    }

    public int[] getAllItemID() 
    {
       
        for(int i = 0;i<slotList.Length;i++)
        {
            InventorySlot slot = slotList[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if(itemInSlot != null)
            {
                itemIDList[i] = itemInSlot.item.ID;

                
            }else
            {
                itemIDList[i] = -1;

            }
        }

        return itemIDList;
    }

    public int[] getAllItemAmount()
    {

        for (int i = 0; i < slotList.Length; i++)
        {
            InventorySlot slot = slotList[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if (itemInSlot != null)
            {
     
                itemAmountList[i] = itemInSlot.amount;
            

            }
            else
            {
         
                itemAmountList[i] = 0;
    
            }
        }

        return itemAmountList;
    }


    public int[] getAllItemExistence()
    {

        for (int i = 0; i < slotList.Length; i++)
        {
            InventorySlot slot = slotList[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();
            if (itemInSlot != null)
            {

                itemExistList[i] = 1;

            }
            else
            {
      
                itemExistList[i] = 0;
            }
        }

        return itemExistList;
    }

    public void addItemViaItemID()
    {

        for (int i = 0; i < itemExistList.Length; i++)
        {
            if (itemExistList[i] == 1)
            {
                InventorySlot slot = slotList[i];
                Item itemInSlot = slot.GetComponentInChildren<Item>();
                if (itemInSlot == null)
                {
                    int itemID = itemIDList[i];
                    int amount = itemAmountList[i];
                    ItemData itemData = GetItemDataByID(itemID);
                    if (itemData != null)
                    {
                        SpawnItem(itemData, slot,amount);
                       
                    }
                    else
                    {
                        Debug.LogWarning("ItemData not found for itemID: " + itemID);
                    }
                }

            }

        }
    }

    ItemData GetItemDataByID(int itemID)
    {
        foreach (var lookup in itemLookUpList)
        {
            if (lookup.itemID == itemID)
            {
                return lookup.item;
            }
        }
        return null; // Or handle the case where the itemID is not found
    }


}
