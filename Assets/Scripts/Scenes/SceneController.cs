using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeLearn.Scenes
{
    public class SceneController : MonoBehaviour
    {
        public void NextLevel()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
