using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;

    private void Start()
    {
        gameObject.SetActive(false); // Ensure it starts hidden
    }

    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
