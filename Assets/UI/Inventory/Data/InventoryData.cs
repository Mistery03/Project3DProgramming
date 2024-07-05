using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
   
    public int[] itemIDList;
    public int[] itemAmountList;
    public int[] itemSlotExist;
    public InventoryData(InventoryController ic)
    {
       
        itemIDList = ic.getAllItemID();
        itemAmountList  = ic.getAllItemAmount();
        itemSlotExist = ic.getAllItemExistence();

    }
}
