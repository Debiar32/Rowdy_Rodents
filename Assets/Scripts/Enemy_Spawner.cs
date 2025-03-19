using System.Collections;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject Enemy_Prefab;
    public float Spawn_Time = 2f;
    public float Spawn_Radius = 10f;  // How far from the spawner enemies can appear
    public LayerMask ObstacleMask;    // Optional: to avoid spawning inside objects

    void Start()
    {
        StartCoroutine(Continuous_Spawn());
    }

    private IEnumerator Continuous_Spawn()
    {
        while (true)
        {
            Vector3 spawnPos = GetValidSpawnPosition();
            Instantiate(Enemy_Prefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Spawn_Time);
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 randomPos;
        int attempts = 0;
        do
        {
            // Random point in a circle around the spawner
            Vector2 circle = Random.insideUnitCircle * Spawn_Radius;
            randomPos = new Vector3(transform.position.x + circle.x, transform.position.y, transform.position.z + circle.y);

            attempts++;
            if (attempts > 10) break;  // Failsafe in case it can’t find a good spot
        } while (Physics.CheckSphere(randomPos, 0.5f, ObstacleMask));

        return randomPos;
    }
}
