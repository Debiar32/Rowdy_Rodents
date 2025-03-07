using UnityEngine;

public class Health_System : MonoBehaviour
{
    public float max_health = 200f;
    private float current_health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_health = max_health;
    }
    public void Deal_Damage(float damage)
    {
        current_health -= damage;
        Debug.Log(gameObject.name + "took damage equal to:" + damage + ". Updated hp: " + current_health);

        if(current_health <=0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        Debug.Log(gameObject.name + "was destroyed");
        Destroy(gameObject);
    }
    public void heal(float heal_amount)
    {
        current_health += heal_amount;
        if(current_health>max_health)
        {
            current_health = max_health;
        }
        Debug.Log(gameObject.name + "was healed:" + heal_amount + ". Updated hp:" + current_health);
    }
}
