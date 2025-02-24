using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] public Transform Camera_Target;
    [SerializeField] public float Smoothness;
    [SerializeField] public Vector3 Offset;
    [SerializeField] public GameObject Cam;
    void Start()
    {
        
    }
    private void Awake()
    {
        Cam = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera_Target != null) { 
            Vector3 target_Position = Camera_Target.position + Offset;
            transform.position = Vector3.Lerp(transform.position,target_Position,Smoothness * Time.deltaTime) ;
        }
    }
}
