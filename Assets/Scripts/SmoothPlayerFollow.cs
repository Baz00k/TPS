using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SmoothPlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    [Tooltip("How smooth the camera follows the player. Lower values are stiffer, higher values are smoother.")]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    [Range(0.01f, 10.0f)]
    [Tooltip("How much the mouse affects the camera movement. Lower values are less affected, higher values are more affected.")]
    private float mouseOffsetMultiplier = 5.0f;

    [SerializeField]
    [Tooltip("The offset of the camera from the player.")]
    private Vector3 offset = new(0, 0, -10);

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not set in SmoothPlayerFollow script");
        }
    }

    private void FixedUpdate()
    {
        Vector3 mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized * mouseOffsetMultiplier;
        Vector3 desiredPosition = target.position + offset + mouseOffset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
