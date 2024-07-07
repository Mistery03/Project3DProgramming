using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResultSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  
{
    public ChemicalDescHover cdHover;

  
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Chemical itemInSlot = GetComponentInChildren<Chemical>();

        if (itemInSlot != null) 
        {
            cdHover.gameObject.SetActive(true);
            cdHover.setHoverContent(itemInSlot.chemical);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cdHover.gameObject.SetActive(false);
    }
}
