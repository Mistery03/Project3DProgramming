using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class holeOne : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AudioDestroy();
            Player player = collision.GetComponent("Player") as Player;
            SaveSystem.savePlayer(player);
            SaveSystem.saveInventory(player.inventoryController);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            gameManager.ChangeScene("CombatArea"); 
        }
    }

}
