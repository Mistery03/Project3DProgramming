using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Scientist : MonoBehaviour, IPointerClickHandler
{

    public GameObject conversationUI;
    public Dialogue dialougue;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Been click");
            ToggleConversationUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleConversationUI()
    {
        if (conversationUI != null)
        {
            conversationUI.SetActive(!conversationUI.activeSelf);
            if(conversationUI.activeSelf )
            {
                Debug.Log("activated");
                //dialougue.startDialogue();
            }
            
        }
    }
}
