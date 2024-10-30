using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject ballPrefab; 
    public Transform shootPoint; 
    public TextMeshProUGUI[] timerText; 
    public GameObject gameMessage;
    public GameObject winMessage;
    public GameObject loseMessage;
    [SerializeField] private AudioSource audioComponentDestroy;
    private float currentTime; // Текущее время
    private bool levelCompleted = false;
    private BlockSpawner blockSpawner; 
    private int totalBlocks; 
    private int destroyedBlocks = 0; 
    private Ball _ball;
    private PaddleController paddleController;
    private bool ballLaunched = false;

    void Start()
    {
        Time.timeScale = 1f;
        blockSpawner = FindObjectOfType<BlockSpawner>();
        paddleController = FindObjectOfType<PaddleController>();

        if (blockSpawner != null)
        {
            totalBlocks = blockSpawner.numberOfBlocks;
        }
        else
        {
            Debug.LogError("BlockSpawner не найден на сцене!");
        }

        SpawnBall();
        winMessage.SetActive(false);
        loseMessage.SetActive(false);
        paddleController.EnableMovement(false); // Блокируем движение платформы до запуска шарика

        // Загружаем настройки
        GameSettings.Instance.LoadSettings();
        currentTime = TimerManager.Instance.LevelTime; // Устанавливаем текущее время на уровень
    }

    void Update()
    {
        if (!levelCompleted)
        {
            // Обновляем текущее время, если таймер активен
            if (TimerManager.Instance.IsTimerActive)
            {
                currentTime -= Time.deltaTime;
            }
            
            foreach (var textTimerIndex in timerText)
            {
                textTimerIndex.text = $"Time:\n{Mathf.CeilToInt(currentTime).ToString()} sec";
            }

            if (currentTime <= 0)
            {
                GameOver();
            }

            if (destroyedBlocks >= totalBlocks)
            {
                LevelCompleted();
            }

            // Если шарик еще не запущен, проверяем, отпущен ли шарик
            if (!ballLaunched && Input.GetMouseButtonUp(0))
            {
                LaunchBall();
            }
        }
    }

    public void LaunchBall()
    {
        if (_ball != null)
        {
            _ball.RespawnBall(); // Запускаем шарик
            ballLaunched = true;  // Устанавливаем флаг, что шарик запущен
            paddleController.EnableMovement(true); // Разрешаем движение платформы
        }
    }

    void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity, shootPoint);
        _ball = ball.GetComponent<Ball>();  // Присваиваем ссылку на шарик
        ballLaunched = false;               // Сбрасываем флаг запуска
        paddleController.ResetPosition(); // Сбрасываем позицию платформы
    }

    public void BlockDestroyed()
    {
        audioComponentDestroy.Play();
        VibrationManager.Instance.TriggerVibration();
        destroyedBlocks++;
    }

    void GameOver()
    {
        VibrationManager.Instance.TriggerVibration();
        gameMessage.SetActive(false);
        levelCompleted = true;
        loseMessage.SetActive(true);
        Debug.Log("Game Over! Time's up.");
        Time.timeScale = 0;
    }

    void LevelCompleted()
    {
        VibrationManager.Instance.TriggerVibration();
        gameMessage.SetActive(false);
        levelCompleted = true;
        winMessage.SetActive(true);
        Debug.Log("Level Completed!");
        Time.timeScale = 0;
    }
}
