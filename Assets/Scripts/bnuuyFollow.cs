using UnityEngine;

public class bnuuyFollow : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the bunny moves
    public float rotationSpeed = 5f; // Speed at which the bunny rotates
    public float desiredXRotation = -90f; // Desired x-axis rotation
    public float detectionRadius = 10f; // Radius within which the bunny detects the player
    public int terrainWidth = 100; // Width of the terrain
    public int terrainHeight = 100; // Height of the terrain

    private Transform target; // Reference to the player's transform
    private bool isFollowing = false; // Flag to indicate whether the bunny is following the player
    private Vector3 randomTarget; // Target position for random movement

    public Transform Target { get; private set; } 

    private void Start()
    {
        // Set initial random target position
        SetRandomTarget();
    }

    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }

    private void Update()
    {
        if (isFollowing)
        {
            if (target != null)
            {
                FollowTarget();
            }
        }
        else
        {
            MoveRandomly();
        }
    }

    private void FollowTarget()
    {
        // Calculate the direction to move towards the player
        Vector3 moveDirection = (target.position - transform.position).normalized;

        // Move the bunny towards the player
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

        // Adjust the target rotation to keep z-axis rotation at desiredXRotation
        Vector3 euler = targetRotation.eulerAngles;
        euler.x = desiredXRotation;
        targetRotation = Quaternion.Euler(euler);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveRandomly()
    {
        // Calculate the direction to move towards the random target
        Vector3 moveDirection = (randomTarget - transform.position).normalized;

        // Move the bunny towards the random target
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

        // Adjust the target rotation to keep z-axis rotation at desiredXRotation
        Vector3 euler = targetRotation.eulerAngles;
        euler.x = desiredXRotation;
        targetRotation = Quaternion.Euler(euler);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the bunny is close to the random target
        if (Vector3.Distance(transform.position, randomTarget) < 0.5f)
        {
            // Set a new random target position
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        // Set a new random target position within the terrain bounds
        float newX = Mathf.Clamp(transform.position.x + Random.Range(-5f, 5f), 0, terrainWidth - 1);
        float newZ = Mathf.Clamp(transform.position.z + Random.Range(-5f, 5f), 0, terrainHeight - 1);
        randomTarget = new Vector3(newX, transform.position.y, newZ);
    }

    private void FixedUpdate()
    {
        if (!isFollowing)
        {
            // Check for nearby colliders to detect the player
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // Switch to follow mode if player detected
                    isFollowing = true;
                    target = collider.transform; // Set the player as the target
                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the detection radius in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
