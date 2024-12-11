using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    // Variable que podr�a usarse si deseas tener control sobre la escena final
    public string escenaGanadora = "SceneNina";

    void OnDestroy()
    {
        if (gameObject.CompareTag("Enemigo"))
        {
            SceneManager.LoadScene("SceneNina");
        }
    }
}
