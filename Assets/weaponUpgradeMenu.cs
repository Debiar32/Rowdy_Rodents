using UnityEngine;
using UnityEngine.InputSystem;

public class weaponUpgradeMenu : MonoBehaviour
{

    public InputSystem_Actions navigation;
    private InputAction exitButton;

    public Player_HubMovement playerHubMove;


    private void Awake()
    {
        navigation = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        exitButton = navigation.UI.GoBack;
        exitButton.Enable();
        exitButton.performed += CloseMenu;
    }

    private void OnDisable()
    {
        exitButton.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
        playerHubMove.HubIsInteracting = false;
    }
}
