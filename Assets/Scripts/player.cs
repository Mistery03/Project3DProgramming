using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public InventoryController inventoryController;

    private GameObject carriedObject; 
    private bool isCarrying = false;
    private Vector3 carryOffset = new Vector3(-1f, 1f, -1f);

    private Rigidbody rb;
    public float maxHealth = 100f;
    public float currentHealth;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentHealth = maxHealth;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

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

    public void TakeDamage(float damageTiming)
    {
        currentHealth -= damageTiming;
        Debug.Log(currentHealth);
        if (currentHealth < 0f)
        {
            Debug.Log("Died");
        }
    }
}
