using System.Collections;
using UnityEngine;

public class Player_Attack_Manager : MonoBehaviour
{
    [SerializeField] private Transform Attack_ref;
    [SerializeField] private Player_Movement player_movement;

    public enum Attack_States
    {
        Idle,
        Basic_Slash_Glave,
        Heavy_Strike_Glave,
        Dash_Strike_Glave

    }
    public Attack_States Current_Attack_State = Attack_States.Idle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_movement = GetComponent<Player_Movement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Handle_Attacks() { 
    
    }

    IEnumerator Glave_Slash()
    {
        Debug.Log("SLASH!!");
        yield return new WaitForSeconds(.2f);

    }

    private void Slash(float range) { 
        
    
    }
}
