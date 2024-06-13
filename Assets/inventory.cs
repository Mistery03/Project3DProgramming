using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class inventory : MonoBehaviour
{

    [SerializeField] GameObject slot;
    [SerializeField] int maxInventorySize;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxInventorySize; i++) 
        {
            GameObject slotToBeAdded = Instantiate(slot);

           slotToBeAdded.transform.SetParent(this.transform,false);
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
