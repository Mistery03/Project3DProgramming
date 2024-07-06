using Inventory.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float maxHP = 100;
    public float currentHP;

    private Animator animator;

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
    public GameObject taskUI;
    public TaskPanel taskPanel;

    public bool isTask1done = false;
    public bool isTask2done = false;
    public bool isTask3done = false;
    public bool isTask4done = false;

    public bool isWoodCollected = false;
    public bool isUraniumCollected = false;
    public bool isAppleCollected = false;

    public float throwForce = 10f;
    public float maxThrowForce = 30f;
    public float aimRadius = 5f;

    private bool isAiming = false;
    private float currentThrowForce;

    public LineRenderer aimLineRenderer;

    public Text hpText;

    private void Start()
    {
        
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentHP = maxHP;

        animator = GetComponent<Animator>();

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
        //TaskData taskdata = SaveSystem.loadTask();
        //if (taskdata != null)
        //{
        //    isTask1done = taskdata.taskIsDoneList[0] != 0;
        //    isTask2done = taskdata.taskIsDoneList[1] != 0;
        //    isTask3done = taskdata.taskIsDoneList[2] != 0;
        //    isTask4done = taskdata.taskIsDoneList[3] != 0;
        ///}
    }

    private void Update()
    {
        currentHP = Mathf.Clamp(currentHP, 0, 100);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Correct movement input
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Update Rigidbody velocity instead of transforming position directly
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

        // Update animator parameters
        bool isMoving = moveDirection.magnitude > 0;
        animator.SetBool("IsRunning", isMoving);

        if (isMoving)
        {
            // Calculate the direction the player should face
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Cancel pick up animation if the player starts moving
        if (isMoving && animator.GetBool("IsPickingUp"))
        {
            animator.SetBool("IsPickingUp", false);
        }

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

        if (Input.GetKeyDown(KeyCode.B))
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
            if (hit.collider.CompareTag("Carryable") || hit.collider.CompareTag("explosion"))
            {
                carriedObject = hit.collider.gameObject;
                isCarrying = true;

                carriedObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics while carrying

                // Trigger the pick-up animation
                animator.SetTrigger("PickUp");
                animator.SetBool("IsPickingUp", true);

                
                if(carriedObject == woodData)
                {
                    isWoodCollected = true;
                    Debug.Log("Wood Collected");
                }

                if (carriedObject == uraniumData)
                {
                    isUraniumCollected = true;
                    Debug.Log("Uranium Collected");
                }

                if (carriedObject == appleData)
                {
                    isAppleCollected = true;
                    Debug.Log("Apple Collected");
                }
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
            // Set animator parameters for throwing
            animator.SetBool("IsThrowing", true);
            animator.SetTrigger("Throwing");

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

            // Reset throwing state after a short delay
            StartCoroutine(ResetThrowingState());
        }
    }

    private IEnumerator ResetThrowingState()
    {
        yield return new WaitForSeconds(0.5f); // Adjust delay to match your throwing animation length
        animator.SetBool("IsThrowing", false);
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

        }else if(collision.collider.CompareTag("Carryable"))
        {
            objectBase objectInstante = collision.gameObject.GetComponent<objectBase>();
            inventoryController.AddItem(objectInstante.itemdata);
            
            switch(objectInstante.itemID)
            {
                case 0:
                    isAppleCollected = true;
                    Debug.Log("Apple Collected");
                    break;
                case 1:
                    isWoodCollected = true;
                    Debug.Log("Wood Collected");
                    break;
                case 2:
                    isUraniumCollected = true;
                    Debug.Log("Uranium Collected");
                    break;
            }
            Destroy(collision.gameObject);
        }else if(collision.collider.CompareTag("HoleEntrance"))
        {
            isTask4done = true;
            SaveSystem.saveTask(this);
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
