using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    public int TimeMultiplier { get; private set; } = 1; // Начальный множитель
    public bool IsTimerActive { get; private set; } = false; // Начальное состояние таймера

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Не уничтожать объект при загрузке новой сцены
    }

    public void SetTimeMultiplier(int multiplier)
    {
        TimeMultiplier = multiplier;
        TimerManager.Instance.SetTimeMultiplier(multiplier); // Устанавливаем множитель в TimerManager
    }

    public void ToggleTimer()
    {
        IsTimerActive = !IsTimerActive;
        if (IsTimerActive)
        {
            TimerManager.Instance.StartTimer();
        }
        else
        {
            TimerManager.Instance.StopTimer();
        }
    }

    public void LoadSettings()
    {
        TimerManager.Instance.SetTimeMultiplier(TimeMultiplier);
        if (IsTimerActive)
        {
            TimerManager.Instance.StartTimer();
        }
        else
        {
            TimerManager.Instance.StopTimer();
        }
    }
}