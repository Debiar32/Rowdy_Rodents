using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager instance;

    public bool[] levelsCompleted = new bool[4]; // Tracks Level 1-4 completion

    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If no instance, make this the instance and mark it to persist
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensures this object stays across scenes
        }
        else
        {
            // If instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
}
