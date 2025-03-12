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

    [Header("Shake Effect")]
    public cameraShake cameraShake;

    [Header("Breathing")]
    public GameObject breathingBubbles;
    //public ParticleSystem breathingBubbles;
    public Transform breathingSpawnPoint;
    public float minBubbleWaitTime = 4f; // Minimum time between bubble spawns
    public float maxBubbleWaitTime = 8f; // Maximum time between bubble spawns
    private float bubbleWaitTimer; // Timer for counting down



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

        // Countdown the timer
        if (bubbleWaitTimer > 0)
        {
            bubbleWaitTimer -= Time.deltaTime;
        }
        else
        {
            // When the timer reaches 0, spawn bubbles and reset the timer
            SpawnBreathingBubbles();
            SetRandomTimer(); // Set a new random time for the next spawn
        }
    }
    public void Deal_Damage(float damage)
    {
        current_health -= damage;
        SpawnBubbles();
        StartCoroutine(cameraShake.Shake(.4f, .45f));
        Debug.Log(gameObject.name + "took damage equal to:" + damage + ". Updated hp: " + current_health);

        UpdateHealthBar();

        EnemyNew_Logic enemyLogic = GetComponent<EnemyNew_Logic>();
        if (enemyLogic != null)
        {
            enemyLogic.Stun(0.2f);
        }

        if (current_health <=0)
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

    private void SetRandomTimer()
    {
        // Set a random wait time between minBubbleWaitTime and maxBubbleWaitTime
        bubbleWaitTimer = Random.Range(minBubbleWaitTime, maxBubbleWaitTime);
    }

    private void SpawnBreathingBubbles()
    {
        if (bubbleEffectPrefab != null)
        {
            // Instantiate the bubble effect at the spawn point and set the parent to effectSpawnPoint
            GameObject breathingbubble = Instantiate(breathingBubbles, lastSpawnPosition, Quaternion.identity);

            // Set the instantiated bubble's parent to the effectSpawnPoint
            breathingbubble.transform.SetParent(breathingSpawnPoint);

            // Optionally, reset the local position to ensure it doesn't get offset (optional, depending on your needs)
            breathingbubble.transform.localPosition = Vector3.zero;
        }
        else
        {
            // If bubbleEffectPrefab is null, destroy the GameObject this script is attached to
            Destroy(gameObject);
        }
    }


}
