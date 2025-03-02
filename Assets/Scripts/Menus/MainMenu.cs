using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LoadingScreen"); // Transition to the Loading Screen Scene
    }

    public void ExitGame()
    {
        Application.Quit(); //Closes the Game
    }
}
