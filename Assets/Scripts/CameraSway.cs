using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float swayAmount = 0.05f; // The amount the camera will sway
    public float smoothFactor = 2.0f; // How quickly the camera follows the mouse

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition; // Save the initial position of the camera
    }

    private void Update()
    {
        // Get the mouse position as a normalized value between -1 and 1
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;

        // Calculate the target position based on the mouse position
        Vector3 targetPosition = new Vector3(
            initialPosition.x + mouseX * swayAmount,
            initialPosition.y + mouseY * swayAmount,
            initialPosition.z
        );

        // Smoothly interpolate between the current position and the target position
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothFactor);
    }
}
