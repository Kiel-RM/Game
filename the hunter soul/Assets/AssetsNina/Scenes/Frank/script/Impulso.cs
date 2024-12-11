using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulso : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad de movimiento
    public bool haciaAdelante = true; // Controla la direcci�n del movimiento (hacia adelante o atr�s)
    public float tiempoDestruccion = 2f; // Tiempo en segundos antes de destruir el objeto tras la colisi�n

    private Vector3 direccionImpulso;

    void Start()
    {
        // direcci�n del movimiento osea hacia adelante
        SetDireccionImpulso();

        // Bloqueamos la rotaci�n del Rigidbody por que si no empieza a dar vueltas
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
            direccionImpulso = -transform.forward; // hacia atr�s
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

    // Bloquea la rotaci�n del Rigidbody para evitar desv�os
    void BloquearRotacion()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    // M�todo que se activa cuando el objeto colisiona con otro
    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que colisiona tiene el tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruye este objeto despu�s de un tiempo determinado
            Destroy(gameObject, tiempoDestruccion);
        }
    }
}

