using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject scientist;
    public GameObject conversationUI;
    public float interactionDistance = 3f; // Set the distance to interact
    public KeyCode interactionKey = KeyCode.E; // Key to interact

    private bool isNearScientist = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, scientist.transform.position);
        //Debug.Log(distance);
        // Check the distance between the player and the scientist
        if (Vector3.Distance(transform.position, scientist.transform.position) <= interactionDistance)
        {
            isNearScientist = true;


            // Show interaction hint (Optional)
            Debug.Log("Press 'E' to interact with the scientist");
        }
        else
        {
            isNearScientist = false;
            //Debug.Log(isNearScientist);
        }

        // Check for interaction key press
        if (isNearScientist && Input.GetKeyDown(interactionKey))
        {
            ToggleConversationUI();
        }
    }

    void ToggleConversationUI()
    {
        if (conversationUI != null)
        {
            conversationUI.SetActive(!conversationUI.activeSelf);
        }
    }

}
