using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SlotData : ScriptableObject
{
   

    [field: SerializeField]
    public ItemData item { get; set; }

    [field: SerializeField]
    public int amount { get; set; }


    
}
