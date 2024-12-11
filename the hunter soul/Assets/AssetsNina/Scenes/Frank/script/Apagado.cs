using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apagado : MonoBehaviour
{
    // Referencia al GameObject "Control"
    public GameObject control;

    // Start is called before the first frame update
    void Start()
    {
        // Si no se asignó el GameObject en el inspector, lo buscamos por nombre
        if (control == null)
        {
            control = GameObject.Find("Control");
        }
    }

    // Este método se llama cuando un objeto con un collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisionó es el jugador (se puede ajustar dependiendo del nombre o etiqueta del jugador)
        if (other.CompareTag("Player"))
        {
            // Desactivar el GameObject "Control"
            if (control != null)
            {
                control.SetActive(false);
            }
        }
    }
}

