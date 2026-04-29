using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}