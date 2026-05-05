using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Kameranın kimi takip edeceği (Player)
    
    // Kameranın 2D düzlemde geride durması için gereken mesafe (Z ekseni -10 olmalı)
    public Vector3 offset = new Vector3(0f, 0f, -10f); 

    // Update yerine LateUpdate kullanıyoruz ki karakter yürüdükten SONRA kamera onu takip etsin (titremeyi önler)
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}