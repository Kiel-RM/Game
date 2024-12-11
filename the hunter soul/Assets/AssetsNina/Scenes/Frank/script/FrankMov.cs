using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankMov : MonoBehaviour
{
    public Transform puntoDestino; 
    public float velocidad = 5f; 
    public float fuerzaRepulsiva = 5f; 

    private Rigidbody rb; // Componente Rigidbody3D del objeto

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el Rigidbody del objeto
        rb = GetComponent<Rigidbody>();

        // Si no hay un punto de destino asignado, crear uno por defecto en (0,0,0)
        if (puntoDestino == null)
        {
            puntoDestino = new GameObject("PuntoDestino").transform;
            puntoDestino.position = transform.position + new Vector3(10f, 0f, 0f); // Un destino predeterminado en X
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Mover el objeto hacia el punto destino con la velocidad ajustable
        if (puntoDestino != null)
        {
            MoverHaciaDestino();
        }
    }

    // Método para mover el objeto hacia el punto destino
    void MoverHaciaDestino()
    {
        // Calculamos el paso de movimiento
        float step = velocidad * Time.deltaTime;

        // Mover el objeto hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, puntoDestino.position, step);
    }

    // Detectar colisiones con otros objetos
    private void OnCollisionEnter(Collision collision)
    {
        // Repeler objetos al tocarlos
        RepelerObjeto(collision);
    }

    // Método para repeler objetos
    void RepelerObjeto(Collision collision)
    {
        // Solo repeler objetos que tengan un Rigidbody
        Rigidbody otroRb = collision.collider.GetComponent<Rigidbody>();
        if (otroRb != null)
        {
            // Calcular la dirección opuesta a la colisión
            Vector3 direccionRepulsion = collision.transform.position - transform.position;

            // Normalizar la dirección para que la fuerza de repulsión sea consistente
            direccionRepulsion.Normalize();

            // Aplicar una fuerza que repela el objeto
            otroRb.AddForce(direccionRepulsion * fuerzaRepulsiva, ForceMode.Impulse);
        }
    }
}

