using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [Header("Ayarlar")]
    public GameObject enemyPrefab;
    public Transform player;
    public int maxWaves = 10;            // Maksimum dalga sayısı
    public float spawnRadius = 12f;

    [Header("Arayüz (UI)")]
    public TextMeshProUGUI waveText;     // Üstteki Wave yazısı
    public GameObject recapPanel;        // Dalga sonu açılacak Panel
    public TextMeshProUGUI recapText;    // Dalga sonu paneli içindeki yazı

    // Arka Plan Verileri
    public int currentWave = 1;
    private float waveTimer;
    private float spawnRate = 2f;        // Düşmanların doğma sıklığı
    private float spawnTimer;
    private bool isWaveActive = false;

    // Skor Tablosu Verileri
    private int enemiesKilledThisWave = 0;
    private int xpGainedThisWave = 0;

    void Start()
    {
        PrepareWave(); 
    }

    void Update()
    {
        // Eğer dalga aktif değilse (özet ekranındaysak veya oyun bittiyse) zamanı durdur
        if (!isWaveActive) return;

        // 1. OYUN SÜRESİNİ GERİ SAY
        waveTimer -= Time.deltaTime;

        if (waveText != null)
        {
            waveText.text = "Wave: " + currentWave + " | " + Mathf.CeilToInt(waveTimer) + "s";
        }

        // Süre bittiyse dalgayı sonlandır!
        if (waveTimer <= 0)
        {
            StartCoroutine(EndWaveRoutine());
            return;
        }

        // 2. DÜŞMAN ÜRETİMİNİ KONTROL ET
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnRate; 
        }
    }

    void PrepareWave()
    {
        // İlk 2 dalga 40 saniye, sonrakiler 70 saniye!
        if (currentWave <= 2)
            waveTimer = 40f;
        else
            waveTimer = 70f;

        // Her dalga başında o dalganın skorlarını sıfırla
        enemiesKilledThisWave = 0;
        xpGainedThisWave = 0;
        spawnTimer = spawnRate;

        isWaveActive = true;
        Debug.Log("WAVE " + currentWave + " BAŞLADI!");
    }

    // Düşman ölünce bu fonksiyonu çağırıp skor ekliyor
    public void RegisterKill(int xpAmount)
    {
        if (isWaveActive)
        {
            enemiesKilledThisWave++;
            xpGainedThisWave += xpAmount;
        }
    }

    // Dalga bittiğinde çalışan özel zamanlayıcı (Özet ekranı)
   IEnumerator EndWaveRoutine()
    {
        isWaveActive = false; 

        // 1. TEMİZLİK: Sahnedeki tüm düşmanları bul ve tek hamlede yok et!
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in remainingEnemies)
        {
            Destroy(enemy);
        }

        // 2. ŞİFA: Oyuncunun canını yenile! 
        // (Eğer senin oyuncu can kodunun adı farklıysa, 'PlayerHealth' yazan yerleri kendi kodunun adıyla değiştir)
        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.HealToFull();
        }

        recapPanel.SetActive(true);
        recapText.text = $"<color=#FFD700><size=150%><b>WAVE {currentWave} TAMAMLANDI!</b></size></color>\n\n" +
                         $"<color=#FFFFFF>Öldürülen Düşman:</color> <color=#FF4500><b>{enemiesKilledThisWave}</b></color>\n" +
                         $"<color=#FFFFFF>Kazanılan XP:</color> <color=#00FF00><b>{xpGainedThisWave} XP</b></color>\n\n" +
                         $"<size=60%><i><color=#AAAAAA>Bir sonraki dalga hazırlanıyor...</color></i></size>";

        yield return new WaitForSeconds(4f);

        recapPanel.SetActive(false);

        if (currentWave >= maxWaves)
        {
            waveText.text = "HAYATTA KALDIN!";
            recapPanel.SetActive(true);
            recapText.text = "<color=#00FFFF><size=200%><b>TEBRİKLER!</b></size></color>\n\n" +
                             "<color=#FFFFFF>10 DALGAYI DA ATLATIP HAYATTA KALDIN!</color>";
        }
        else
        {
            currentWave++;
            spawnRate = Mathf.Max(0.2f, spawnRate * 0.8f); 
            PrepareWave(); 
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return; 

        Vector2 randomDirection = Random.insideUnitCircle.normalized; 
        Vector2 spawnPos = (Vector2)player.position + (randomDirection * spawnRadius);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}