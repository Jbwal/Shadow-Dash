using UnityEngine;

public class AnimationBridge : MonoBehaviour
{
    private BossAI bossAI;

    void Start()
    {
        // On va chercher le script BossAI qui se trouve sur le parent
        bossAI = GetComponentInParent<BossAI>();

        if (bossAI == null)
        {
            Debug.LogError("AnimationBridge : Impossible de trouver le script BossAI sur le parent !");
        }
    }

    // L'Animation Event va maintenant appeler CETTE fonction sur "Root"
    public void HitPlayer()
    {
        if (bossAI != null)
        {
            bossAI.HitPlayer(); // On transmet l'ordre au script principal
        }
    }
}