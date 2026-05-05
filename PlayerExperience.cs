using UnityEngine;
using TMPro; // DİKKAT: Eski 'UnityEngine.UI' silindi, yerine yeni ve daha havalı olan TextMeshPro kütüphanesi geldi!

public class PlayerExperience : MonoBehaviour
{
    public int currentLevel = 1;      
    public int currentXP = 0;         
    public int xpToNextLevel = 100;   
    
    // DİKKAT: Burada 'Text' yerine 'TextMeshProUGUI' kullanıyoruz
    public TextMeshProUGUI levelText;            

    void Start()
    {
        UpdateLevelUI(); 
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;                  
        currentXP -= xpToNextLevel;      
        xpToNextLevel += 50;             
        
        UpdateLevelUI();
    }

    void UpdateLevelUI()
    {
        if (levelText != null)
        {
            // Yazıyı da senin istediğin gibi "XP Level: " yaptık
            levelText.text = "XP Level: " + currentLevel;
        }
    }
}

