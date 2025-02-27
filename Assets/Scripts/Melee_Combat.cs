using UnityEngine;
using static Player_Movement;

public class Melee_Combat : MonoBehaviour
{
    public float att_range = 2.2f;
    public Transform att_point;
    public LayerMask enemy_layer;
    public float att_cd = 0.6f;
    private float last_att_time;
    private bool is_attacking = false;
    private Player_Movement player_controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_controller = GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire2")) && Time.time >= last_att_time + att_cd)
        {
            //if (player_controller.Current_State == Player_States.Attack) { }
        }
    }
}
