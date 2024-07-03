using Inventory.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPointerClickHandler
{
    public float moveSpeed = 20f;
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
    public ItemData appleData;

    public HotBarModel hotBarModel;
    public GameObject taskUI, hotbarObject;
    public TaskPanel taskPanel;

    public bool isTask1done = false;
    public bool isTask2done = false;
    public bool isTask3done = false;

    public bool isWoodCollected = false;
    public bool isAppleCollected = false;
    public bool isUraniumCollected = false;

    public float throwForce = 10f;
    public float maxThrowForce = 30f;
    public float aimRadius = 5f;

    private bool isAiming = false;
    private float currentThrowForce;

    public LineRenderer aimLineRenderer;

    public Text hpText;

    public ChemicalList chemicalList;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentHP = maxHP;

        UIManager.Instance.setHpText(hpText);
        UIManager.Instance.UpdateHP(currentHP, maxHP); // Initialize HP text
        
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        if (aimLineRenderer == null)
        {
            Debug.LogError("Aim LineRenderer not assigned.");
        }
        else
        {
            aimLineRenderer.positionCount = 2; // Set to 2 points (start and end)
            aimLineRenderer.enabled = false; // Disable initially
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
            TryPickUpObject();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartAiming();
        }

        if (Input.GetMouseButtonUp(1))
        {
            ThrowObject();
        }

        if (isAiming)
        {
            AimObject();
        }

        if (isCarrying && carriedObject != null)
        {
            CarryObject();
        }

        if (Input.GetMouseButtonDown(1))
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
                    {
                        inventoryController.inventoryModel.Insert(woodData, 1);
                        isWoodCollected = true;
                        Debug.Log("isWoodCollected:" + isWoodCollected); 
                    }
                    else if (carriedObject.name == "uranium(Clone)")
                    {
                        inventoryController.inventoryModel.Insert(uraniumData, 1);
                        isUraniumCollected = true;
                        Debug.Log("isUraniumColleted:" + isUraniumCollected);
                    }
                    else if (carriedObject.name == "apple(Clone)")
                    {
                        inventoryController.inventoryModel.Insert(appleData, 1);
                        isAppleCollected = true;
                        Debug.Log("isAppleColleted:" + isAppleCollected);
                    }


                    Destroy(carriedObject);
                }
            }    
        }else if (Input.GetKeyDown(KeyCode.B))
        {
         
            if (taskUI != null)
            {
                taskUI.SetActive(!taskUI.activeSelf);
                if (taskUI.activeSelf)
                {
                    Debug.Log("Taskui activated");
                    
                }

            }
        }
    }


    private void TryPickUpObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the object hit by the raycast is carryable
            if (hit.collider.CompareTag("Carryable"))
            {
                carriedObject = hit.collider.gameObject;
                isCarrying = true;

                carriedObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics while carrying
            }
        }
    }

    private void CarryObject()
    {
        if (carriedObject != null)
        {
            // Rotate the object to face the mouse position
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 directionToMouse = (worldMousePosition - transform.position).normalized;
            carriedObject.transform.position = transform.position + directionToMouse * carryOffset.magnitude;
            carriedObject.transform.rotation = Quaternion.LookRotation(directionToMouse);
        }
    }

    private void StartAiming()
    {
        if (isCarrying && carriedObject != null)
        {
            isAiming = true;
            currentThrowForce = throwForce;
            aimLineRenderer.enabled = true; // Enable the line renderer
        }
    }

    private void AimObject()
    {
        if (isAiming)
        {
            currentThrowForce += Time.deltaTime * throwForce; // Increase throw force over time
            if (currentThrowForce > maxThrowForce)
            {
                currentThrowForce = maxThrowForce;
            }

            // Update aim line renderer
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            aimLineRenderer.SetPosition(0, transform.position + carryOffset); // Start point
            aimLineRenderer.SetPosition(1, worldMousePosition); // End point
        }
    }

    private void ThrowObject()
    {
        if (isAiming && isCarrying && carriedObject != null)
        {
            carriedObject.GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 throwDirection = (worldMousePosition - transform.position).normalized;

            carriedObject.GetComponent<Rigidbody>().AddForce(throwDirection * currentThrowForce, ForceMode.VelocityChange);

            carriedObject = null;
            isCarrying = false;
            isAiming = false;
            aimLineRenderer.enabled = false; // Disable the line renderer
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
