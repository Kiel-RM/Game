using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaSalto : MonoBehaviour
{

    public int saltosNecesarios = 3; // Número de saltos necesarios para dispararse
    private int saltosRecibidos = 0; // Contador de saltos recibidos
    public float impulsoVertical = 10f; // Fuerza con la que el objeto se dispara hacia arriba
    public float impulsoHorizontal = 5f; // Fuerza con la que el objeto se dispara hacia los lados
    public float impulsoAdelanteAtras = 5f; // Fuerza con la que el objeto se dispara hacia adelante o atrás
    private Rigidbody rb; // Referencia al Rigidbody para mover el objeto
    public float tiempoDestruccion = 3f; // Tiempo en segundos después de los saltos necesarios para destruir el objeto
    public AudioClip sonidoSalto; // Sonido que se reproduce cuando se cumplen los saltos necesarios
    private AudioSource audioSource; // Componente AudioSource para reproducir el sonido

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody del objeto
        audioSource = GetComponent<AudioSource>(); // Obtener el AudioSource del objeto
        if (audioSource == null)
        {
            Debug.LogError("AudioSource no encontrado en el objeto. Añádelo para reproducir el sonido.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (saltosRecibidos >= saltosNecesarios)
        {
            Disparar(); // Disparar al alcanzar los saltos necesarios
            saltosRecibidos = 0; // Reiniciar el contador de saltos

            // Reproducir el sonido cuando se alcanzan los saltos necesarios
            if (sonidoSalto != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoSalto); // Reproducir el sonido una vez
            }

            // Destruir el objeto después de "tiempoDestruccion" segundos
            Destroy(gameObject, tiempoDestruccion);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            saltosRecibidos++; // Incrementar el contador de saltos cuando el jugador colisiona con el objeto
        }
    }

    void Disparar()
    {
        rb.AddForce(Vector3.up * impulsoVertical, ForceMode.Impulse);
        rb.AddForce(Vector3.right * impulsoHorizontal, ForceMode.Impulse);
        rb.AddForce(Vector3.forward * impulsoAdelanteAtras, ForceMode.Impulse);
    }
}


