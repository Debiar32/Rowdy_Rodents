using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class SaveSlotManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject confirmDeletePopup;  // Popup for confirming delete (this is a panel)
    public TextMeshProUGUI manageButtonText;  // Text on the manage button to toggle between "Manage" and "Cancel"

    private int selectedSlotToDelete = -1;  // To track which slot is selected for deletion
    private bool isManageModeActive = false; // Track whether manage mode is active

    private void Start()
    {
        confirmDeletePopup.SetActive(false);  // Hide the confirmation popup by default
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
            confirmDeletePopup.SetActive(true);  // Show confirmation popup
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
    }

    // Cancel delete (return to manage menu)
    public void CancelDelete()
    {
        // Hide the confirmation popup and return to manage menu
        confirmDeletePopup.SetActive(false);
    }

    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
