using UnityEngine;

public class ZoneEffect : MonoBehaviour
{
    public enum ZoneType { Shadow, Light }
    [SerializeField] private ZoneType zoneType;
    [SerializeField] private float amountPerSecond = 10f;

    private PlayerHealth playerHealth;
    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        playerHealth = collision.GetComponent<PlayerHealth>();
        playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        playerInside = false;
        playerHealth = null;
    }

    private void Update()
    {
        if (!playerInside || playerHealth == null) return;

        if (zoneType == ZoneType.Shadow)
            playerHealth.TakeDamage(amountPerSecond * Time.deltaTime);
        else
            playerHealth.Heal(amountPerSecond * Time.deltaTime);
    }
}