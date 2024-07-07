using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chemical : MonoBehaviour, IPointerClickHandler
{
    public ChemicalData chemical;
    public Image image;

    [Header("Item Amount")]
    public int amount = 1;
    public TextMeshProUGUI countText;
    private void Start()
    {

        initiliaseChemical(chemical);
    }
    public void initiliaseChemical(ChemicalData newItem)
    {
        chemical = newItem;
        image.sprite = newItem.ItemImage;

        refreshCount();

    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
  
    }

    public void refreshCount()
    {
        countText.text = amount.ToString();
        bool textActive = amount > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Player player = FindAnyObjectByType(typeof(Player)) as Player;  

            if (player != null)
            {
                player.chemicalList.gameObject.SetActive(true);
                player.chemicalList.addChemical(chemical, amount);
                player.chemicalList.refreshCount();
                Destroy(this.gameObject);
            }
        }
       
    }
}
