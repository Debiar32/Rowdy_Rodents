
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    
    Gamepad gp = Gamepad.current;
    Keyboard kb = Keyboard.current;
    public TextMeshProUGUI debug_text;
    [Header("Pyhsics")]
    [SerializeField] Rigidbody Player_rb;

    [Header("Movement")]
    [SerializeField] InputAction Move;
    Vector2 Move_Vector;
    [SerializeField] public float Move_Speed;
    [SerializeField] private float Turning_Speed;
    [SerializeField] private bool Can_Move;

    [Header("Dashing")]
    [SerializeField] InputAction Dash;
    [SerializeField] public float Dash_Force;
    [SerializeField] float Dash_Cooldown;
    [SerializeField] public bool Is_Dashing = false;

    [Header("Interacting")]
    [SerializeField] InputAction Interaction;
    [SerializeField] private bool Can_Interact;

    Player_Attack_Manager Attack_Manager;

    private Health_System health;

    /*
    [Header("Attack")]
    [SerializeField]  InputAction Attack;
    
    [SerializeField] public bool is_Attacking = false;
    [SerializeField] public bool Can_Attack = true;
    [SerializeField] private float Delay_Between_Attacks;
    [SerializeField] private float Attack_Cooldown;
    public int Attack_Order = 0; 
    */


    public enum Player_States { 
        Idle,
        Run,
        Dash,
        Attack,
        Attack_Chain_Window,
        Interaction,
        Die,
        Respawn
    }
    public Player_States Current_State = Player_States.Idle;
    private void OnEnable()
    {
        Move.Enable();
        Dash.Enable();
        Interaction.Enable();
        
        
    }

    private void OnDisable()
    {
        Move.Disable();
        Dash.Disable();
        Interaction.Disable();
        
    }
    private void Awake()
    {
        Player_rb = GetComponent<Rigidbody>();
        Attack_Manager = GetComponent<Player_Attack_Manager>();
        health = GetComponent<Health_System>();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     Move_Vector =  Move.ReadValue<Vector2>();
     Vector3 Move_Direction = new Vector3(Move_Vector.x, 0, Move_Vector.y);
     
     
    }
    private void FixedUpdate()
    {
        Handle_State();
        
        
    }

    private void Handle_State() {
        switch (Current_State)
        {
            case Player_States.Idle:
                debug_text.text = "Idle";
                Player_rb.linearVelocity = Vector3.zero;
                //Idle Animations and etc. here


                Is_Dashing = false;

                if (Move_Vector != Vector2.zero) {
                    Current_State = Player_States.Run;

                }
                else if (health.Is_Dead)
                {
                    Current_State = Player_States.Die;
                }
                break;

            case Player_States.Run:

                debug_text.text = "Run";
                Movement();


                if (Move_Vector == Vector2.zero) {
                    Current_State = Player_States.Idle;
                }

                else if (Dash.IsPressed() && Is_Dashing == false) {
                    Current_State = Player_States.Dash;
                }
                else if (health.Is_Dead == true)
                {
                    Current_State = Player_States.Die;
                }




                break;

            case Player_States.Dash:
                debug_text.text = "Dash";
                StopAllCoroutines();
                StartCoroutine(Dashing());
                Current_State = Player_States.Run;
                if (health.Is_Dead == true)
                {
                    Current_State = Player_States.Die;
                }
                
                
                break;

            case Player_States.Attack:
                gp.SetMotorSpeeds(10, 10);
                debug_text.text = "Attack";
                if (health.Is_Dead == true) 
                {
                    Current_State = Player_States.Die;
                
                }

                
                break;

            case Player_States.Interaction:
                debug_text.text = "Interact";
                if (health.Is_Dead == true)
                {
                    Current_State = Player_States.Die;
                }
                break;

            case Player_States.Die:
                debug_text.text = "Die";
                break;

            case Player_States.Respawn:
                debug_text.text = "Respawn";
                health.Is_Dead = false;
                Current_State = Player_States.Idle;
                break;
        }
    
    }

    private void Movement()
    {
        Player_rb.linearVelocity = new Vector3(Move_Vector.x, 0, Move_Vector.y) * Time.fixedDeltaTime * Move_Speed; 
        Quaternion To_Rot = Quaternion.LookRotation(new Vector3(Move_Vector.x, 0, Move_Vector.y), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, To_Rot, Turning_Speed);
    }

    private IEnumerator Dashing() {
        
        Is_Dashing = true;
        
        Player_rb.AddForce(transform.forward * Dash_Force  ,ForceMode.Impulse);
        Current_State = Player_States.Idle;
        yield return new WaitForSeconds(Dash_Cooldown);
        Is_Dashing = false;
        
        
        
        
    }
}