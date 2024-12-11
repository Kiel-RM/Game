using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int vida = 3;
    public string gameOverSceneName = "Game Over";

    public void PerderVida()
    {
        vida--;
        Debug.Log("Vida del jugador: " + vida);

        if (vida <= 0)
        {
            StartCoroutine(CargarPantallaDerrota());
        }
    }

    private IEnumerator CargarPantallaDerrota()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(gameOverSceneName);
    }
}
