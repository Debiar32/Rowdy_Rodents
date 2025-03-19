using UnityEngine;
using TMPro; // If using TextMeshPro

public class KillCounterUI : MonoBehaviour
{
    public TextMeshProUGUI killText; // Reference to UI text

    void Update()
    {
        if (killText != null)
        {
            killText.text = "Kills: " + Player_Movement.killCount;
        }
    }
}
