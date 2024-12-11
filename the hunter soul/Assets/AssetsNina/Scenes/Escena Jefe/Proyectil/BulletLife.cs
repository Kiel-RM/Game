using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLife : MonoBehaviour
{
    private static List<BulletLife> allBullets = new List<BulletLife>();

    private void OnEnable()
    {
        if (PlayerMovement.InputsBlocked)
        {
            Destroy(this.gameObject);
        }
        else
        {
            allBullets.Add(this);
        }
    }

    private void OnDisable()
    {
        allBullets.Remove(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossAttack bossAttack = FindObjectOfType<BossAttack>();
            if (bossAttack != null)
            {
                Vector3 playerPosition = other.transform.position;
                float attackDistance = 3f;
                float direction = Mathf.Sign(bossAttack.transform.position.x - playerPosition.x);
                Vector3 attackPosition = new Vector3(playerPosition.x + (attackDistance * direction), playerPosition.y, playerPosition.z);
                bossAttack.ActivateBossMovement(false, attackPosition);
            }

            GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject);
        }
    }

    public static void DestroyAllBullets()
    {
        foreach (var bullet in new List<BulletLife>(allBullets))
        {
            Destroy(bullet.gameObject);
        }
    }
}
