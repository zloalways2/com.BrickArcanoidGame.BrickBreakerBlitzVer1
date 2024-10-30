using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float shootSpeed = 10f;
    public Image arrow;
    public float respawnTime = 6f;

    private Rigidbody2D rb;
    private bool isDragging = true;
    private Vector2 shootDirection;
    private float shootTimer;
    private Transform spawnPoint;
    private PaddleController paddleController;

    // Новый флаг для проверки состояния шарика
    public bool IsBallInPlay { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameController gameController = FindObjectOfType<GameController>();

        if (gameController != null)
        {
            spawnPoint = gameController.shootPoint;
        }
        else
        {
            Debug.LogError("GameController не найден на сцене!");
        }

        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        arrow.gameObject.SetActive(true);

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        paddleController = FindObjectOfType<PaddleController>();
    }

    void Update()
    {
        if (isDragging)
        {
            AimBall();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseBall();
        }

        if (!isDragging && IsOutOfScreen())
        {
            RespawnBall();
        }

        if (!isDragging)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= respawnTime)
            {
                RespawnBall();
            }
        }
    }

    bool IsOutOfScreen()
    {
        Camera camera = Camera.main;
        Vector3 screenPos = camera.WorldToViewportPoint(transform.position);
        return screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;
    }

    void AimBall()
    {
        arrow.enabled = true;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootDirection = (mousePos - (Vector2)transform.position).normalized;
        UpdateArrowDirection(shootDirection);
    }

    void ReleaseBall()
    {
        isDragging = false;
        rb.isKinematic = false;
        rb.velocity = shootDirection * shootSpeed;
        arrow.gameObject.SetActive(false);
        shootTimer = 0f;

        IsBallInPlay = true; // Устанавливаем флаг, что шарик запущен
        if (paddleController != null)
        {
            paddleController.EnableMovement(true); // Включаем движение платформы
        }
    }

    public void RespawnBall()
    {
        VibrationManager.Instance.TriggerVibration();
        arrow.gameObject.SetActive(true);
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        isDragging = true;

        // Возвращаем платформу на начальную позицию
        if (paddleController != null)
        {
            paddleController.ResetPosition();
        }

        IsBallInPlay = false; // Устанавливаем флаг, что шарик не в игре
    }

    void UpdateArrowDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        if (spawnPoint != null)
        {
            arrow.transform.position = spawnPoint.position;
        }
    }
}
