using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public TextMeshProUGUI debug_text;
    [SerializeField] Rigidbody Player_rb;
    [SerializeField] InputAction Move;
    Vector2 Move_Vector;
    [SerializeField] public float Move_Speed;
    [SerializeField] private float Turning_Speed;

    [SerializeField] InputAction Dash;
    [SerializeField] public float Dash_Force;
    [SerializeField] float Dash_Cooldown;
    [SerializeField] InputAction Interaction;
    [SerializeField] InputAction Attack;
    public enum Player_States { 
        Idle,
        Run,
        Dash,
        Attack,
        Interaction,
        Die,
        Respawn
    }
    private Player_States Current_State = Player_States.Idle;

    [SerializeField] private Transform Att_Point;
    [SerializeField] private float Att_Range = 2.5f;
    [SerializeField] private LayerMask Enemy_Layer;

    private void OnEnable()
    {
        Move.Enable();
        Dash.Enable();
        Interaction.Enable();
        Attack.Enable();

    }

    private void OnDisable()
    {
        Move.Disable();
        Dash.Disable();
        Interaction.Disable();
        Attack.Disable();
    }
    private void Awake()
    {
        Player_rb = gameObject.GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     Move_Vector =  Move.ReadValue<Vector2>();
     Vector3 Move_Direction = new Vector3(Move_Vector.x, 0, Move_Vector.y);

        if (Attack.triggered && Current_State != Player_States.Attack)
        {
            Current_State = Player_States.Attack;
            Attacking();
        }
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
                
                if (Move_Vector != Vector2.zero) {
                    Current_State = Player_States.Run;

                }
                break;

            case Player_States.Run:
                debug_text.text = "Run";
                Player_rb.linearVelocity = new Vector3(Move_Vector.x, 0, Move_Vector.y) * Move_Speed;
                Quaternion To_Rot = Quaternion.LookRotation(new Vector3(Move_Vector.x,0,Move_Vector.y),Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,To_Rot,Turning_Speed);

                if (Move_Vector == Vector2.zero) {
                    Current_State = Player_States.Idle;
                }
                break;

            case Player_States.Dash:
                debug_text.text = "Dash";
                
                break;

            case Player_States.Attack:
                debug_text.text = "Attack";
                
                break;

            case Player_States.Interaction:
                debug_text.text = "Interact";
                break;

            case Player_States.Die:
                debug_text.text = "Die";
                break;

            case Player_States.Respawn:
                debug_text.text = "Respawn";
                break;
        }
    
    }
    private void Attacking()
    {
        Collider[] hit_enemy = Physics.OverlapSphere(Att_Point.position, Att_Range, Enemy_Layer);
        foreach (Collider enemy in hit_enemy)
        {
            Debug.Log("Hit: " + enemy.name);
            Rigidbody enemyRB = enemy.GetComponent<Rigidbody>();
            if (enemyRB != null)
            {
                Vector3 Push_Back_Direction = (enemy.transform.position - Att_Point.position).normalized;
                enemyRB.AddForce(Push_Back_Direction * 5f, ForceMode.Impulse);
            }
        }
        Invoke("Reset_Attack_State", 0.5f);
    }
    private void Reset_Attack_State()
    {
        if (Move_Vector != Vector2.zero)
        {
            Current_State = Player_States.Run;
        }
        else
        {
            Current_State = Player_States.Idle;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (Att_Point == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Att_Point.position, Att_Range);
    }
}

