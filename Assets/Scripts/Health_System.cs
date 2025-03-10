using UnityEngine;

public class Health_System : MonoBehaviour
{
    public float max_health = 200f;
    private float current_health;
    public bool Is_Player = false;
    public bool Is_Invincible = false;
    public bool Is_Dead = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        current_health = max_health;
        
    }
    void Start()
    {
       
        if (gameObject.CompareTag("Player")) { 
            Is_Player = true;
        }
        Is_Dead = false;
    }

    public void Deal_Damage(float damage)
    {
        
        
            current_health -= damage;
            Debug.Log(gameObject.name + "took damage equal to:" + damage + ". Updated hp: " + current_health);

           
        
        if (current_health <= 0)
        {
            Dead();
        }

    }
    public void Dead()
    {
        /*Debug.Log(gameObject.name + "was destroyed");
        Destroy(gameObject);
        */

        if (Is_Player)
        {
            Is_Dead = true;
            

        }
        else gameObject.SetActive(false);
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
