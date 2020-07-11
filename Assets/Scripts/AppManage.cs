using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManage : MonoBehaviour
{

    [SerializeField] private string _sceneToLoad = default;

    /// <summary>
    /// Method to simply load next scene in build index
    /// </summary>
    public void LoadNextScene()
    {
        SceneLoader.Load(_sceneToLoad);
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
