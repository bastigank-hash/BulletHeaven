using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 10;
    public int pendingUpgrades = 0;
    public Slider healthSlider;
    public Slider xpSlider;

    void Start() { currentHealth = maxHealth; UpdateUI(); }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        UpdateUI();
        if (currentHealth <= 0) Destroy(gameObject);
    }

    public void AddXP(int amount) {
        currentXP += amount;
        if (currentXP >= xpToNextLevel) LevelUp();
        UpdateUI();
    }

    void LevelUp() {
        level++; currentXP = 0; xpToNextLevel += 5; pendingUpgrades++;
    }

    void UpdateUI() {
        if(healthSlider) { healthSlider.maxValue = maxHealth; healthSlider.value = currentHealth; }
        if(xpSlider) { xpSlider.maxValue = xpToNextLevel; xpSlider.value = currentXP; }
    }
}