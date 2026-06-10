using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private const string LAST_SCENE_KEY = "LastScene";

    public void Retry()
    {
        string lastScene = PlayerPrefs.GetString(LAST_SCENE_KEY, "Level_1");
        SceneManager.LoadScene(lastScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }
}