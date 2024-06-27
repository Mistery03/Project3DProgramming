using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Scientist : MonoBehaviour, IPointerClickHandler
{

    public GameObject Task1Convo;
    public GameObject Task2Convo;
    public GameObject Task3Convo;
    public Dialogue dialougue1;
    public Player player;
    public GameManager gameManager;

    
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
            Debug.Log("task1: "+ player.isTask1done);
        }else if(player.isTask2done == false)
        {
            //check if the item collect
            if(player.isWoodCollected == true && player.isAppleCollected == true && player.isWoodCollected == true)
            {
                player.isTask2done = true;
                Debug.Log("task2: " + player.isTask2done);
                ToggleTask2DialogueUI();
            }
            else
            {
                ToggleTask1DialogueUI();
            }
        }else if(player.isTask3done == false)
        {
            ToggleTask3DialogueUI();
            player.isTask3done = true;
            Debug.Log("task3: " + player.isTask3done);
        }


    }
    void ToggleTask1DialogueUI()
    {
        if (Task1Convo != null)
        {
            Task1Convo.SetActive(!Task1Convo.activeSelf);
            if(Task1Convo.activeSelf )
            {
                Debug.Log("activated");
                //dialougue.startDialogue();
            }
            
        }
    }

    void ToggleTask2DialogueUI()
    {
        if (Task2Convo != null)
        {
            Task2Convo.SetActive(!Task2Convo.activeSelf);
            if (Task2Convo.activeSelf)
            {
                Debug.Log("activated");
                //dialougue.startDialogue();
            }

        }
    }

    void ToggleTask3DialogueUI()
    {
        if (Task3Convo != null)
        {
            Task3Convo.SetActive(!Task3Convo.activeSelf);
            if (Task3Convo.activeSelf)
            {
                Debug.Log("activated");
                //dialougue.startDialogue();
            }

        }
    }
}
