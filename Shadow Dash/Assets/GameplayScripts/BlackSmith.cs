using UnityEngine;
using UnityEngine.InputSystem;

public class BlackSmith : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    private Player player;
    private bool playerNearby = false;
    private bool isOpen = false;

    private void Update()
    {
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            isOpen = !isOpen;
            shopUI.SetActive(isOpen);
            player.SetFrozen(isOpen);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        playerNearby = true;
        player = collision.GetComponent<Player>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        playerNearby = false;
        isOpen = false;
        shopUI.SetActive(false);
        player?.SetFrozen(false);
        player = null;
    }
}