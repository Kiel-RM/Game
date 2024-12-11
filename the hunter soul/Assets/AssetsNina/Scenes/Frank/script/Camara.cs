using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform target;  // El objetivo (jugador o cualquier otro objeto que desees seguir)
    public float damping = 0.2f; // La suavidad del movimiento de la c�mara (ajustado para ser m�s suave)

    private Vector3 velocity = Vector3.zero;  // La velocidad para SmoothDamp (suavizado del movimiento)

    private Vector3 initialPosition; // Para almacenar la posici�n inicial de la c�mara

    void Start()
    {
        // Asegurarse de que la c�mara comienza en la misma posici�n que en el editor (en este caso, -20.8, -9.1, -64.6).
        initialPosition = transform.position;

        if (target != null)
        {
            // Establecemos la c�mara en la posici�n deseada (sin moverla en X ni Y, solo en Z)
            transform.position = new Vector3(initialPosition.x, initialPosition.y, target.position.z + (initialPosition.z - target.position.z));
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculamos la nueva posici�n de la c�mara para seguir al jugador solo en el eje Z.
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y, target.position.z);

            // Suavizamos el movimiento en Z.
            Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);

            // Asignamos la nueva posici�n de la c�mara.
            transform.position = newPosition;
        }
    }
}
