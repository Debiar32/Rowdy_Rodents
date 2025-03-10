using System.Collections;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    public float spinDamage = 30f;    // Damage dealt to enemies
    public float spinRadius = 2f;     // Range of the spin attack
    public float spinCooldown = 1.5f; // Cooldown before the next spin

    private bool canSpin = true;
    private Player_Movement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<Player_Movement>();
    }

    void Update()
    {
        if (canSpin && Input.GetButtonDown("SpinAttack")) // Replace with correct input
        {
            StartCoroutine(PerformSpinAttack());
        }
    }

    private IEnumerator PerformSpinAttack()
    {
        if (!canSpin) yield break;
        canSpin = false;

        // Set attack state
        if (playerMovement != null)
        {
            playerMovement.SetAttackState(true);
        }

        // **Create the hitbox by checking for enemies in range**
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, spinRadius);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyNew_Logic enemyLogic = enemy.GetComponent<EnemyNew_Logic>();
                if (enemyLogic != null)
                {
                    enemyLogic.TakeDamage(spinDamage); // Damage enemy
                }
            }
        }

        // Wait a bit to simulate attack animation/effect
        yield return new WaitForSeconds(0.3f);

        // Reset attack state
        if (playerMovement != null)
        {
            playerMovement.SetAttackState(false);
        }

        // Cooldown
        yield return new WaitForSeconds(spinCooldown);
        canSpin = true;
    }

    // **Optional: Visual Debugging**
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spinRadius);
    }
}
