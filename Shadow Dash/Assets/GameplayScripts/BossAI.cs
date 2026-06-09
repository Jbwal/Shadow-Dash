using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("Cibles")]
    public Transform player;
    public Animator anim;

    [Header("Distances de comportement")]
    public float runDistance = 10f;
    // J'augmente légèrement à 2f pour être sûr que les colliders ne bloquent pas l'attaque
    public float attackDistance = 2f;

    [Header("Vitesses de déplacement")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    [Header("Combat (Attaque)")]
    public float damage = 15f;
    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    private bool facingRight = true;
    private PlayerHealth playerHealth;

    void Start()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player == null) return;

        // --- CORRECTION 1 : Calcul de la distance purement en 2D (ignore l'axe Z) ---
        Vector2 bossPos2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos2D = new Vector2(player.position.x, player.position.y);
        float distanceToPlayer = Vector2.Distance(bossPos2D, playerPos2D);

        // --- GESTION DU FLIP 2D ---
        if (player.position.x < transform.position.x && !facingRight) Flip();
        else if (player.position.x > transform.position.x && facingRight) Flip();

        // --- GESTION DES ÉTATS ---

        // Cas 1 : À portée d'attaque !
        if (distanceToPlayer <= attackDistance)
        {
            // CORRECTION 2 : On force l'Animateur à passer à 0 IMMEDIATEMENT
            if (anim != null) anim.SetFloat("Speed", 0f);

            if (Time.time >= nextAttackTime)
            {
                TriggerAttack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        // Cas 2 : Trop loin pour attaquer, assez proche pour marcher
        else if (distanceToPlayer <= runDistance)
        {
            MoveTowardsPlayer2D(walkSpeed);
            if (anim != null) anim.SetFloat("Speed", walkSpeed);
        }
        // Cas 3 : Très loin -> Course
        else if (distanceToPlayer > runDistance)
        {
            MoveTowardsPlayer2D(runSpeed);
            if (anim != null) anim.SetFloat("Speed", runSpeed);
        }
    }

    void MoveTowardsPlayer2D(float speed)
    {
        Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void TriggerAttack()
    {
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void HitPlayer()
    {
        if (playerHealth != null) playerHealth.TakeDamage(damage);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}