using UnityEngine;

public class ProjectileJack : MonoBehaviour
{
    public float speed = 20f;          // Velocidad del proyectil
    public float lifetime = 5f;        // Tiempo antes de que el proyectil sea destruido

    private void Start()
    {
        // Destruye el proyectil despu�s de que haya pasado su tiempo de vida
        Destroy(gameObject, lifetime);
        transform.Rotate(0, 180, 0);
    }

    private void Update()
    {
        // Mueve el proyectil hacia adelante en la direcci�n del eje X
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destruye el proyectil al colisionar con cualquier objeto
        Destroy(gameObject);
    }
}
