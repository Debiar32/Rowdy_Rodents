using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigationHandler : MonoBehaviour
{
    public MainMenu mainMenu;
    //[Header("First Button Navigation")]
    public GameObject mainMenuFirstButton, playFirstButton, optionsFirstButton, creditsFirstButton, deleteSaveFirstButton, confirmationButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  /*      if (mainMenu.titleScreenCanvas.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        }
        else if (mainMenu.saveSlotCanvas.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(playFirstButton);
        }
        else if (mainMenu.optionsCanvas.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        }
        else if (mainMenu.creditsCanvas.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditsFirstButton);
        }
  */
    }
}
