using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Clé PlayerPrefs pour sauvegarder le niveau
    private const string LAST_SCENE_KEY = "LastScene";

    private void Start()
    {
        // Si aucune sauvegarde, désactive le bouton Continue
        GameObject btnContinue = GameObject.Find("BtnContinue");
        if (btnContinue != null)
        {
            UnityEngine.UI.Button btn = btnContinue.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
                btn.interactable = PlayerPrefs.HasKey(LAST_SCENE_KEY);
        }
    }

    public void NewGame()
    {
        // Supprime toutes les sauvegardes (upgrades, niveau, pièces)
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Sauvegarde Level_1 comme scène de départ
        PlayerPrefs.SetString(LAST_SCENE_KEY, "Level_1");
        PlayerPrefs.Save();

        SceneManager.LoadScene("Level_1");
    }

    public void Continue()
    {
        string lastScene = PlayerPrefs.GetString(LAST_SCENE_KEY, "Level_1");
        SceneManager.LoadScene(lastScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }
}