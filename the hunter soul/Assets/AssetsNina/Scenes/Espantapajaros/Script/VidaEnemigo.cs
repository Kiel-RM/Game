using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaEnemigo : MonoBehaviour
{
    public int vida = 1;
    public string nivel;

    void Update()
    {
        Morir();
    }
    void Morir()
    {
        if (vida <= 0)
        {
            Destroy(this.gameObject);
            Invoke("SigNivel", 2f);
        }
    }

    public void Contarvida(int dano)
    {
        vida = vida - dano;
        Debug.Log("Mi vida es: " + vida);
    }

    void SigNivel() 
    {
        //cargar nivel despues de muerte
        SceneManager.LoadScene(nivel);
    }
}
