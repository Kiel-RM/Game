using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeJack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            other.GetComponent<PlayerLife>().PerderVida();
            Destroy(gameObject);
        }
    }
}
