using UnityEngine;

public class EnemyOutlineController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Weapon.cs bu fonksiyonu çağırarak düşmanın rengini değiştirir
    public void SetOutline(bool isSelected)
    {
        if (spriteRenderer != null)
        {
            if (isSelected)
            {
                // Hedef seçilince düşman kırmızı olsun
                spriteRenderer.color = Color.red; 
            }
            else
            {
                // Hedef bırakılınca normal rengine dönsün
                spriteRenderer.color = Color.white; 
            }
        }
    }
}