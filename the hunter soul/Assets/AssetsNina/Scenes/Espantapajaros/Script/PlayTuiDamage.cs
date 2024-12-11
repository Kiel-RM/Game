using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTuiDamage : MonoBehaviour
{
    private int vidas = 3;  // El jugador comienza con 3 vidas (puedes ajustar este valor)

    void Update()
    {
        // Si las vidas llegan a cero, el jugador muere
        if (vidas <= 0)
        {
            Die();
        }
    }

    // Método para reducir las vidas del jugador
    public void PerderVida()
    {
        vidas -= 1;
        Debug.Log("Vida del jugador: " + vidas);  // Mensaje en consola para depurar
    }

    // Función que maneja la muerte del jugador
    void Die()
    {
        // Cargar la escena llamada "Frank" cuando el jugador muere
        UnityEngine.SceneManagement.SceneManager.LoadScene("Frank");
    }

    // Detecta colisiones con otros objetos usando Collider 3D
    private void OnCollisionEnter(Collision col)
    {
        // Si el objeto con el que colisiona tiene el tag "Enemy", pierde vida
        if (col.gameObject.CompareTag("Enemy"))
        {
            PerderVida();
        }
    }

    // También puede ser que el jugador reciba daño por contacto con un Trigger (si es necesario)
    private void OnTriggerEnter(Collider col)
    {
        // Si el jugador entra en contacto con un objeto con el tag "Enemy", pierde vida
        if (col.gameObject.CompareTag("Enemy"))
        {
            PerderVida();
        }
    }
}
