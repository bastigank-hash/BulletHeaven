using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.5f; // Düşmanın yürüme hızı
    private Transform player;

    void Start()
    {
        // Oyun başlayınca "Player" etiketli karakterimizi bulup hedefe kilitlenir
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Eğer oyuncu sahnede varsa, ona doğru yavaşça yürü
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}