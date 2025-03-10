using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Max_Health = 100f;
    private float Current_Health;

    void Start()
    {
        Current_Health = Max_Health;
    }

    public void Deal_Damage(float damage)
    {
        Current_Health -= damage;
        EnemyNew_Logic enemyLogic = GetComponent<EnemyNew_Logic>();
        if (enemyLogic != null)
        {
            enemyLogic.Stun(0.2f);
        }

        if (Current_Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle the enemy's death here
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject); // Destroy the enemy object
    }

}
