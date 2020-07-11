using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManage : MonoBehaviour
{
    /// <summary>
    /// Method to simply load next scene in build index
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Method to load a specific scene by name
    /// </summary>
    /// <param name="sceneName"> Scene name to be loaded</param>
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Method to reset current scene
    /// </summary>
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Method to exit game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
