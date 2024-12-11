using System.Collections;
using UnityEngine;

public class SpriteDes : MonoBehaviour
{
    public GameObject spriteDeDestruccion; // El prefab del sprite que aparecerá al destruir el objeto
    public float tiempoDeDestruccion = 2f; // El tiempo en segundos que el sprite será visible

    // Llamar a este método para destruir el objeto con el sprite de destrucción
    public void DestruirConEfecto()
    {
        if (spriteDeDestruccion != null)
        {
            // Crear el sprite en la posición actual del objeto
            GameObject spriteInstanciado = Instantiate(spriteDeDestruccion, transform.position, Quaternion.identity);

            // Destruir el sprite después de cierto tiempo
            Destroy(spriteInstanciado, tiempoDeDestruccion);
        }

        // Destruir el objeto original
        Destroy(gameObject);
    }

    // Método opcional si deseas hacer la destrucción al colisionar con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestruirConEfecto();
    }
}
