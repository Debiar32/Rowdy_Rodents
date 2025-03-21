using UnityEngine;

public class Scr_HubArtifactShowcase : MonoBehaviour
{
    public int levelIndex; // 0 = Level 1, 1 = Level 2, etc.
    public GameObject model;

    private void Start()
    {
        ModelSwitcher();
    }

    public void ModelSwitcher()
    {
        if (LevelProgressManager.instance.levelsCompleted[levelIndex])
        {
            model.SetActive(true);
        }
        else
        {
            model.SetActive(false);
        }
    }
}
