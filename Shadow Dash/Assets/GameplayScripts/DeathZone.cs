using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
            health.Die();
    }
}