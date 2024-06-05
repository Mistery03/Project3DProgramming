using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    private GameObject carriedObject; // Reference to the object being carried
    private bool isCarrying = false; // Flag to track if the player is currently carrying an object
    private Vector3 carryOffset = new Vector3(-1f, 1f, -1f);

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Freeze rotation constraints to prevent tipping over
        rb.freezeRotation = true;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        // Check for mouse click
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
                        // Attach the object to the player capsule
                        carriedObject = hit.collider.gameObject;
                        carriedObject.transform.parent = transform;
                        isCarrying = true;

                        carriedObject.transform.localPosition = carryOffset;
                    }
                }
            }
            else
            {
                // Release the carried object
                carriedObject.transform.parent = null;
                carriedObject = null;
                isCarrying = false;
            }
        }


    }
}
