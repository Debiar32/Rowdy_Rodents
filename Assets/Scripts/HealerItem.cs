using UnityEngine;

public class HealerItem : MonoBehaviour
{
    [Header("Blah Blah")]
    public int healingamount = 20;

    private void OnTriggerEnter(Collider other)
    {
        Health_System healsplayer = other.GetComponent<Health_System>();

        if (healsplayer != null)
        {
            healsplayer.HealPlayer(healingamount);
            gameObject.SetActive(false);
        }
    }



    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
