using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulso : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad de movimiento
    public bool haciaAdelante = true; // Controla la dirección del movimiento (hacia adelante o atrás)
    public float tiempoDestruccion = 2f; // Tiempo en segundos antes de destruir el objeto tras la colisión

    private Vector3 direccionImpulso;

    void Start()
    {
        // dirección del movimiento osea hacia adelante
        SetDireccionImpulso();

        // Bloqueamos la rotación del Rigidbody por que si no empieza a dar vueltas
        BloquearRotacion();

        // Aplicamos el impulso inmediatamente para que salga con ganas
        AplicarImpulso();
    }

    void FixedUpdate()
    {
        // Aplicamos la fuerza para mover el objeto sin parar 
        MoverObjeto();
    }

    void SetDireccionImpulso()
    {
        if (haciaAdelante)
        {
            direccionImpulso = transform.forward; // hacia adelante
        }
        else
        {
            direccionImpulso = -transform.forward; // hacia atrás
        }
    }

    void MoverObjeto()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direccionImpulso * velocidad, ForceMode.Force);
        }
        else
        {
            Debug.LogWarning("No hay rigid body en el objeto el movimiento no va a jalar");
        }
    }

    void AplicarImpulso()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Evitar rotaciones
            rb.AddForce(direccionImpulso * velocidad, ForceMode.Impulse);
        }
    }

    // Bloquea la rotación del Rigidbody para evitar desvíos
    void BloquearRotacion()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    // Método que se activa cuando el objeto colisiona con otro
    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que colisiona tiene el tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruye este objeto después de un tiempo determinado
            Destroy(gameObject, tiempoDestruccion);
        }
    }
}

