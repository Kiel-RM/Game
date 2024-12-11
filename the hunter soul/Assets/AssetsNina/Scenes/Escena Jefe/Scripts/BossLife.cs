using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLife : MonoBehaviour
{
    public int vida = 3;
    public string NextBoss = "SceneJack";

    public void PerderVida()
    {
        vida--;
        Debug.Log("Vida del jefe: " + vida);

        if (vida <= 0)
        {
            StartCoroutine(CargarPantallaSiguiente());
        }
    }

    private IEnumerator CargarPantallaSiguiente()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SceneJack");
    }
}