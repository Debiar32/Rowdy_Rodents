using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Attack_Manager : MonoBehaviour
{


    [Header("Inputs")]
    [SerializeField] InputAction Attack;
    [SerializeField] InputAction Heavy_Attack;

    [Header("Variables")]
    [SerializeField] public bool is_Attacking = false;
    [SerializeField] public bool Can_Attack = true;
    [SerializeField] private float Delay_Between_Attacks;
    [SerializeField] private float Attack_Cooldown;
    [SerializeField] public int Attack_Order = 0;
    [SerializeField] public int Max_Attack_Count = 3;

    [Header("References")]
    [SerializeField] private Transform Attack_ref;
    [SerializeField] private Player_Movement player_movement;

    [SerializeField] private TrailRenderer Slash_Effect;
    [SerializeField] private Rigidbody Player_Rb;
    [SerializeField] private TextMeshProUGUI Debug_text;
    [SerializeField] private Health_System Enemy_Health;
    [SerializeField] private Collider[] Detected_Enemies;
    [SerializeField] private LayerMask Enemy;

    private Coroutine Active_Attack = null;




    public enum Attack_States
    {
        Idle,
        Attacking,
        Heavy_Attack,
        Attack_Combiner





    }
    public Attack_States Current_Attack_State = Attack_States.Idle;

    private void OnEnable()
    {
        Attack.Enable();
        Heavy_Attack.Enable();
    }
    private void OnDisable()
    {
        Attack.Disable();
        Heavy_Attack.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }
    private void Awake()
    {
        player_movement = GetComponent<Player_Movement>();
        Player_Rb = GetComponent<Rigidbody>();
        Slash_Effect.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        Attack_Order = Mathf.Clamp(Attack_Order, 0, Max_Attack_Count);
        if (Attack_Order <= 0)
        {
            Can_Attack = false;
        }
        else if (Attack_Order > 0)
        {
            Can_Attack = true;
        }

        Handle_Attacks();
    }

    private void Handle_Attacks()
    {

        switch (Current_Attack_State)
        {
            case Attack_States.Idle:
                if (Attack_Order < Max_Attack_Count)
                {
                    Attack_Order = Max_Attack_Count;
                }

                if (Attack.IsInProgress())
                {
                    Current_Attack_State = Attack_States.Attacking;
                }
                break;

            case Attack_States.Attacking:
                Attack_Order -= 1;
                if (Attack_Order > 0)
                {
                    Debug_text.text = "Attack";
                    Glave_Slash();
                   
                    Current_Attack_State = Attack_States.Idle; 

                }
                break;

            case Attack_States.Attack_Combiner:
                StartCoroutine(Attack_Combine_Delay());
                Debug_text.text = "Combine";



                break;



        }
    }

    IEnumerator Glave_Slash()
    {
        Debug.Log("SLASH!!");
        yield return new WaitForSeconds(.5f);

    }

    private void calculate_Attack(float range, float damage)
    {

        Detected_Enemies = Physics.OverlapSphere(Attack_ref.position, range, Enemy);

        foreach (Collider enemy in Detected_Enemies)
        {
            Enemy_Health = enemy.GetComponent<Health_System>();

            enemy.gameObject.SetActive(false);
        }


    }
    public void Knockback(Rigidbody rb, float Knockback_Power)
    {
        rb.AddForce(rb.gameObject.transform.forward, ForceMode.Impulse);

    }

    private IEnumerator Basic_Slash()
    {
        yield return new WaitForSeconds(.3f);
        Debug.Log("Slashed");


    }

    private IEnumerator Attack_Combine_Delay() { 
        yield return new WaitForEndOfFrame();
    }
}
