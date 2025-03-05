using UnityEngine;

public class Player_Attack_Manager : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    [SerializeField] private Transform Attack_ref;
    [SerializeField] private Player_Movement player_movement;

    public enum Attack_States
    {
        Idle,
        Basic_Slash_Glave,
        Heavy_Strike_Glave,
        Dash_Strike_Glave,
        Spin_Galve_Attack

    }
    public Attack_States Current_Attack_State = Attack_States.Idle;
>>>>>>> Stashed changes
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
