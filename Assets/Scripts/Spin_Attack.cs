using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_SpinAttack : MonoBehaviour
{
    [Header("Spin Attack Settings")]
    [SerializeField] private float spinRadius = 3f;
    [SerializeField] private float spinDuration = 0.5f;
    [SerializeField] private float spinDamage = 30f;
    [SerializeField] private float spinCooldown = 2f;  // New cooldown
    [SerializeField] private LayerMask Enemy_Layer;
    [SerializeField] private InputAction Spin;

    [Header("UI Settings")]
    [SerializeField] private Image spinCooldownImage; // UI overlay
    [SerializeField] private Image spinUsableImage; // UI overlay
    [SerializeField] private Image spinBaseImage; // UI overlay
    [SerializeField] private Image WaveUsableShowcase; // UI overlay

    private bool isSpinning = false;
    private bool isSpinOnCooldown = false;
    private Player_Attack_Manager attackManager;
    private Player_WaveAttack waveAttack;

    private GameObject attackVisualSpin;
    public Transform Attack_PointSpin;
    public Vector3 Attack_SizeSpin = new Vector3(24f, 1f, 24f);

    private void Awake()
    {
        attackManager = GetComponent<Player_Attack_Manager>();
        waveAttack = GetComponent<Player_WaveAttack>();
    }

    private void OnEnable() { Spin.Enable(); }
    private void OnDisable() { Spin.Disable(); }

    private void Start()
    {
        spinCooldownImage.fillAmount = 0f; // Start as usable
        spinUsableImage.fillAmount = 0f; // Start as usable
        spinBaseImage.fillAmount = 1f; // Start as usable
        WaveUsableShowcase.fillAmount = 0f; // Start as usable
    }

    private void Update()
    {
        if (Spin.WasPressedThisFrame())
        {
            Perform_SpinAttack();
        }

        if (spinCooldownImage.fillAmount == 0f && attackManager.Nbr_Attacks == attackManager.Max_Attacks && attackManager.Is_Cooldown)
        {
            spinUsableImage.fillAmount = 1f;
            WaveUsableShowcase.fillAmount = 1f;
            spinBaseImage.fillAmount = 0f;
        }
        else
        {
            spinBaseImage.fillAmount = 1f;
            WaveUsableShowcase.fillAmount = 0f;
            spinUsableImage.fillAmount = 0f;
        }
    }

    private void CreateAttackVisualSpin()
    {
        if (attackVisualSpin == null)
        {
            attackVisualSpin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            attackVisualSpin.transform.position = Attack_PointSpin.position;
            attackVisualSpin.transform.localScale = Attack_SizeSpin;
            attackVisualSpin.GetComponent<Renderer>().material.color = Color.red;
            attackVisualSpin.transform.SetParent(transform); // Volgt de speler
            attackVisualSpin.GetComponent<Collider>().enabled = false;
        }
    }


    public void Perform_SpinAttack()
    {
        if (!isSpinning && !isSpinOnCooldown && attackManager.Is_Cooldown && attackManager.Nbr_Attacks == attackManager.Max_Attacks)
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
        CreateAttackVisualSpin();
        isSpinOnCooldown = true;

        // Start cooldown UI effect (full when attack is available)
        spinCooldownImage.fillAmount = 1f;


        // Detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, spinRadius, Enemy_Layer);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.Deal_Damage(spinDamage);
                }
            }
        }

        // Spin effect duration
        yield return new WaitForSeconds(spinDuration);

        Destroy(attackVisualSpin);
        isSpinning = false;

        // Start cooldown UI effect, now counting down
        float elapsedTime = 0f;
        while (elapsedTime < spinCooldown)
        {
            elapsedTime += Time.deltaTime;
            spinCooldownImage.fillAmount = 1f - (elapsedTime / spinCooldown); // Countdown from 1 to 0
            yield return null;
        }

        spinCooldownImage.fillAmount = 0f; // Indicate cooldown is complete (optional visual effect)
        isSpinOnCooldown = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spinRadius);
    }
}



/*
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
*/