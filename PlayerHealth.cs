using UnityEngine;
using UnityEngine.UI; // Ekrana UI (Can Barı) çizebilmek için bu kütüphane şart!
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Image healthBarFill; // Ekrandaki kırmızı can barımız

    void Start()
    {
        currentHealth = maxHealth; // Oyun başında canı fulle
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(20); 
            Destroy(collision.gameObject); 
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // Can barının doluluk oranını güncelle (0 ile 1 arasında bir değer hesaplar)
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Dalga bittiğinde canı tamamen dolduran komut
    public void HealToFull()
    {
        // Kendi kodunda can değişkeninin adı neyse onu kullan (Örn: health veya currentHealth)
        // Eğer maxHealth diye bir sınır belirlediysen: currentHealth = maxHealth; yap.
        // Aşağıdaki 100 sayısını kendi oyununun maksimum canına göre değiştirebilirsin.
        
        currentHealth = maxHealth;

        // Eğer can barı kullanıyorsan, barın dolması için barı güncelleyen kodunu da buraya yazmalısın.
        // Örnek: healthSlider.value = health; 
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}