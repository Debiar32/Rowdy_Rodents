using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI loadingText;
    public GameObject rotatingObject;
    private float _target;

    void Start()
    {
        // Start the scene loading coroutine with delay
        StartCoroutine(LoadGameScene());
    }

    void Update()
    {
        // Rotate the object continuously during loading
        if (rotatingObject != null)
        {
            rotatingObject.transform.Rotate(Vector3.forward * 50 * Time.deltaTime);  // Rotate object around Y-axis
        }

        progressBar.value = Mathf.MoveTowards(progressBar.value, _target, 3 * Time.deltaTime);
    }

    IEnumerator LoadGameScene()
    {
        // Delay before starting the loading
        float delayTime = 2f;  // Delay in seconds (adjust this as needed)
        yield return new WaitForSeconds(delayTime);

        // Reset progress bar to 0 when starting the loading
        progressBar.value = 0;

        // Temporary scene load as you mentioned there are no save files yet
        string sceneToLoad = "Test_n_Chill";  // Always load SampleScene for now

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;  // Prevent automatic scene activation before loading is complete

        // Update progress bar and loading text
        while (operation.progress < 0.9f)
        {
            _target = operation.progress;
            loadingText.text = "Loading... " + Mathf.FloorToInt(operation.progress * 100) + "%";
            yield return null;
        }

        // Smoothly increase progress to 100% for better UX
        for (float i = 0.9f; i <= 1; i += 0.01f)
        {
            _target = i;
            loadingText.text = "Almost there... " + Mathf.FloorToInt(i * 100) + "%";
            yield return new WaitForSeconds(0.05f);  // Wait for a short period to create the illusion of a smooth transition
            loadingText.text = "Almost there... 100%";
        }

        // Allow the scene to activate after a short delay
        yield return new WaitForSeconds(2   );
        operation.allowSceneActivation = true;  // Activate the scene
    }
}
