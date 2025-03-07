using System.Collections;
using TMPro;
using UnityEngine;

public class Player_Attack_Manager : MonoBehaviour
{
    [SerializeField] private Transform Attack_ref;
    [SerializeField] private Player_Movement player_movement;
    [SerializeField] private int Attack_Order = 0;
    [SerializeField] private TrailRenderer Slash_Effect;
    [SerializeField] private Rigidbody Player_Rb;
    [SerializeField] private TextMeshProUGUI Debug_text;

     
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

        Slash_Effect.gameObject.SetActive(false);

    }
    private void Awake()
    {
        player_movement = GetComponent<Player_Movement>();
        Player_Rb = GetComponent<Rigidbody>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Handle_Attacks() {

        switch (Current_Attack_State)
        {
                case Attack_States.Idle:
                    Debug_text.text = "Idling";
                    break;

                case Attack_States.Basic_Slash_Glave:
                    Debug_text.text = "Basic_Slash";
                    break;


        }
    }

    IEnumerator Glave_Slash()
    {
        Debug.Log("SLASH!!");
        yield return new WaitForSeconds(.2f);

    }

    private void Slash(float range) 
    { 
        
    
    }
}
