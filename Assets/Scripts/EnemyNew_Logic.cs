using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNew_Logic : MonoBehaviour
{
    [SerializeField] private float Move_Speed_Walk; // Speed when walking toward player
    [SerializeField] private float Move_Speed_Run;  // Speed when chasing the player
    [SerializeField] private float General_Detection_Range; // Range to detect the player
    [SerializeField] private float Walk_Detection_Range; // Range to walk toward player (slower speed)
    [SerializeField] private float Attack_Detection_Range; // Range to trigger the attack

    [SerializeField] private GameObject Target; // Player target
    private NavMeshAgent agent;
    public float Enemy_Attack_Duration = 1f;

    // Attack-related variables
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool isStunned = false;
    public GameObject attackIndicator; // Small sphere to show attack area
    public GameObject attackRangeCollider; // Collider that detects player in attack range

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        attackIndicator.SetActive(false); // Initially hide the attack indicator
        attackRangeCollider.SetActive(false); // Initially hide the attack range collider
    }

    private void Awake()
    {
        Target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Get distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, Target.transform.position);
        if (isStunned) return; // If stunned, skip movement logic


        // **General Detection Range**: Detect player and try to move toward them
        if (distanceToPlayer <= General_Detection_Range)
        {
            agent.destination = Target.transform.position;

            // **Walk Detection Range**: Move slower when close to player (walking speed)
            if (distanceToPlayer <= Walk_Detection_Range)
            {
                agent.speed = Move_Speed_Walk; // Slow down the enemy
            }
            else
            {
                agent.speed = Move_Speed_Run; // Speed up the enemy
            }

            // **Attack Range**: If the player is in range, initiate the attack sequence
            if (distanceToPlayer <= Attack_Detection_Range && canAttack)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            // Stop moving if player is outside of detection range
            agent.destination = transform.position;
        }
    }

    private IEnumerator Attack()
    {
        if (isAttacking || !canAttack)
            yield break; // Exit if already attacking or on cooldown

        isAttacking = true;

        // Show attack indicator (small sphere) for 1 second
        attackIndicator.SetActive(true);
        attackIndicator.transform.position = transform.position + transform.forward * 1.5f + Vector3.up * 2f;
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the attack range

        // Hide the attack indicator after 1 second
        attackIndicator.SetActive(false);

        // Show the attack range collider
        attackRangeCollider.SetActive(true);

        // Attack logic: Check if player is inside the attack range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * 1.5f, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.Log("Hit!"); // Player is inside the attack range
            }
        }

        // Wait for 2 seconds before hiding the attack range collider
        yield return new WaitForSeconds(Enemy_Attack_Duration);

        // Hide the attack range collider
        attackRangeCollider.SetActive(false);

        // Cooldown before the enemy can attack again
        canAttack = false;
        yield return new WaitForSeconds(2f); // 2 seconds cooldown
        canAttack = true; // The enemy can attack again after cooldown

        isAttacking = false; // Attack has finished, ready for the next one
    }
    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        agent.isStopped = true; // Stop movement
        yield return new WaitForSeconds(duration);
        isStunned = false;
        agent.isStopped = false; // Resume movement
    }
}

