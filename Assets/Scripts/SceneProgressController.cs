using TMPro;
using UnityEngine;

public class SceneProgressController : MonoBehaviour
{

    private int activeLevel;

    private void Awake()
    {
        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        activeLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
    }
    

    public void WinGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", activeLevel + 1);
        PlayerPrefs.Save();

        var levelTracker = FindObjectOfType<LevelSystemController>();
        levelTracker.MarkLevelAsCompleted(activeLevel);
    }
}