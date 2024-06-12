using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventory;
   

    // Update is called once per frame
    void Update()
    {

        toggleInventory();
       

        
    }

    void toggleInventory()
    {

        if (Input.GetKeyDown(KeyCode.I))
            inventory.SetActive(!inventory.activeSelf);
    }
}
