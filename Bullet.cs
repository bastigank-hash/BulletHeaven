using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;   // Düşmana vereceği hasar
    public float speed = 15f; // Merminin uçuş hızı

    void Start()
    {
        // Mermi uzayın sonsuzluğuna gidip oyunu kastırmasın diye, 3 saniye sonra kendi kendini yok et
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        // Mermiyi sürekli olarak ileriye doğru uçur
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. KONTROL: Eğer çarptığımız şey "Player" (Kendi karakterimiz) ise, mermi onu görmezden gelip uçmaya devam etsin!
        if (collision.CompareTag("Player"))
        {
            return; // Kodu burada kes, alt satırlara inme.
        }

        // 2. KONTROL: Eğer çarptığımız şeyin etiketi "Enemy" (Düşman) ise
        if (collision.CompareTag("Enemy"))
        {
            // Düşmanın can kodunu bul
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            
            // Eğer kod varsa, hasarı ver
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Mermi hedefini bulup hasarını verdikten sonra kendi kendini yok etsin
            Destroy(gameObject); 
        }
    }
}
