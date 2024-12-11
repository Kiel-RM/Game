using System.Collections;
using UnityEngine;

public class SpriteDes : MonoBehaviour
{
    public GameObject spriteDeDestruccion; // El prefab del sprite que aparecer� al destruir el objeto
    public float tiempoDeDestruccion = 2f; // El tiempo en segundos que el sprite ser� visible

    // Llamar a este m�todo para destruir el objeto con el sprite de destrucci�n
    public void DestruirConEfecto()
    {
        if (spriteDeDestruccion != null)
        {
            // Crear el sprite en la posici�n actual del objeto
            GameObject spriteInstanciado = Instantiate(spriteDeDestruccion, transform.position, Quaternion.identity);

            // Destruir el sprite despu�s de cierto tiempo
            Destroy(spriteInstanciado, tiempoDeDestruccion);
        }

        // Destruir el objeto original
        Destroy(gameObject);
    }

    // M�todo opcional si deseas hacer la destrucci�n al colisionar con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestruirConEfecto();
    }
}
