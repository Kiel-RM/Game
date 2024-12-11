using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    
    public VidaEnemigo danoaenemigo;
    public int dano = 1;
    

    private void OnCollisionEnter(Collision otro)
    {
        if (otro.gameObject.tag == "Enemigo")
        {

            danoaenemigo = otro.gameObject.GetComponent<VidaEnemigo>();
            danoaenemigo.Contarvida(dano);
            Destroy(this.gameObject);
        }
    }
}
