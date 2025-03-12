using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SaveSlotManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject confirmDeletePopup;  // Popup for confirming delete (this is a panel)
    public TextMeshProUGUI manageButtonText;  // Text on the manage button to toggle between "Manage" and "Cancel"

    public MenuNavigationHandler menuNav;
    private int selectedSlotToDelete = -1;  // To track which slot is selected for deletion
    private bool isManageModeActive = false; // Track whether manage mode is active

    private GameObject lastSelectedButton;

    public InputSystem_Actions UIControls;
    private InputAction back;

    private void Start()
    {
        confirmDeletePopup.SetActive(false);  // Hide the confirmation popup by default
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


    // Toggle Manage Mode
    public void ToggleManageMode()
    {
        isManageModeActive = !isManageModeActive;  // Toggle manage mode
        manageButtonText.text = isManageModeActive ? "Cancel" : "Manage"; // Toggle button text
        confirmDeletePopup.SetActive(false);  // Hide the confirmation popup when exiting manage mode
    }

    // Select a save slot
    public void SelectSlot(int slotNumber)
    {
        if (!isManageModeActive)  // If manage mode is not active, load the game as usual
        {
            if (File.Exists(Application.persistentDataPath + $"/save{slotNumber}.json"))
            {
                PlayerPrefs.SetInt("CurrentSave", slotNumber);
                SceneManager.LoadScene("RR_LoadingScreen");
            }
            else
            {
                CreateNewGame(slotNumber);
            }
        }
        else  // If manage mode is active, show the delete confirmation
        {
            selectedSlotToDelete = slotNumber;  // Store the slot to delete
            lastSelectedButton = EventSystem.current.currentSelectedGameObject;
            confirmDeletePopup.SetActive(true);  // Show confirmation popup
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(menuNav.confirmationButton);

        }
    }

    public void CreateNewGame(int slotNumber)
    {
        PlayerPrefs.SetInt("CurrentSave", slotNumber);
        SceneManager.LoadScene("RR_LoadingScreen");
    }

    // Delete the selected save
    public void DeleteSave()
    {
        if (selectedSlotToDelete >= 0)
        {
            string path = Application.persistentDataPath + $"/save{selectedSlotToDelete}.json";
            if (File.Exists(path))
            {
                File.Delete(path);  // Delete the file
                Debug.Log($"Save {selectedSlotToDelete} deleted.");
            }
        }

        // Hide the confirmation popup and exit manage mode
        confirmDeletePopup.SetActive(false);
        isManageModeActive = false;
        manageButtonText.text = "Manage";  // Reset the button text
        EventSystem.current.SetSelectedGameObject(lastSelectedButton);
    }

    // Cancel delete (return to manage menu)
    public void CancelDelete()
    {
        // Hide the confirmation popup and return to manage menu
        confirmDeletePopup.SetActive(false);
        if (lastSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }

    }

    public void BackToMainMenu()
    {
        isManageModeActive = false;
        manageButtonText.text = "Manage";  // Reset the button text
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);
    }

    public void GoingBack(InputAction.CallbackContext context)
    {
        // If the confirmation delete popup is active, we cancel the delete action
        if (confirmDeletePopup.activeSelf)
        {
            CancelDelete();
        }
        else
        {
            // If the popup is not active, we go back to the main menu
            isManageModeActive = false;
            manageButtonText.text = "Manage";  // Reset the button text
            gameObject.SetActive(false);  // Deactivate the SaveSlotManager
            mainMenuCanvas.SetActive(true);  // Show the main menu

            // Ensure the EventSystem updates properly by setting the selected button
            EventSystem.current.SetSelectedGameObject(null);  // Clear current selection
            EventSystem.current.SetSelectedGameObject(menuNav.mainMenuFirstButton);  // Set the main menu first button as selected
        }
    }

}
