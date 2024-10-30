using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs; // Массив префабов блоков
    public Transform[] spawnPoints;    // Массив точек спауна
    public int numberOfBlocks;    // Количество блоков для спауна
    public float spawnChance = 0.5f;    // Вероятность спауна блока в точке (0.0 - 0.0, 1.0 - 100%)

    void Awake()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Проверяем, будет ли блок заспаунен в данной точке
            if (Random.value <= spawnChance)
            {
                // Выбор случайного префаба блока
                GameObject blockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];

                // Спавн блока и установка родительского объекта
                GameObject newBlock = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
                numberOfBlocks++;
            }
        }
    }
}