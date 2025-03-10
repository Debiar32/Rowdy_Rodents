using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Attack_Manager : MonoBehaviour
{


    [Header("Attack")]
    [SerializeField] InputAction Attack;
    [SerializeField] InputAction Heavy_Attack;

    [SerializeField] public bool is_Attacking = false;
    [SerializeField] public bool Can_Attack = true;
    [SerializeField] private float Delay_Between_Attacks;
    [SerializeField] private float Attack_Cooldown;
    [SerializeField]  public int Attack_Order = 0; 
     
     
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
        Basic_Slash_Glave,
        Heavy_Strike_Glave,
        Dash_Strike_Glave,
        Exit_Attack

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
        if (player_movement.Current_State == Player_Movement.Player_States.Idle || player_movement.Current_State == Player_Movement.Player_States.Run) {
            Can_Attack = true;   
        }
        else Can_Attack = false;
        Handle_Attacks();
    }

    private void Handle_Attacks() {

        switch (Current_Attack_State)
        {
                case Attack_States.Idle:
                    Debug_text.text = "Idling";
                if (Can_Attack == true && Attack.IsInProgress()) {
                    Current_Attack_State = Attack_States.Basic_Slash_Glave;
                }
                else if (Can_Attack == true && Heavy_Attack.IsInProgress()) {
                    Current_Attack_State = Attack_States.Heavy_Strike_Glave;
                }
                    
                    break;

                case Attack_States.Basic_Slash_Glave:
                    Debug_text.text = "Basic_Slash";
                    if (Active_Attack != null) {
                     StopCoroutine(Active_Attack);
                     }
                    Active_Attack = StartCoroutine(Basic_Slash());
                    
                    if (Can_Attack == false && ! Attack.WasPerformedThisFrame()) {
                        Current_Attack_State = Attack_States.Idle;
                        }
                    
                    break;


        }
    }

    IEnumerator Glave_Slash()
    {
        Debug.Log("SLASH!!");
        yield return new WaitForSeconds(.2f);

    }

    private void calculate_Attack(float range, float damage) 
    {

        Detected_Enemies = Physics.OverlapSphere(Attack_ref.position,range,Enemy);

        foreach (Collider enemy in Detected_Enemies)
        {
            Enemy_Health = enemy.GetComponent<Health_System>();

            enemy.gameObject.SetActive(false);
        }
        
    
    }
    public void Knockback(Rigidbody rb, float Knockback_Power) {
        rb.AddForce(rb.gameObject.transform.forward, ForceMode.Impulse);
    
    }

    private IEnumerator Basic_Slash() {
        yield return new WaitForSeconds(.3f);
        Debug.Log("Slashed");

        
    }
}
