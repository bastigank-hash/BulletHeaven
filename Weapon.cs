using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Silah Ayarlari")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    public float attackRange = 5f;

    [Header("Çoklu Atiş")]
    public int projectilesPerShot = 1; 
    public float spreadAngle = 0f;     

    private float nextFireTime;
    private Transform lockedTarget = null; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) SelectTargetWithTouch();

        if (Time.time >= nextFireTime)
        {
            Transform targetToShoot = GetBestTarget();
            if (targetToShoot != null)
            {
                Shoot(targetToShoot);
                nextFireTime = Time.time + (1f / fireRate);
            }
        }
    }

    void SelectTargetWithTouch()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            if (lockedTarget != null)
            {
                EnemyOutlineController oldOutline = lockedTarget.GetComponent<EnemyOutlineController>();
                if (oldOutline != null) oldOutline.SetOutline(false);
            }
            lockedTarget = hit.collider.transform;
            EnemyOutlineController newOutline = lockedTarget.GetComponent<EnemyOutlineController>();
            if (newOutline != null) newOutline.SetOutline(true);
        }
    }

    Transform GetBestTarget()
    {
        if (lockedTarget != null)
        {
            float distanceToLocked = Vector2.Distance(transform.position, lockedTarget.position);
            if (!lockedTarget.gameObject.activeInHierarchy || distanceToLocked > attackRange)
            {
                EnemyOutlineController oldOutline = lockedTarget.GetComponent<EnemyOutlineController>();
                if (oldOutline != null) oldOutline.SetOutline(false);
                lockedTarget = null; 
            }
            else return lockedTarget; 
        }
        return FindClosestEnemy();
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, currentPos);
            if (dist < minDistance && dist <= attackRange)
            {
                closest = enemy.transform;
                minDistance = dist;
            }
        }
        return closest;
    }

    void Shoot(Transform target)
    {
        if (firePoint == null || bulletPrefab == null) return;

        Vector2 direction = (target.position - firePoint.position).normalized;
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (projectilesPerShot <= 1)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, baseAngle));
        }
        else
        {
            float startAngle = baseAngle - (spreadAngle / 2f);
            float angleStep = spreadAngle / (projectilesPerShot - 1);
            for (int i = 0; i < projectilesPerShot; i++)
            {
                float currentAngle = startAngle + (angleStep * i);
                Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, currentAngle));
            }
        }
    }
}

