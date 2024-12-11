using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeEsp : MonoBehaviour
{
    
    private int vidas = 1;


    void Update()
    {

        


        if (vidas == 0)
        {
            Die();
        }
    }

    public void PerderVida()
    {
        vidas -= 1;
     

    }

    void Die()
    {
        SceneManager.LoadScene("Game Over");
    }
}
