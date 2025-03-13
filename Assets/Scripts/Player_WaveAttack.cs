using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_WaveAttack : MonoBehaviour
{
    [Header("Wave Attack Settings")]
    [SerializeField] private float waveRange = 10f;         // How far the wave travels
    [SerializeField] private float waveAngle = 45f;         // The angle of the cone
    [SerializeField] private float waveDamage = 50f;        // Damage dealt by the wave
    [SerializeField] private float waveDuration = 0.8f;     // Duration before wave disappears
    [SerializeField] private LayerMask Enemy_Layer;
    [SerializeField] private InputAction WaveAttack;

    private int spinCount = 0;       // Tracks how many times spin attack was used
    private bool canUseWave = false; // Becomes true after 5 spin attacks

    private void OnEnable()
    {
        WaveAttack.Enable();
    }

    private void OnDisable()
    {
        WaveAttack.Disable();
    }

    private void Update()
    {
        if (WaveAttack.WasPressedThisFrame())
        {
            Perform_WaveAttack();
        }
    }

    public void RegisterSpinAttack()
    {
        spinCount++;

        if (spinCount >= 3)
        {
            canUseWave = true;
        }
    }

    private void Perform_WaveAttack()
    {
        if (!canUseWave) return;

        Debug.Log("Wave attack unleashed!");

        // Detect enemies in a cone shape
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, waveRange, Enemy_Layer);
        Vector3 playerForward = transform.forward;

        foreach (Collider enemy in hitEnemies)
        {
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(playerForward, directionToEnemy);

            if (angle < waveAngle / 2f)
            {
                Debug.Log($"Hit {enemy.name} with wave attack!");

                if (enemy.CompareTag("Enemy"))
                {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.Deal_Damage(waveDamage);
                    }
                }
            }
        }

        // Reset wave attack usage after one use
        canUseWave = false;
        spinCount = 0;

        StartCoroutine(WaveEffect());
    }

    private IEnumerator WaveEffect()
    {
        // Example of visual effect (if needed)
        yield return new WaitForSeconds(waveDuration);
        Debug.Log("Wave effect ended");
    }

    private void OnDrawGizmosSelected()
    {
        // Draw cone shape in editor
        Gizmos.color = Color.blue;
        Vector3 leftLimit = Quaternion.Euler(0, -waveAngle / 2, 0) * transform.forward * waveRange;
        Vector3 rightLimit = Quaternion.Euler(0, waveAngle / 2, 0) * transform.forward * waveRange;

        Gizmos.DrawLine(transform.position, transform.position + leftLimit);
        Gizmos.DrawLine(transform.position, transform.position + rightLimit);
    }
}
