using UnityEngine;

public class EnemyShooterJack : MonoBehaviour
{
    public GameObject projectilePrefab;    // Prefab del proyectil que ser� disparado
    public Transform firePoint;            // Punto desde el cual se disparar� el proyectil
    public float minFireInterval = 1f;     // Intervalo m�nimo de disparo
    public float maxFireInterval = 3f;     // Intervalo m�ximo de disparo

    private void Start()
    {
        // Iniciar el disparo repetitivo
        Invoke("FireProjectile", GetRandomFireInterval());
    }

    // M�todo para disparar el proyectil
    public void FireProjectile()
    {
        if (projectilePrefab && firePoint)
        {
            // Instanciar el proyectil en el punto de disparo
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }

        // Programar el siguiente disparo
        Invoke("FireProjectile", GetRandomFireInterval());
    }

    // M�todo para obtener un intervalo de disparo aleatorio
    private float GetRandomFireInterval()
    {
        return Random.Range(minFireInterval, maxFireInterval);
    }
}