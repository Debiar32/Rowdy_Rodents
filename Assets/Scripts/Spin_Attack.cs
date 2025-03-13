using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_SpinAttack : MonoBehaviour
{
    [Header("Spin Attack Settings")]
    [SerializeField] private float spinRadius = 3f;
    [SerializeField] private float spinDuration = 0.5f;
    [SerializeField] private float spinDamage = 30f;
    [SerializeField] private LayerMask Enemy_Layer;
    [SerializeField] private InputAction Spin;

    private bool isSpinning = false;
    private Player_Attack_Manager attackManager;
    private Player_WaveAttack waveAttack;
    private void Awake()
    {
        attackManager = GetComponent<Player_Attack_Manager>();
        waveAttack = GetComponent<Player_WaveAttack>();
    }

    private void OnEnable()
    {
        Spin.Enable();
    }

    private void OnDisable()
    {
        Spin.Disable();
    }

    private void Update()
    {
        if (Spin.WasPressedThisFrame())
        {
            Perform_SpinAttack();
        }
    }

    public void Perform_SpinAttack()
    {
        // Only allow spin attack when the player has reached max attacks AND the long cooldown is running
        if (!isSpinning && attackManager != null && attackManager.Is_Cooldown && attackManager.Nbr_Attacks == attackManager.Max_Attacks)
        {
            StartCoroutine(SpinAttack());   
            if (waveAttack != null)
            {
                waveAttack.RegisterSpinAttack();
            }
        }
    }

    private IEnumerator SpinAttack()
    {
        isSpinning = true;

        // Detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, spinRadius, Enemy_Layer);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log($"Hit {enemy.name} with spin attack!");

            if (enemy.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.Deal_Damage(spinDamage);
                }
            }
        }

        // Wait for spin duration before allowing another spin
        yield return new WaitForSeconds(spinDuration);
        isSpinning = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spinRadius);
    }
}
