using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    private GameObject carriedObject; 
    private bool isCarrying = false;
    private Vector3 carryOffset = new Vector3(-1f, 1f, -1f);

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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
}
