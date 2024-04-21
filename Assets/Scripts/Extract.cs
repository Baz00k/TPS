using UnityEngine;
using UnityEngine.SceneManagement;
using TPS.Characters;
using UnityEngine.Events;

public class Extract : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlayerEnter.Invoke();
        }
    }
}
