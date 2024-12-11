using UnityEngine;

public class ProjectileEsp : MonoBehaviour
{
    public float speed = 20f;          // Speed of the projectile
    public float lifetime = 2f;        // Time before the projectile is destroyed

    private void Start()
    {
        // Destroy the projectile after its lifetime has elapsed
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the projectile forward in the direction it was fired
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the projectile on collision with any object
        Destroy(gameObject);
    }
}