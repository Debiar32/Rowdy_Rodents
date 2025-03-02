using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Movement speed
    public float rotationSpeed = 10f;  // Rotation speed (how fast the player turns)
    public Camera playerCamera;  // The player's camera for movement direction

    private Rigidbody rb;
    private Vector2 moveInput;

    private InputAction attack;
    private InputAction moveAction;

    private Vector3 velocity;  // To store the current velocity

    private void Awake()
    {
        // Get the Rigidbody component for physics-based movement
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;  // Ensure physics are applied to the Rigidbody (for gravity and collisions)

        // Setup input actions for movement and attack
        attack = InputSystem.actions.FindAction("Attack");
        attack.performed += Attack_performed;

        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += Move_performed;
        moveAction.canceled += Move_canceled;  // Handle input cancelation when no keys are pressed
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        moveInput = Vector2.zero;  // Reset movement when keys are released
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Attacked");
    }

    private void FixedUpdate()
    {
        // Apply gravity naturally by relying on Unity's physics system

        // Calculate the desired movement direction
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        movement = playerCamera.transform.TransformDirection(movement);  // Convert to world space
        movement.y = 0f;  // Don't change the Y component (we'll leave gravity to handle it)

        // Smooth the movement using velocity
        if (movement.magnitude > 0.1f)
        {
            velocity = Vector3.Lerp(velocity, movement * moveSpeed, Time.deltaTime * 10f);  // Smooth transition
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * 5f);  // Smooth deceleration
        }

        // Apply the movement velocity
        rb.MovePosition(rb.position + velocity * Time.deltaTime);

        // Handle rotation: Rotate smoothly towards the movement direction
        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);  // Calculate the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  // Smoothly rotate
        }
    }
}
