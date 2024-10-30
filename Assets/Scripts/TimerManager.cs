using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; } // Singleton экземпляр

    public float LevelTime { get; private set; } 
    private float _currentTime; // Текущее время
    private bool _isTimerActive; // Активен ли таймер

    public bool IsTimerActive => _isTimerActive; // Свойство для доступа к состоянию таймера

    private void Awake()
    {
        // Если экземпляр уже существует, то удаляем текущий объект
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject); // Не уничтожать объект при загрузке новой сцены
    }

    private void Start()
    {
        LevelTime = 80f; // Установите ваше начальное время уровня здесь
        _currentTime = LevelTime;
        _isTimerActive = false;
    }

    public void StartTimer()
    {
        _isTimerActive = true;
    }

    public void StopTimer()
    {
        _isTimerActive = false;
    }

    public void ResetTimer()
    {
        _currentTime = LevelTime;
    }

    public void SetLevelTime(float newLevelTime)
    {
        LevelTime = newLevelTime;
        ResetTimer(); // Сброс таймера с новым временем
    }

    public void SetTimeMultiplier(int multiplier)
    {
        if (multiplier < 2 || multiplier > 5) // Исправлено на <= и >=
        {
            return;
        }

        LevelTime = 80f * multiplier; // Устанавливаем новое время уровня с учетом множителя
        ResetTimer(); // Сброс таймера с новым временем
    }
}