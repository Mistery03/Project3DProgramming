using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Scientist : MonoBehaviour, IPointerClickHandler
{

    public GameObject Task1Convo;
    public GameObject Task2Convo;
    public GameObject Task3Convo, Task4Convo;
    public Dialogue dialougue1;
    public Player player;
    public GameManager gameManager;

    public bool isWoodCollected = false;
    public bool isUraniumCollected = false;
    public bool isAppleCollected = false;

    public ItemData apple, wood, uranium;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            
            checkTask1();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = gameManager.getPlayerScript();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        player = gameManager.getPlayerScript();
    }

    void checkTask1()
    {
        if(player.isTask1done == false)
        {
            ToggleTask1DialogueUI();
            player.isTask1done = true;
            SaveSystem.saveTask(player);
            Debug.Log("task1: "+ player.isTask1done);
        }else
        {
            if (player.isTask2done == false && player.isTask1done)
            {
                ToggleTask2DialogueUI();
                //check if the item collect

                isAppleCollected = player.inventoryController.searchItem(apple);
                isWoodCollected = player.inventoryController.searchItem(wood);
                isUraniumCollected = player.inventoryController.searchItem(uranium);


                if (isAppleCollected == true && isWoodCollected == true && isUraniumCollected == true)
                {
                    player.isTask2done = true;
                    SaveSystem.saveTask(player);
                    Debug.Log("task2: " + player.isTask2done);
                }
            }else
            {
                if (player.isTask3done == false && player.isTask2done && player.isTask1done)
                {
                    ToggleTask3DialogueUI();

                    if(player.chemicalList.hydrogenAmt >= 10 &&
                        player.chemicalList.carbonAmt >= 10 &&
                        player.chemicalList.oxygenAmt >= 10 &&
                        player.chemicalList.uraniumAmt >= 10
                        )
                        player.isTask3done = true;

                    SaveSystem.saveTask(player);
                    Debug.Log("task3: " + player.isTask3done);
                }
                else
                {
                    if (player.isTask3done && player.isTask2done && player.isTask1done && player.isTask3done)
                    {
                        ToggleTask4DialogueUI();
                        
                    }
                }
            }
        }
        
        
        


    }
    void ToggleTask1DialogueUI()
    {
        if (Task1Convo != null)
        {
            Task1Convo.SetActive(!Task1Convo.activeSelf);
          
            
        }
    }

    void ToggleTask2DialogueUI()
    {
        if (Task2Convo != null)
        {
            Task2Convo.SetActive(!Task2Convo.activeSelf);
          

        }
    }

    void ToggleTask3DialogueUI()
    {
        if (Task3Convo != null)
        {
            Task3Convo.SetActive(!Task3Convo.activeSelf);
         

        }
    }

    void ToggleTask4DialogueUI()
    {
        if (Task4Convo != null)
        {
            Task4Convo.SetActive(!Task4Convo.activeSelf);


        }
    }
}
