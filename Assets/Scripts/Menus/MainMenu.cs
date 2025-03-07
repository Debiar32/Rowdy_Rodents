using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject saveSlotCanvas, optionsCanvas, creditsCanvas;
    public GameObject titleScreenCanvas;

    private void Start()
    {
        // Ensure only Main Menu is active, others are hidden
        gameObject.SetActive(false);
        saveSlotCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void PlayGame()
    {
        gameObject.SetActive(false);
        saveSlotCanvas.SetActive(true);
    }

    public void OpenOptions()
    {
        gameObject.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void OpenCredits()
    {
        gameObject.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void BackToTitleScreen()
    {
        gameObject.SetActive(false);
        titleScreenCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
