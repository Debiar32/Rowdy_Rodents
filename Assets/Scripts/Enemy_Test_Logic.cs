using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

public class Enemy_Test_Logic : MonoBehaviour
{
    
    [SerializeField] private float Move_Speed;
    [SerializeField] private float Chase_Distance;
    
    [SerializeField] private GameObject Target;
    [SerializeField] public float Attack_Damage = 20f;
    Vector3 direction;
    void Start()
    {
        
    }
    private void Awake()
    {
        Target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(Target.transform.position,Vector3.up * 3f);
        direction = transform.position - Target.transform.position;
        Vector3.Normalize(direction);
        if (Vector3.Distance(transform.position, Target.transform.position) <= Chase_Distance) { // checking distance
          transform.position =  Vector3.MoveTowards(transform.position,Target.transform.position,Move_Speed * Time.deltaTime);
        
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        Health_System target_hp = other.gameObject.GetComponent<Health_System>();
        if (target_hp != null)
        {
            target_hp.Deal_Damage(Attack_Damage);
            Debug.Log("Hit: " + other.gameObject.name + " for " + Attack_Damage);
        }
    }



}
