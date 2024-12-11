using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeJack : MonoBehaviour
{
    public int vida = 3;
    private bool esInvulnerable = false;
    //public string gameOverSceneName = "Game Over";

    public void PerderVida()
    {
        if (!esInvulnerable)
        {
            vida--;
            Debug.Log("Vida del jugador: " + vida);

            if (vida <= 0)
            {
                StartCoroutine(CargarPantallaDerrota());
            }
            else
            {
                StartCoroutine(InvulnerabilidadTemporal());
            }
        }
    }

    private IEnumerator CargarPantallaDerrota()
    {
        //yield return new WaitForSeconds(3);
        //SceneManager.LoadScene(gameOverSceneName);
        Debug.Log("Game Over");
        yield return null;
    }

    private IEnumerator InvulnerabilidadTemporal()
    {
        esInvulnerable = true;
        yield return new WaitForSeconds(3);
        esInvulnerable = false;
    }
}
