using UnityEngine;
using UnityEngine.SceneManagement;
using TPS.Characters;

public class End : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadSceneNumberThree();
        }
    }

    private void LoadSceneNumberThree()
    {
        SceneManager.LoadScene(4);
    }
}
