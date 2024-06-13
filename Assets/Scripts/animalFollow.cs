using UnityEngine;

public class animalFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
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
        transform.rotation = Quaternion.Euler(0, 1, 0);
    }

    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
        isFollowing = newTarget != null;
    }

    private void Update()
    {
        if (isFollowing && Target != null)
        {
            FollowTarget();
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

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveRandomly()
    {
        Vector3 moveDirection = (randomTarget - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, randomTarget) < 0.5f)
        {
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        float newX = Mathf.Clamp(transform.position.x + Random.Range(-5f, 5f), 0, terrainWidth);
        float newZ = Mathf.Clamp(transform.position.z + Random.Range(-5f, 5f), 0, terrainHeight);
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
                    SetTarget(collider.transform);
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
