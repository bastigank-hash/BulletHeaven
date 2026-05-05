using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        // Joystick yerine bilgisayar klavyesi (WASD veya Ok tuşları)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() 
    { 
        // Çapraz giderken hızlanmaması için .normalized ekledik
        rb.linearVelocity = moveInput.normalized * moveSpeed; 
    }
}