using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float speed = 2.5f;
    private Transform player;

    void Start()
    {
        // Oyun başlayınca "Player" etiketli karakterimizi bulur
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Karakterimize doğru sürekli yürür
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}