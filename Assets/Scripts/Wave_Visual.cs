using UnityEngine;

public class WaveVisual : MonoBehaviour
{
    public float expandSpeed = 5f;
    public float moveSpeed = 5f;
    public float maxScale = 5f;

    private void Update()
    {
        // Move forward (local forward = curve direction)
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // Expand scale
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxScale, maxScale, maxScale), Time.deltaTime * expandSpeed);
    }
}
