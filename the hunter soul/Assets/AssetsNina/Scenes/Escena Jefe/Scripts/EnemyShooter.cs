using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject projectilePrefab;    // Projectile prefab to be fired
    public Transform firePoint;            // Point from which the projectile will be fired

    // Method to fire the projectile
    public void FireProjectile()
    {
        if (projectilePrefab && firePoint)
        {
            // Instantiate projectile at the fire point
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}