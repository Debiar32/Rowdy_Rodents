using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject controlsTab, graphicsTab, audioTab;
    public GameObject mainMenuCanvas;

    private void Start()
    {
        controlsTab.SetActive(true); // Default to showing Controls Tab
        graphicsTab.SetActive(false);
        audioTab.SetActive(false);
    }

    public void OpenControls()
    {
        controlsTab.SetActive(true);
        graphicsTab.SetActive(false);
        audioTab.SetActive(false);
    }

    public void OpenGraphics()
    {
        controlsTab.SetActive(false);
        graphicsTab.SetActive(true);
        audioTab.SetActive(false);
    }

    public void OpenAudio()
    {
        controlsTab.SetActive(false);
        graphicsTab.SetActive(false);
        audioTab.SetActive(true);
    }

    public void BackToMainMenu()
    {
        controlsTab.SetActive(true); // Default to showing Controls Tab
        graphicsTab.SetActive(false);
        audioTab.SetActive(false);
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
