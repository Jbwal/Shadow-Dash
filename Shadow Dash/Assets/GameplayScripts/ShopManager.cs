using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Prices")]
    [SerializeField] private int doubleJumpCost = 5;
    [SerializeField] private int healthCost = 3;
    [SerializeField] private int speedCost = 3;

    [Header("Buttons")]
    [SerializeField] private GameObject doubleJumpButton;
    [SerializeField] private GameObject healthButton;
    [SerializeField] private GameObject speedButton;

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinText.text = "Coins : " + CoinManager.Instance.GetCoins();

        doubleJumpButton.SetActive(!PlayerPrefs.HasKey("DoubleJump"));
        healthButton.SetActive(CoinManager.Instance.GetCoins() >= healthCost);
        speedButton.SetActive(CoinManager.Instance.GetCoins() >= speedCost);
    }

    public void BuyDoubleJump()
    {
        if (CoinManager.Instance.GetCoins() < doubleJumpCost) return;
        CoinManager.Instance.SpendCoins(doubleJumpCost);
        PlayerPrefs.SetInt("DoubleJump", 1);
        PlayerPrefs.Save();
        player.EnableDoubleJump();
        UpdateUI();
        Debug.Log("Double jump activated !");
    }

    public void BuyHealth()
    {
        if (CoinManager.Instance.GetCoins() < healthCost) return;
        CoinManager.Instance.SpendCoins(healthCost);
        playerHealth.UpgradeMaxHealth(25f);
        UpdateUI();
        Debug.Log("More HP !");
    }

    public void BuySpeed()
    {
        if (CoinManager.Instance.GetCoins() < speedCost) return;
        CoinManager.Instance.SpendCoins(speedCost);
        player.UpgradeSpeed(2f);
        UpdateUI();
        Debug.Log("Speed goes brrr !");
    }
}