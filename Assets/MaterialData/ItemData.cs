using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {

        public int ID = -1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [field: SerializeField]
        public GameObject instance {  get; set; }

        [field: SerializeField]
        public ItemType ItemType { get; set; }

        [field: SerializeField]
        public bool stackable = true;

    }

    public enum ItemType
    {
        Food,
        Radiation,
        EarthElement,
    }




}

