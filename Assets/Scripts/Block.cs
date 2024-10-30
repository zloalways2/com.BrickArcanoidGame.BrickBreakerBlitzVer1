using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioSource audioComponentIncision;
    
    public Image blockImage; // Изображение блока
    public Sprite[] blockSprites; // Массив изображений для разных состояний

    public int health =1; // Количество жизней блока
    private int currentHealth;
    
    private GameController gameController; // Ссылка на GameController

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        currentHealth = health;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioComponentIncision.Play();
            TakeDamage(1); // Каждый удар шарика отнимает 1 единицу здоровья
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DestroyBlock();
        }
        else
        {
            UpdateBlockImage(); // Обновляем изображение блока, если необходимо
        }
    }

    void UpdateBlockImage()
    {
        if (blockSprites.Length > 0)
        {
            // Пример: меняем изображение в зависимости от количества здоровья
            int spriteIndex = Mathf.Clamp(blockSprites.Length - (health - currentHealth), 0, blockSprites.Length - 1);
            blockImage.sprite = blockSprites[spriteIndex];
        }
    }

    void DestroyBlock()
    {
        // Уведомляем GameController о том, что блок был уничтожен
        if (gameController != null)
        {
            gameController.BlockDestroyed(); // Уменьшаем количество оставшихся блоков
        }
        
        Destroy(gameObject); // Уничтожаем блок
    }
}
