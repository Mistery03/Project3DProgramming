using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 13f, -5f);

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = target.position + target.TransformDirection(offset);

            // Set the position of the camera
            transform.position = desiredPosition;

            // Make the camera look at the target
            transform.LookAt(target);
        }
        else
        {
            Debug.LogWarning("No target assigned to the camera. Please assign a target.");
        }
    }
}
