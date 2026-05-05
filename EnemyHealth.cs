using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 30;
    public int xpReward = 10; 

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            // 1. Oyuncuya XP'yi ver
            PlayerExperience playerXP = FindAnyObjectByType<PlayerExperience>();
            if (playerXP != null) playerXP.AddXP(xpReward);

            // 2. Wave Yöneticisine rapor ver (SKOR İÇİN YENİ EKLENDİ)
            WaveSpawner waveManager = FindAnyObjectByType<WaveSpawner>();
            if (waveManager != null) waveManager.RegisterKill(xpReward);

            Destroy(gameObject); 
        }
    }
}