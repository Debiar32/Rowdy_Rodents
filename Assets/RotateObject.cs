using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 10f; // The speed at which the object will rotate

    void Update()
    {
        // Print the rotation speed to the console
        Debug.Log("Rotation Speed: " + rotationSpeed);

        // Rotate the object around its Z-axis every frame
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
