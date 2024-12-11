using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeEsp : MonoBehaviour
{
    
    void OnCollisionEnter(Collision otro)
    {
        if (otro.gameObject.tag == "Player")
        {

            //GameManager.Instantiate
            otro.gameObject.GetComponent<PlayerLifeEsp>().PerderVida();

            //danoaenemigo = otro.gameObject.GetComponent<vidaenemigo>();
            //danoaenemigo.Contarvida(dano);

            //Destroy(this.gameObject);
        }
    }


}
