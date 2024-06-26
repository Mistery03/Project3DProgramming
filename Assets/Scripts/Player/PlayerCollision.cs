using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public string targetTag = "LabCube";
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            gameManager.ChangeScene("LabArea"); 
        }
    }
}
