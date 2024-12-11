using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEspan : MonoBehaviour
{
    public GameObject[] objetos; // Array para almacenar los GameObjects que se pueden instanciar.
    public Transform[] spawnPoints; // Puntos de aparición
    public float spawnInterval = 2f; // Intervalo entre cada spawn
    public int maxZombies = 15; // Máximo número de objetos en la escena
    

    private float timeSinceLastSpawn = 0f;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        int currentObjects = GameObject.FindGameObjectsWithTag("Obstaculo").Length; // Contamos los objetos actuales en la escena.

        // Si hay espacio para más objetos, intentamos crear uno nuevo
        if (currentObjects < maxZombies)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnObject();
                timeSinceLastSpawn = 0f;
            }
        }
    }

    // Método para instanciar un objeto aleatorio
    private void SpawnObject()
    {
        if (spawnPoints.Length == 0 || objetos.Length == 0)
        {
            Debug.LogWarning("No spawn points or objects assigned!");
            return;
        }

        // Elegimos un punto de spawn aleatorio
        Transform randomSpawnPoint = GetRandomSpawnPoint();

        // Elegimos un objeto aleatorio del array de objetos
        GameObject randomObject = objetos[Random.Range(0, objetos.Length)];

        // Instanciamos el objeto en la posición del spawn
        GameObject spawnedObject = Instantiate(randomObject, randomSpawnPoint.position, randomSpawnPoint.rotation);

        // Llamamos al método para destruir el objeto después de un tiempo
        
    }

    // Método para elegir un punto de spawn aleatorio, sin que esté demasiado cerca del jugador
    private Transform GetRandomSpawnPoint()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 playerPosition = playerTransform.position;

        // Nos aseguramos de que el punto de spawn no esté en frente del jugador
        while (Vector3.Dot((randomSpawnPoint.position - playerPosition).normalized, playerTransform.forward) > 0.7f)
        {
            randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        }

        return randomSpawnPoint;
    }
}
