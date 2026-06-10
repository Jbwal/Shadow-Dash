using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaver : MonoBehaviour
{
    private void Awake()
    {
        // Sauvegarde le nom de la scène actuelle dès qu'elle charge
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastScene", currentScene);
        PlayerPrefs.Save();
        Debug.Log("Scene saved : " + currentScene);
    }
}