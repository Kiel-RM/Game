using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebote : MonoBehaviour
{
    public float velocidad = 1;
    public int direccion = 1;
    public bool vigilando = true;
    void Update()
    {
        if (vigilando == true)
        {
            //Vector 3 guarda la posicion en X, Y y Z, Time es el tiempo en Unity y deltatime es para el tiempo consecutivo desde que inicia el juego
            transform.Translate(Vector3.forward * velocidad * direccion * Time.deltaTime);
            Debug.Log("Moviendose");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //Los debug.log son para saber que hace usando la consola de Unity
        Debug.Log("Chocando con collider");

        if (other.gameObject.tag == "Muro")
        {
            Debug.Log("Encontre un muro");

            if (direccion == 1)
            {
                Debug.Log("Encontre muro frontal");
                direccion = -1;
            }
            else
            {
                Debug.Log("Encontre muro trasero");
                direccion = 1;
            }
        }
    }
}
