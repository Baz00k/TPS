using UnityEngine;
using UnityEngine.SceneManagement;

namespace TPS.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
