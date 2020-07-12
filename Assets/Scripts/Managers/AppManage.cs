using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManage : MonoBehaviour
{
    /// <summary>
    /// Method to simply load next scene in build index
    /// </summary>
    public void LoadNextScene(string sceneToLoad)
    {
        SceneLoader.Load(sceneToLoad);
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
