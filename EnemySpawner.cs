using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Hangi düşman şablonu doğacak?
    public float spawnRate = 2f;   // Kaç saniyede bir düşman gelecek?
    public float spawnDistance = 10f; // Bizden ne kadar uzakta doğacaklar? (Ekran dışı için 10 iyidir)

    private float nextSpawnTime;
    private Transform player;

    void Start()
    {
        // Oyun başlayınca "Player" etiketli karakterimizi bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Üretim zamanı geldiyse yeni düşman yarat
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate; // Bir sonraki doğuma süreyi ayarla
        }
    }

    void SpawnEnemy()
    {
        // Oyuncunun etrafında, rastgele yuvarlak bir çember üzerinde nokta seç
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 spawnPos = new Vector2(
            player.position.x + Mathf.Cos(randomAngle) * spawnDistance,
            player.position.y + Mathf.Sin(randomAngle) * spawnDistance
        );

        // Seçilen noktada düşmanı yarat
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
