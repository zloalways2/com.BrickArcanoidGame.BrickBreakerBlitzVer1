using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystemController : MonoBehaviour
{
    public static LevelSystemController Instance; // Статическая ссылка на экземпляр класса
        
    [SerializeField] private Button[] levelButtons; // Кнопки уровней
    private int totalLevels; // Общее количество уровней
    private int levelButtonCurrent = 0;
    void Start()
    {
        levelButtonCurrent = 0;
        totalLevels = levelButtons.Length;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitializeLevelData();
        RefreshLevelButtons();
    }

    private void InitializeLevelData()
    {
        for (int i = 0; i < totalLevels; i++)
        {
            if (!PlayerPrefs.HasKey("Level" + i))
            {
                PlayerPrefs.SetInt("Level" + i, i == 0 ? 1 : 0);
            }
        }
        PlayerPrefs.Save();
    }

    public void MarkLevelAsCompleted(int levelIndex)
    {
        PlayerPrefs.SetInt("Level" + levelIndex, 1);
        PlayerPrefs.Save();
    }

    private void RefreshLevelButtons()
    {
        for (int i = 0; i < totalLevels; i++)
        {
            if (i == 0 || PlayerPrefs.GetInt("Level" + (i), 0) == 1)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].image.color = Color.white;
                levelButtonCurrent += 1;
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].image.color = new Color(1, 1, 1, 0.78f);
            }
        }
    }

    public void LoadLevelPresentLast()
    {
        PlayerPrefs.SetInt("CurrentLevel", levelButtonCurrent);
        PlayerPrefs.Save();
        SceneManager.LoadScene(ScenePathsAudioParameters.GameplayScene);
    }

    public void LoadSelectedLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(ScenePathsAudioParameters.GameplayScene);
    }
}