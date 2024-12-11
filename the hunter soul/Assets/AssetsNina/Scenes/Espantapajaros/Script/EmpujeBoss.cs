using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpujeBoss : MonoBehaviour
{
    public float fuerzaempuje = 5f;
    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        // Repeler objetos al tocarlos
        RepelerObjeto(collision);
    }

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
            otroRb.AddForce(direccionRepulsion * fuerzaempuje, ForceMode.Impulse);
        }
    }
}
