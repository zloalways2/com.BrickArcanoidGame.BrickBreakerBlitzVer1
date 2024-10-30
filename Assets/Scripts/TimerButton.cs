using UnityEngine;
using UnityEngine.UI;

public class TimerButton : MonoBehaviour
{
    [SerializeField] private Button timerToggleButton; // Кнопка включения/выключения таймера
    [SerializeField] private Button[] multiplierButtons; // Кнопки для установки множителя
    [SerializeField] private Image timerToggleImage; // Изображение для кнопки таймера
    [SerializeField] private Sprite timerOnSprite; // Спрайт для включенного таймера
    [SerializeField] private Sprite timerOffSprite; // Спрайт для выключенного таймера

    private void Start()
    {
        // Устанавливаем обработчики событий
        timerToggleButton.onClick.AddListener(ToggleTimer);
        
        foreach (var button in multiplierButtons)
        {
            button.onClick.AddListener(() => SetLevelTimeMultiplier(int.Parse(button.name))); // Используйте имя кнопки как множитель
        }

        GameSettings.Instance.LoadSettings(); // Загружаем сохраненные настройки
        UpdateButtonSprites(); // Обновляем спрайты при старте
    }

    public void ToggleTimer()
    {
        GameSettings.Instance.ToggleTimer(); // Переключаем состояние таймера
        UpdateButtonSprites(); // Обновляем изображение кнопки таймера
    }

    public void SetLevelTimeMultiplier(int multiplier)
    {
        GameSettings.Instance.SetTimeMultiplier(multiplier); // Устанавливаем новое время уровня с множителем
        UpdateMultiplierButtonSprites(); // Обновляем спрайты кнопок множителей
    }

    private void UpdateButtonSprites()
    {
        // Обновляем спрайт кнопки таймера в зависимости от состояния таймера
        timerToggleImage.sprite = GameSettings.Instance.IsTimerActive ? timerOnSprite : timerOffSprite;
    }

    private void UpdateMultiplierButtonSprites()
    {
        foreach (var button in multiplierButtons)
        {
            int multiplier = int.Parse(button.name); // Получаем множитель из имени кнопки
            Image buttonImage = button.GetComponent<Image>(); // Получаем компонент Image кнопки

            if (multiplier == GameSettings.Instance.TimeMultiplier)
            {
                buttonImage.sprite = timerOnSprite; // Устанавливаем спрайт для выбранного множителя
            }
            else
            {
                buttonImage.sprite = timerOffSprite; // Устанавливаем спрайт для невыбранного множителя
            }
        }
    }
}
