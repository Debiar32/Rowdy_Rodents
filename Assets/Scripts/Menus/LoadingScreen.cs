using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(2); // Simulate loading time, Number inside () simulates the seconds
        SceneManager.LoadScene("TestLevel"); // Load the actual game
    }
}
