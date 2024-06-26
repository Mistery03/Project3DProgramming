using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotSlot : MonoBehaviour, IPointerClickHandler
{
    public ItemData item;
    public int amount = 0;

    [SerializeField] UnityEngine.UI.Image texture;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] GameObject itemTexture, itemAmount; //To have visible options

    [SerializeField] UnityEngine.UI.Image border, borderHighLightTexture;
   
    public HotBarModel hotBarModel;

    public bool isSelected = false;

    public Sprite originalSprite;
    void Start()
    {

        // texture.GetComponent<UnityEngine.UI.Image>().sprite;
    }
    // Update is called once per frame
    void Update()
    {
        if(isSelected)
        {
            border.sprite = borderHighLightTexture.sprite;
        }else
        {
            border.sprite = originalSprite;
        }
            

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




    public void OnPointerEnter(PointerEventData eventData)
    {

        Debug.Log(item.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(item.name);
            hotBarModel.RemoveItem(item, 1);
        }
    }
}
