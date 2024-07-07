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


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && chemUI.activeSelf == true)
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
}
