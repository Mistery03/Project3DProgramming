using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ChemicalAnalyser : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject chemUI, hoverText;
    public TextMeshProUGUI textHover;
    public ChemicalResult resultManager;

    public ChemicalData dummyData;

    public InventorySlot materialSlot;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && chemUI.activeSelf == true)
        {

           chemUI.SetActive(!chemUI.activeSelf);

        }

 

       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            chemUI.SetActive(true);
        }
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverText.SetActive(true);
        textHover.text = "Open Material Analyser?";
        hoverText.transform.position = eventData.position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.SetActive(false);
    }

    public void analyseMaterial()
    {
        Item itemInSlot = materialSlot.GetComponentInChildren<Item>();
        if (itemInSlot != null)
        {
            itemInSlot.amount--;
            ChemicalData[] chemicalList = itemInSlot.item.ChemicalData;
            for (int i = 0; i < chemicalList.Length; i++)
            {
               if(resultManager.AddItem(chemicalList[i]))
                {
                   
                    if (itemInSlot.amount <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                    }
                    else
                    {
                        itemInSlot.refreshCount();
                    }

                }
            }

        }

        
    }
}
