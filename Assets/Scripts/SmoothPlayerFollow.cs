using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class SmoothPlayerFollow : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 1.0f)]
    [Tooltip("How smooth the camera follows the player. Lower values are stiffer, higher values are smoother.")]
    private float smoothSpeed = 0.15f;

    [SerializeField]
    [Range(0.01f, 10.0f)]
    [Tooltip("How much the mouse affects the camera movement. Lower values are less affected, higher values are more affected.")]
    private float mouseOffsetMultiplier = 5.0f;

    [SerializeField]
    [Tooltip("The offset of the camera from the player.")]
    private Vector3 offset = new(0, 0, -10);

    [SerializeField]
    [Tooltip("How often the camera should check for the player's existence.")]
    private float findPlayerInterval = 0.1f;

    private Transform target;
    private Vector3 velocity = Vector3.zero;
    private Coroutine findPlayerCoroutine;

    private void FixedUpdate()
    {
        if (target == null)
        {
            findPlayerCoroutine ??= StartCoroutine(FindPlayerCoroutine());

            // if the player doesn't exist, don't try to follow it
            return;
        }

        Vector3 mouseOffset = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).normalized * mouseOffsetMultiplier;
        Vector3 desiredPosition = target.position + offset + mouseOffset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private IEnumerator FindPlayerCoroutine()
    {
        while (target == null)
        {
            yield return new WaitForSeconds(findPlayerInterval);

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                target = player.transform;
                findPlayerCoroutine = null;
            }
        }
    }
}
