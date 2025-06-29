using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Drag your player GameObject here
    public float smoothSpeed = 0.125f;
    public Vector3 offset;        // You can adjust if you want the camera not centered

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z); // Lock Z to avoid weird depth issues
    }
}
