using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SmoothPlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float smoothSpeed = 0.125f;

    [SerializeField] private Vector3 offset = new(0, 0, -10);

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
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
