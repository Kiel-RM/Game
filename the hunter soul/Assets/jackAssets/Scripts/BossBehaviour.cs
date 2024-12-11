using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    private Animator animator;
    private float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 15f && timer < 15.1f)
        {
            animator.SetTrigger("daño");
        }
        else if (timer >= 30f && timer < 30.1f)
        {
            animator.SetTrigger("daño");
        }
        else if (timer >= 45f && timer < 45.1f)
        {
            animator.SetTrigger("daño");
        }
        else if (timer >= 60f)
        {
            //SceneManager.LoadScene("Victoria");
            Debug.Log("Victoria");

            // Crear un nuevo GameObject para contener el componente Winner
            GameObject winObject = new GameObject("WinViewObject");

            // Añadir el componente Winner al nuevo GameObject
            Winner winView = winObject.AddComponent<Winner>();

            // Llamar al método MenuPrincipal del componente Winner
            winView.MenuPrincipal("Winner");

        }
    }
}
