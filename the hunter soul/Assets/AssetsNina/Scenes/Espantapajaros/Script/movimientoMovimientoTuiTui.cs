using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoMovimientoTuiTui : MonoBehaviour
{
    public float velocidad = 1.0f;
    public float llegar = 1.0f;
    public float velocidadrotacion = 10.0f;

    public Transform posicionjugador;
    public GameObject eljugador;

    void Start()
    {
        eljugador = GameObject.FindGameObjectWithTag("Player");
        posicionjugador = eljugador.GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 nuevofrente = Vector3.RotateTowards(transform.forward, posicionjugador.position - transform.position, Time.deltaTime * velocidadrotacion, 5.0f);
        transform.rotation = Quaternion.LookRotation(nuevofrente);

        float distancia = Vector3.Distance(transform.position, posicionjugador.position);
        if (distancia <= llegar)
        {

        }
        else
        {

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(posicionjugador.position.x, transform.position.y, posicionjugador.position.z) /*posicionjugador.position*/, Time.deltaTime * velocidad);
        }

    }
}
