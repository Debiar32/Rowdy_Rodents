using System.Collections;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject Enemy_Prefab;
    public int Enemy_wave_number = 5;
    public float Spawn_Time = 1.5f;
    private bool We_Spawning = false;
    public Vector3[] Spawn_Positions = new Vector3[]
    {
    new Vector3(0, 0, 10),
    new Vector3(20, 0, 20),
    new Vector3(-10, 0, 20)
    };

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate_Spawn(Transform playerTransform)
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > 4f)
            return;
        if(!We_Spawning)
        {
            StartCoroutine(Spawn_Wave());
        }
    }
    private IEnumerator Spawn_Wave()
    {
        We_Spawning = true;
        for (int i=0;i<Enemy_wave_number; i++)
        {
            Vector3 Spawn_pos = Spawn_Positions[Random.Range(0, Spawn_Positions.Length)];
            Instantiate(Enemy_Prefab, Spawn_pos, Quaternion.identity);
            yield return new WaitForSeconds(Spawn_Time);
        }
        We_Spawning = false;
    }
}
