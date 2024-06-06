using UnityEngine;

public class bnuuyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float desiredXRotation = -90f;
    public float detectionRadius = 10f;
    public int terrainWidth = 100;
    public int terrainHeight = 100;

    private Transform target;
    private bool isFollowing = false;
    private Vector3 randomTarget;

    public Transform Target { get; private set; }

    private void Start()
    {
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
            if (Target != null)
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
        Vector3 moveDirection = (Target.position - transform.position).normalized;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

        Vector3 euler = targetRotation.eulerAngles;
        euler.x = desiredXRotation;
        targetRotation = Quaternion.Euler(euler);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveRandomly()
    {
        Vector3 moveDirection = (randomTarget - transform.position).normalized;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

        Vector3 euler = targetRotation.eulerAngles;
        euler.x = desiredXRotation;
        targetRotation = Quaternion.Euler(euler);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, randomTarget) < 0.5f)
        {
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        float newX = Mathf.Clamp(transform.position.x + Random.Range(-5f, 5f), 0, terrainWidth - 1);
        float newZ = Mathf.Clamp(transform.position.z + Random.Range(-5f, 5f), 0, terrainHeight - 1);
        randomTarget = new Vector3(newX, transform.position.y, newZ);
    }

    private void FixedUpdate()
    {
        if (!isFollowing)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    isFollowing = true;
                    Target = collider.transform;
                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
