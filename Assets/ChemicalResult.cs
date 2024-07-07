using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ChemicalResult : MonoBehaviour
{

    public GameObject chemicalPrefab;
    public ResultSlot[] slotList;
    public bool AddItem(ChemicalData chemicalElement)
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            ResultSlot slot = slotList[i];
            Chemical itemInSlot = slot.GetComponentInChildren<Chemical>();
            if (itemInSlot != null &&
                itemInSlot.chemical == chemicalElement)
            {
                itemInSlot.amount++;
                itemInSlot.refreshCount();
                return true;
            }
          
        }

        for (int i = 0; i < slotList.Length; i++)
        {
            ResultSlot slot = slotList[i];
            Chemical itemInSlot = slot.GetComponentInChildren<Chemical>();
            if (itemInSlot == null)
            {
                SpawnItem(chemicalElement, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnItem(ChemicalData item, ResultSlot slot, int amount = 1)
    {
        GameObject newItem = Instantiate(chemicalPrefab, slot.transform);
        Chemical inventoryItem = newItem.GetComponent<Chemical>();
        inventoryItem.initiliaseChemical(item);
    }
}
