using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPj : MonoBehaviour
{
    public Transform DestinoFinal;
    public float velocidad = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (DestinoFinal == null)
        {
            DestinoFinal = new GameObject("PuntoDestino").transform;
            DestinoFinal.position = transform.position + new Vector3(10f, 0f, 0f);
        }
    }

    void Update()
    {
        if (DestinoFinal != null)
        {
            MoverHaciaPunto();
        }
    }

    void MoverHaciaPunto()
    {
        float step = velocidad * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, DestinoFinal.position, step);
    }
}
