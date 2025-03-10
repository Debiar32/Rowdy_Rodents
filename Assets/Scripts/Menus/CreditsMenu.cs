using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CreditsMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public MenuNavigationHandler menuNav;

    public InputSystem_Actions UIControls;
    private InputAction back;

    private void Start()
    {
        gameObject.SetActive(false); // Ensure it starts hidden
    }
    private void Awake()
    {
        UIControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        back = UIControls.UI.GoBack;
        back.Enable();
        back.performed += GoingBack;
    }

    private void OnDisable()
    {
        back.Disable();
    }

    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
    }

    public void GoingBack(InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
    }
}
