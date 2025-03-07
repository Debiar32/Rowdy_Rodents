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

    private bool Is_Attacking = false;
    private bool Is_Cooldown = false;

    private GameObject attackVisual;

    public void Perform_Attack()
    {
        
        if (!Is_Attacking && !Is_Cooldown)
        {
            StartCoroutine(Attack_Coroutine());
        }
    }

    private IEnumerator Attack_Coroutine()
    {
        Is_Attacking = true;

        CreateAttackVisual();

        Collider[] Hit_Enemies = Physics.OverlapBox(Attack_Point.position, Attack_Size / 2, Quaternion.identity, Enemy_Layer);

        foreach (Collider Enemy in Hit_Enemies)
        {
            Health_System Enemy_Health = Enemy.GetComponent<Health_System>();
            if (Enemy_Health != null)
            {
                Enemy_Health.Deal_Damage(Attack_Damage);
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

        yield return new WaitForSeconds(Attack_Cooldown);

        Is_Cooldown = false;
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
