using UnityEngine;
using UnityEngine.SceneManagement;
using TPS.Characters;

public class Extract : MonoBehaviour
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
        SceneManager.LoadScene(nextSceneIndex - 1, LoadSceneMode.Additive);
    }   
}
