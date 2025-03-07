using UnityEngine;
using UnityEngine.UI;

public class Health_System : MonoBehaviour
{
    [Header("Health Bar")]
    public Slider slider;

    [Header("Bubble Effect")]
    public GameObject bubbleEffectPrefab;
    public Transform effectSpawnPoint;
    private Vector3 lastSpawnPosition;

    [Header("Misc")]
    public float max_health = 200f;
    private float current_health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_health = max_health;
        UpdateHealthBar();
    }

    private void Update()
    {
        // Update the last known position of the spawn point.
        lastSpawnPosition = effectSpawnPoint.position;
    }
    public void Deal_Damage(float damage)
    {
        current_health -= damage;
        SpawnBubbles();
        Debug.Log(gameObject.name + "took damage equal to:" + damage + ". Updated hp: " + current_health);

        UpdateHealthBar();

        if(current_health <=0)
        {
            SpawnBubbles();
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

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (slider != null)
        {
            slider.maxValue = max_health;
            slider.value = current_health;
        }
    }

private void SpawnBubbles()
{
    if (bubbleEffectPrefab != null && effectSpawnPoint != null)
    {
        // Instantiate the bubble effect at the spawn point and set the parent to effectSpawnPoint
        GameObject bubble = Instantiate(bubbleEffectPrefab, lastSpawnPosition, Quaternion.identity);
        
        // Set the instantiated bubble's parent to the effectSpawnPoint
        bubble.transform.SetParent(effectSpawnPoint);

        // Optionally, reset the local position to ensure it doesn't get offset (optional, depending on your needs)
        bubble.transform.localPosition = Vector3.zero;
    }
    else
    {
        // If bubbleEffectPrefab is null, destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}


}
