using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHP = 100;
    private int currentHP;

    public InventoryController inventoryController;

    private GameObject carriedObject; 
    private bool isCarrying = false;
    private Vector3 carryOffset = new Vector3(-1f, 1f, -1f);

    private Rigidbody rb;
    private GameManager gameManager;

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
            if (!isCarrying)
            {
                // Perform raycast to check for objects to carry
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the object hit by the raycast is carryable
                    if (hit.collider.CompareTag("Carryable"))
                    {
                        carriedObject = hit.collider.gameObject;
                        carriedObject.transform.parent = transform;
                        isCarrying = true;

                        carriedObject.transform.localPosition = carryOffset;
                    }
                }
            }
            else
            {
                carriedObject.transform.parent = null;
                carriedObject = null;
                isCarrying = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.Instance.PlayerTakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Player has died!");
            SceneManager.LoadScene("LabArea");
        }
        Debug.Log("Player HP: " + currentHP);
        UIManager.Instance.UpdateHP(currentHP, maxHP); // Update HP text
    }
}
