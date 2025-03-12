using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_Attack_Manager : MonoBehaviour
{
    [Header("Attack Settings")]
    public float Attack_Damage = 20f;
    public float Attack_Duration = 0.2f;
    public Transform Attack_Point;
    public Vector3 Attack_Size = new Vector3(1.5f, 1f, 1.5f);
    public LayerMask Enemy_Layer;
    public float Attack_Cooldown = 0.6f;
    public int Nbr_Attacks = 0;
    public int Max_Attacks = 2;
    public float Attack_Reset_Time = 2f; // Additional time before resetting attack counter

    private bool Is_Attacking = false;
    private bool Is_Cooldown = false;
    private Coroutine cooldownCoroutine;

    [Header("UI Settings")]
    public Image CooldownImage; // Overlay fill image

    private GameObject attackVisual;

    private void Start()
    {
        CooldownImage.fillAmount = 0;
    }

    public void Perform_Attack()
    {
        if (!Is_Attacking && !Is_Cooldown)
        {
            StartCoroutine(Attack_Coroutine());
            Nbr_Attacks += 1;

            // Restart the reset timer whenever an attack is performed
            if (cooldownCoroutine != null)
            {
                StopCoroutine(cooldownCoroutine);
            }
            cooldownCoroutine = StartCoroutine(ResetComboCounter());
        }
    }

    private IEnumerator Attack_Coroutine()
    {
        Is_Attacking = true;
        CreateAttackVisual();

        // Detect and damage enemies
        Collider[] Hit_Enemies = Physics.OverlapBox(Attack_Point.position, Attack_Size / 2, Quaternion.identity, Enemy_Layer);
        foreach (Collider Enemy in Hit_Enemies)
        {
            if (Enemy.CompareTag("Enemy"))
            {
                EnemyHealth Enemy_Health = Enemy.GetComponent<EnemyHealth>();
                if (Enemy_Health != null)
                {
                    Enemy_Health.Deal_Damage(Attack_Damage);
                }
            }
        }

        yield return new WaitForSeconds(Attack_Duration);
        Destroy(attackVisual);
        Is_Attacking = false;
        StartCoroutine(AttackCooldown());
    }

    private void CreateAttackVisual()
    {
        if (attackVisual == null)
        {
            attackVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
            attackVisual.transform.position = Attack_Point.position;
            attackVisual.transform.localScale = Attack_Size;
            attackVisual.GetComponent<Renderer>().material.color = Color.red;
            Destroy(attackVisual.GetComponent<Collider>());
        }
    }

    private IEnumerator AttackCooldown()
    {
        Is_Cooldown = true;
        float elapsedTime = 0f;

        // If max attacks reached, do the long cooldown directly
        if (Nbr_Attacks == Max_Attacks)
        {
            float extraCooldownTime = Attack_Cooldown * 4; // Adjust cooldown as needed
            CooldownImage.fillAmount = 1f;

            // Long cooldown
            while (elapsedTime < extraCooldownTime)
            {
                elapsedTime += Time.deltaTime;
                CooldownImage.fillAmount = 1f - (elapsedTime / extraCooldownTime);
                yield return null;
            }

            CooldownImage.fillAmount = 0f;
            Is_Cooldown = false;
            Nbr_Attacks = 0; // Reset attack counter after long cooldown
        }
        else
        {
            // Normal cooldown if not max attacks yet
            CooldownImage.fillAmount = 1f;
            while (elapsedTime < Attack_Cooldown)
            {
                elapsedTime += Time.deltaTime;
                CooldownImage.fillAmount = 1f - (elapsedTime / Attack_Cooldown);
                yield return null;
            }

            CooldownImage.fillAmount = 0f;
            Is_Cooldown = false;
        }
    }


    private IEnumerator ResetComboCounter()
    {
        yield return new WaitForSeconds(Attack_Cooldown + Attack_Reset_Time); // Wait for cooldown + extra time
        Nbr_Attacks = 0;
    }

    private void OnDrawGizmos()
    {
        if (Attack_Point != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Attack_Point.position, Attack_Size);
        }
    }
}



/*
using System.Collections;
using UnityEngine;

public class Player_Attack_Manager : MonoBehaviour
{
    [Header("Attack_Settings")]
    public float Attack_Damage = 20f;
    public float Attack_Duration = 0.2f;
    public Transform Attack_Point;
    public Vector3 Attack_Size = new Vector3(1.5f, 1f, 1.5f);
    public LayerMask Enemy_Layer;
    public float Attack_Cooldown = 0.6f;
    public int Nbr_Attacks = 0;
    public int Max_Attacks = 2;

    private bool Is_Attacking = false;
    private bool Is_Cooldown = false;

    private GameObject attackVisual;

    public void Perform_Attack()
    {
        if (!Is_Attacking && !Is_Cooldown)
        {
            StartCoroutine(Attack_Coroutine());
            Nbr_Attacks += 1;
        }
    }

    private IEnumerator Attack_Coroutine()
    {
        Is_Attacking = true;

        CreateAttackVisual();

        // Detect all enemies within the attack area and damage them
        Collider[] Hit_Enemies = Physics.OverlapBox(Attack_Point.position, Attack_Size / 2, Quaternion.identity, Enemy_Layer);

        foreach (Collider Enemy in Hit_Enemies)
        {
            // Check if the enemy has the "Enemy" tag
            if (Enemy.CompareTag("Enemy"))
            {
                EnemyHealth Enemy_Health = Enemy.GetComponent<EnemyHealth>();
                if (Enemy_Health != null)
                {
                    Enemy_Health.Deal_Damage(Attack_Damage); // Deal damage to the enemy
                }
            }
        }

        // Wait for the attack duration before removing the visual
        yield return new WaitForSeconds(Attack_Duration);

        // Destroy the attack visual (the cube)
        Destroy(attackVisual);

        Is_Attacking = false;

        // Start cooldown
        StartCoroutine(AttackCooldown());
    }

    private void CreateAttackVisual()
    {
        if (attackVisual == null)
        {
            // Create the visual attack area (a red cube)
            attackVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
            attackVisual.transform.position = Attack_Point.position;
            attackVisual.transform.localScale = Attack_Size;
            attackVisual.GetComponent<Renderer>().material.color = Color.red;

            // Remove the collider on the visual (so it doesn't interfere with the physics)
            Destroy(attackVisual.GetComponent<Collider>());
        }
    }

    private IEnumerator AttackCooldown()
    {
        Is_Cooldown = true; 

        yield return new WaitForSeconds(Attack_Cooldown);

        Is_Cooldown = false;

        if (Nbr_Attacks == Max_Attacks)
        {
            Is_Cooldown = true;
            yield return new WaitForSeconds(Attack_Cooldown * 4);
            Is_Cooldown = false;
            Nbr_Attacks = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (Attack_Point != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Attack_Point.position, Attack_Size);
        }
    }

    private void Update()
    {
        // Update function left empty as no logic is needed here for now
    }
}
*/
