using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance; 
    private int totalCoins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            totalCoins = PlayerPrefs.GetInt("Coins", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt("Coins", totalCoins);
        PlayerPrefs.Save();
        Debug.Log("Pièces totales : " + totalCoins);
    }

    public int GetCoins()
    {
        return totalCoins;
    }

    public void SpendCoins(int amount)
    {
        totalCoins -= amount;
        totalCoins = Mathf.Max(0, totalCoins);
        PlayerPrefs.SetInt("Coins", totalCoins);
        PlayerPrefs.Save();
    }
}