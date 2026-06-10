using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private RectTransform healthBarRect;
    private float barMaxWidth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        barMaxWidth = healthBarRect.sizeDelta.x;
        UpdateBar();
    }

    private bool isInvincible = false;

    public void SetInvincible(bool state)
    {
        isInvincible = state;
    }


    public void TakeDamage(float amount)
    {
        if (isDead || isInvincible) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateBar();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        if (isDead) return;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateBar();
    }

    private void UpdateBar()
    {
        if (healthBarRect == null) return;
        float newWidth = (currentHealth / maxHealth) * barMaxWidth;
        healthBarRect.sizeDelta = new Vector2(newWidth, healthBarRect.sizeDelta.y);
    }

    public void Die()
    {
        isDead = true;
        //Debug.Log("You Died :/");
        SceneManager.LoadScene("GameOver");
    }
    public void UpgradeMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateBar();
    }
}