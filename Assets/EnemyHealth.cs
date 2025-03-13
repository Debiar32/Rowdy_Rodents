using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [Header("Health Bar")]
    public Image hpbar;
    public float Max_Health = 100f;
    private float Current_Health;

    public GameObject DamageIndicatorPrefab; // Prefab for the damage indicator (can be a sprite or simple 3D object)
    public ParticleSystem DamageParticlesPrefab; // Particle system for damage effect

    public Transform DamageIndicatorAttachPoint; // Point to attach the damage indicator (can be chosen in the inspector)
    public Transform DamageParticlesAttachPoint; // Point to attach the particle system (can be chosen in the inspector)
    private Vector3 lastSpawnPositionenem;

    [Header("Death Effect")]
    public ParticleSystem DeathEffect;

    void Start()
    {
        Current_Health = Max_Health;

        // Make sure the damage indicator is hidden at the start
        if (DamageIndicatorPrefab != null)
        {
            DamageIndicatorPrefab.SetActive(false);
        }
    }

    private void Update()
    {
        lastSpawnPositionenem = DamageParticlesAttachPoint.position;
    }

    public void Deal_Damage(float damage)
    {
        // Start showing damage indicator and apply damage
        StartCoroutine(ShowDamage());

        // Reduce health
        Current_Health -= damage;

        // Optionally apply stun or other effects
        EnemyNew_Logic enemyLogic = GetComponent<EnemyNew_Logic>();
        if (enemyLogic != null)
        {
            enemyLogic.Stun(0.2f);
        }
        SpawnEnemyBlood();
        UpdateHealthBarEnemy();

        // Check for death
        if (Current_Health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBarEnemy()
    {
        if (hpbar != null)
        {
            // Calculate the fill amount as the ratio of current health to max health
            hpbar.fillAmount = Current_Health / Max_Health;
        }
    }



    private void SpawnEnemyBlood()
    {
        Instantiate(DamageParticlesPrefab, lastSpawnPositionenem, Quaternion.identity);


        /*
        if (DamageParticlesPrefab != null && DamageParticlesAttachPoint != null)
        {
            // Instantiate the bubble effect at the spawn point and set the parent to effectSpawnPoint
            GameObject bloodenem = Instantiate(DamageParticlesPrefab.gameObject, lastSpawnPositionenem, Quaternion.identity);

            // Set the instantiated bubble's parent to the effectSpawnPoint
            bloodenem.transform.SetParent(DamageParticlesAttachPoint);

            // Optionally, reset the local position to ensure it doesn't get offset (optional, depending on your needs)
            bloodenem.transform.localPosition = Vector3.zero;
        }
        else
        {
            // If bubbleEffectPrefab is null, destroy the GameObject this script is attached to
            Destroy(gameObject);
        }
        */
    }


    // Coroutine to show the damage indicator for a short period
    private IEnumerator ShowDamage()
    {
        // Activate the damage indicator
        if (DamageIndicatorPrefab != null)
        {
            DamageIndicatorPrefab.SetActive(true);
            if (DamageIndicatorAttachPoint != null)
            {
                // Attach the damage indicator to the specified point (optional)
                DamageIndicatorPrefab.transform.position = DamageIndicatorAttachPoint.position;
            }
            else
            {
                // Default to the enemy's position if no attach point is set
                DamageIndicatorPrefab.transform.position = transform.position;
            }

            // Wait for the specified duration before hiding the damage indicator
            yield return new WaitForSeconds(0.15f); // Change the time here as needed

            // Hide the damage indicator after the delay
            DamageIndicatorPrefab.SetActive(false);
        }
    }

    private void Die()
    {
        // Handle the enemy's death here
        Instantiate(DeathEffect, transform.position, DamageIndicatorAttachPoint.rotation * Quaternion.Euler(0, 180, 0));
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject); // Destroy the enemy object
    }
}
