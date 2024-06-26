using Inventory.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxHP = 100;
    private float currentHP;

    public InventoryController inventoryController;

    private GameObject carriedObject; 
    private bool isCarrying = false;
    private Vector3 carryOffset = new Vector3(-1f, 1f, -1f);

    private Rigidbody rb;
    private GameManager gameManager;

    public ItemData woodData;
    public ItemData uraniumData;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentHP = maxHP;
        UIManager.Instance.UpdateHP(currentHP, maxHP); // Initialize HP text

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;

        // Update Rigidbody velocity instead of transforming position directly
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object hit by the raycast is carryable
                if (hit.collider.CompareTag("Carryable"))
                {
                    carriedObject = hit.collider.gameObject;

                    Debug.Log(carriedObject.name);

                    if (carriedObject.name == "Wood(Clone)")
                        inventoryController.inventoryModel.Insert(woodData, 1);
                    else if (carriedObject.name == "uranium(Clone)")
                        inventoryController.inventoryModel.Insert(uraniumData, 1);
                    
                    Destroy(carriedObject);
                }
            }    
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.Instance.PlayerTakeDamage(10);
        }
        else if (collision.collider.CompareTag("DeathPoint"))
        {
            TakeDamage(100); // Apply 100 damage if colliding with DeathPoint
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Player has died!");
            GameManager.Instance.AudioDestroy();

            // Show the death panel
            UIManager.Instance.ShowDeathPanel();

            // Optionally, stop the game or do other death-related actions here
        }
        Debug.Log("Player HP: " + currentHP);
        UIManager.Instance.UpdateHP(currentHP, maxHP); // Update HP text
    }
}
