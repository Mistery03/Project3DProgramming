using Inventory.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPointerClickHandler
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

    public HotBarModel hotBarModel;

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

    //TO Lynn, I put the hotbarmodel with it functions that is useful to you so you can avoid the sphagetti code 
    //WARNING: there a bug where you need to press 1-4 to make sure there no null
    //removeItem(ItemData item, int amount)
    //hotBarModel.RemoveItem(hotBarModel.getCurrentItem(), hotBarModel.getCurrentItemAmount())
    //hotBarModel.getCurrentItem() -> return itemData
    //hotBarModel.getCurrentItemAmount() -> return item amount in integer
    //Also Lynn you might need to check the clicking function if you wanna press outside of player (can only click on player with the function below)
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Remove upon receiving
            Debug.Log(hotBarModel.getCurrentItem());
            Debug.Log(hotBarModel.getCurrentItemAmount());
        }
        
    }
}
