using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Test_Movement : MonoBehaviour
{   
    
    [SerializeField] public GameObject Cam_Pivot;
    [SerializeField] private InputAction Move;
    Vector2 Move_Vector;
    Vector3 Movement_Direction;
    [SerializeField] private InputAction Dash;
    [SerializeField] private InputAction Interaction;
    [SerializeField] private InputAction Attack;
    [SerializeField] private Rigidbody Player_Rb;
    [SerializeField] private float Move_Speed;
    [SerializeField] private float Dash_Force;
    [SerializeField] private float Rotation_Speed;
    [SerializeField] public Camera_Follow cam_follow;
    [SerializeField] private Enemy_Spawner spawner;
    bool Is_Moving = false;
    bool Is_Dashing = false;
    bool Is_Attacking = false;
    public bool Is_Interacted = false;



    private void OnEnable()
    {
        Move.Enable();
        Dash.Enable();
        Interaction.Enable();
        Interaction.performed += On_Interaction;
        Attack.Enable();
    }
    private void OnDisable()
    {
        Move.Disable();
        Dash.Disable();
        Interaction.Disable();
        Interaction.performed -= On_Interaction;
        Attack.Disable();
    }
    private void Awake()
    {
        Player_Rb = GetComponent<Rigidbody>();
        Cam_Pivot = GameObject.FindWithTag("Cam_Pivot");
        cam_follow = Cam_Pivot.GetComponent<Camera_Follow>();
    }
    private void On_Interaction(InputAction.CallbackContext context)
    {
        if (spawner != null)
        {
            Debug.Log("Interaction; enemies let's go");
            spawner.Activate_Spawn(transform);
        }
        else
        {
            Debug.Log("No spawner detected");
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        Move_Vector = Move.ReadValue<Vector2>();
        Move_Vector.Normalize();
        Movement_Direction = new Vector3(Move_Vector.x,0,Move_Vector.y);
        
       
    }
     void FixedUpdate()
    {
        Player_Rb.linearVelocity =  Movement_Direction*Move_Speed* Time.fixedDeltaTime;
        if (Move_Vector != Vector2.zero)
        {
            Quaternion To_Rot = Quaternion.LookRotation(Movement_Direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, To_Rot, Rotation_Speed);
            cam_follow.Offset = Vector3.zero;

        }
      
    }
}
