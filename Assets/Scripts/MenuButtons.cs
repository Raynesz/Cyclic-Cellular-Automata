using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}