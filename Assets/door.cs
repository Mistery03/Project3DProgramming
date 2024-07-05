using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent("Player") is Player)
        {
        
            Player player = other.GetComponent("Player") as Player;
            SaveSystem.savePlayer(player);
        }
        SceneManager.LoadScene(1);
    }
}
