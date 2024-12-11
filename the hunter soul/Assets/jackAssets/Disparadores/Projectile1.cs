using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    public float speed = 20f;          // Velocidad del proyectil
    public float lifetime = 5f;        // Tiempo antes de que el proyectil sea destruido

    private void Start()
    {
        // Destruye el proyectil después de que haya pasado su tiempo de vida
        Destroy(gameObject, lifetime);
        transform.Rotate(0, 180, 0);
    }

    private void Update()
    {
        // Mueve el proyectil hacia abajo en la dirección del eje Y negativo
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destruye el proyectil al colisionar con cualquier objeto
        Destroy(gameObject);
    }
}
