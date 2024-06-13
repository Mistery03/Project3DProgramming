using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlot : MonoBehaviour
{
    public ItemData item;
    public int amount = 0;

    [SerializeField] UnityEngine.UI.Image texture;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] GameObject itemTexture, itemAmount; //To have visible options

   

    void Start()
    {
    
       // texture.GetComponent<UnityEngine.UI.Image>().sprite;
    }
    // Update is called once per frame
    void Update()
    {
       
        if(item != null) 
        {
            itemTexture.SetActive(true);
            itemAmount.SetActive(true);
            texture.sprite = item.ItemImage;
            amountText.text = amount.ToString();
        }else
        {
            itemTexture.SetActive(false);
            itemAmount.SetActive(false);

        }
    }

    public void UpdateSlot()
    {
        if (item != null)
        {
            itemTexture.SetActive(true);
            itemAmount.SetActive(true);
            texture.sprite = item.ItemImage;
            amountText.text = amount.ToString();
        }
        else
        {
            itemTexture.SetActive(false);
            itemAmount.SetActive(false);

        }
    }

    public void ClearSlot()
    {
        item = null;
        amount = 0;
        UpdateSlot();
    }
}
