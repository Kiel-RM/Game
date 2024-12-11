using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaSalida : MonoBehaviour
{
    public string nivel;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            SceneManager.LoadScene(nivel);
        }
    }
    
}
