using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // For UI elements

public class Player_HubMovement : MonoBehaviour
{
    Gamepad gp = Gamepad.current;
    Keyboard kb = Keyboard.current;

    [Header("Physics")]
    [SerializeField] Rigidbody Player_rb;

    [Header("Movement")]
    public InputSystem_Actions hubmover;
    public InputAction Interacting;

    [SerializeField] InputAction Move;
    Vector2 Move_Vector;
    [SerializeField] public float Move_Speed;
    [SerializeField] private float Turning_Speed;

    [Header("Interacting")]
    [SerializeField] InputAction Interaction;
    [SerializeField] public bool HubIsInteracting;
    [SerializeField] public bool canInteractWith;


    public enum Player_States
    {
        Idle,
        Run,
        Interaction,
        Die,
        Respawn
    }
    private Player_States Current_State = Player_States.Idle;

    private void Start()
    {

    }


    private void OnEnable()
    {
        Move.Enable();
        Interacting = hubmover.PlayerHub.Interact;
        Interacting.Enable();
        Interaction.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();
        Interacting.Disable();
        Interaction.Disable();
    }

    private void Awake()
    {
        Player_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!HubIsInteracting)
        {
            Move_Vector = Move.ReadValue<Vector2>();
            Vector3 Move_Direction = new Vector3(Move_Vector.x, 0, Move_Vector.y);
        }
    }

    private void FixedUpdate()
    {
        if (!HubIsInteracting)
        {
            Handle_State();
        }
    }

    private void Handle_State()
    {
        switch (Current_State)
        {
            case Player_States.Idle:
                Player_rb.linearVelocity = Vector3.zero;
                if (Move_Vector != Vector2.zero)
                {
                    Current_State = Player_States.Run;
                }
                break;

            case Player_States.Run:
                Movement();
                if (Move_Vector == Vector2.zero)
                {
                    Current_State = Player_States.Idle;
                }
                break;
        }
    }

    private void Movement()
    {
        Player_rb.linearVelocity = new Vector3(Move_Vector.x, 0, Move_Vector.y) * Time.fixedDeltaTime * Move_Speed;
        Quaternion To_Rot = Quaternion.LookRotation(new Vector3(Move_Vector.x, 0, Move_Vector.y), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, To_Rot, Turning_Speed);

    }




}