using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public TMP_Text progressText;
    public RectTransform loadingIcon;
    public float rotationSpeed = 100f;
    public TMP_Text loadingMessage;
    public TMP_Text tipsText;  // New TMP_Text for tips

    private string[] messages = {
        "Loading assets...",
        "Initializing world...",
        "Fetching data...",
        "Preparing enemies...",
        "Almost there..."
    };

    private string[] tips = {
        "Did you know that WASD is used to move?",
        "Don't look at the screen for too long or your eyes might become cubes.",
        "Try pressing spacebar for extra fun!",
        "Loading takes time, just like cooking noodles.",
        "Isn't this loading screen just the best?",
        "The game will be worth the wait, trust us."
    };

    private void Start()
    {
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(0.5f); // Short delay for smoother transition

        AsyncOperation operation = SceneManager.LoadSceneAsync("TestLevel");
        operation.allowSceneActivation = false;

        float displayedProgress = 0f;
        int lastMessageIndex = -1;
        float loadingStartTime = Time.time;

        // Fade variables for tips
        float stayDuration = Random.Range(5f, 7f); // Stay visible for 5-7 seconds
        float fadeStartTime = Time.time;
        int currentTipIndex = -1;

        // Initial state for progress and tips
        progressText.text = "0%";
        tipsText.text = "Loading...";  // Placeholder for initial tip

        // Initially, hide the fill area of the slider
        progressBar.fillRect.gameObject.SetActive(false);

        while (!operation.isDone)
        {
            // Ensure loading takes at least 20 seconds
            float elapsedTime = Time.time - loadingStartTime;
            if (elapsedTime < 20f)
            {
                // Simulate loading time with glitches
                displayedProgress = Mathf.Min(displayedProgress + Time.deltaTime / 20f, 0.9f); // Progress slowly
            }
            else
            {
                float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);
                displayedProgress = Mathf.Min(displayedProgress + Time.deltaTime * 0.5f, targetProgress); // Smooth out progress after 20 sec
            }

            // Glitch effect: Add random jumps in progress
            if (elapsedTime > 10f && elapsedTime < 15f && displayedProgress < 0.5f)
            {
                yield return new WaitForSeconds(Random.Range(1f, 2f)); // Wait for a glitch interval
                displayedProgress = Random.Range(0.2f, 0.5f); // Jump progress
                loadingMessage.text = messages[Random.Range(0, messages.Length)]; // Change message on glitch
            }
            else if (elapsedTime > 15f && displayedProgress < 0.7f)
            {
                yield return new WaitForSeconds(Random.Range(1f, 2f));
                displayedProgress = Random.Range(0.5f, 0.7f); // Another progress jump
                loadingMessage.text = messages[Random.Range(0, messages.Length)];
            }

            // If the progress reaches 5%, start showing the slider fill
            if (displayedProgress >= 0.05f)
            {
                progressBar.fillRect.gameObject.SetActive(true);
            }

            // Update progress bar and text
            progressBar.value = displayedProgress;
            progressText.text = (displayedProgress * 100).ToString("F0") + "%";

            // Update loading messages dynamically
            int messageIndex = Mathf.FloorToInt(displayedProgress * messages.Length);
            if (messageIndex != lastMessageIndex && messageIndex < messages.Length)
            {
                loadingMessage.text = messages[messageIndex];
                lastMessageIndex = messageIndex;
            }

            // Update tips at regular intervals
            if (Time.time - fadeStartTime > stayDuration)
            {
                // Set new tip after staying for the required duration
                currentTipIndex = (currentTipIndex + 1) % tips.Length; // Cycle through tips
                tipsText.text = tips[currentTipIndex];
                fadeStartTime = Time.time; // Reset timer for next tip
            }

            // Update loading messages when progress reaches 90%
            if (displayedProgress >= 0.9f)
            {
                loadingMessage.text = "Spawning in...";
                yield return new WaitForSeconds(2f); // Delay before switching scenes
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void Update()
    {
        if (loadingIcon != null)
        {
            loadingIcon.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
