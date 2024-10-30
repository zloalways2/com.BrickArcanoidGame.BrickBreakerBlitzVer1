using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance { get; private set; }
    public bool IsVibrationEnabled { get; private set; } = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVibrationSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Включение и выключение вибрации
    public void ToggleVibration()
    {
        IsVibrationEnabled = !IsVibrationEnabled;
        SaveVibrationSettings();
    }

    public void TriggerVibration()
    {
        if (IsVibrationEnabled && Application.isMobilePlatform)
        {
            Handheld.Vibrate();
        }
    }

    // Сохранение настроек вибрации
    private void SaveVibrationSettings()
    {
        PlayerPrefs.SetInt("VibrationEnabled", IsVibrationEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Загрузка настроек вибрации
    private void LoadVibrationSettings()
    {
        IsVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
    }
}