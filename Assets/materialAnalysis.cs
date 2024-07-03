using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class materialAnalysis : MonoBehaviour, IPointerClickHandler
{
    public GameObject materialAnalyserObject, chemicalDescObject;
    public Player player;
    public GameManager gameManager;

    public ChemicalData uraniumData;
    public ChemicalData carbonData;
    public ChemicalData oxygenData;
    public ChemicalData mercuryData;
    public ChemicalData hydrogenData;

    public ItemData apple;
    public ItemData wood;
    public ItemData uranium;


    public TextMeshProUGUI chemicalName;
    public TextMeshProUGUI chemicalDesc;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button==0)
        {
            materialAnalyserObject.SetActive(true);
            player.hotbarObject.SetActive(false);
            player.inventoryController.chemicalListObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        player = gameManager.getPlayerScript();

        if (materialAnalyserObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            { 
                materialAnalyserObject.SetActive(false);
                chemicalDescObject.SetActive(false);
                player.hotbarObject.SetActive(true);
                player.inventoryController.chemicalListObject.SetActive(false);
            }
        }
    }

    public void test()
    {
        Debug.Log("Cliecked");
    }

    public void uraniumOnClicked()
    {
        if (player.inventoryController.inventoryModel.PlayerHasItem(uranium))
        {
            
            chemicalDescObject.SetActive(true);
            chemicalName.text = uraniumData.name;
            chemicalDesc.text = uraniumData.Description;
            player.inventoryController.inventoryModel.Remove(uranium, 1, false);
            player.chemicalList.uraniumAmount += 1;
            player.chemicalList.uraniumAmountText.text = player.chemicalList.uraniumAmount.ToString();
        }
    }

    public void onAppleClicked()
    {
        if (player.inventoryController.inventoryModel.PlayerHasItem(apple))
        {
            
            chemicalDescObject.SetActive(true);
            chemicalName.text = carbonData.name;
            chemicalDesc.text = carbonData.Description;
            player.inventoryController.inventoryModel.Remove(apple, 1, false);
            player.chemicalList.carbonAmount += 1;
            player.chemicalList.carbonAmountText.text = player.chemicalList.carbonAmount.ToString();
        }
    }

    public void onWoodClicked()
    {
        if (player.inventoryController.inventoryModel.PlayerHasItem(wood))
        {
           
            chemicalDescObject.SetActive(true);
            chemicalName.text = oxygenData.name;
            chemicalDesc.text = oxygenData.Description;
            player.inventoryController.inventoryModel.Remove(wood, 1, false);
            player.chemicalList.oxygenAmount += 1;
            player.chemicalList.oxygenAmountText.text = player.chemicalList.oxygenAmount.ToString();
        }
       
    }
}
