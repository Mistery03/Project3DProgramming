using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0f, 13f, -5f);

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = playerTransform.position + offset;

            // Set the position of the camera
            transform.position = desiredPosition;

            // Make the camera look at the target
            transform.LookAt(playerTransform);
        }
        else
        {
            Debug.LogWarning("No target assigned to the camera. Please assign a target.");
        }
    }
}
