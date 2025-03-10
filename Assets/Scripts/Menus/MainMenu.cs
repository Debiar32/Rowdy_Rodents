using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("All Canvas Assets Menu")]
    public GameObject saveSlotCanvas, optionsCanvas, creditsCanvas;
    public GameObject titleScreenCanvas;

    public MenuNavigationHandler menuNav;

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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.playFirstButton);


    }

    public void OpenOptions()
    {
        gameObject.SetActive(false);
        optionsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.optionsFirstButton);
    }

    public void OpenCredits()
    {
        gameObject.SetActive(false);
        creditsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.creditsFirstButton);
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
