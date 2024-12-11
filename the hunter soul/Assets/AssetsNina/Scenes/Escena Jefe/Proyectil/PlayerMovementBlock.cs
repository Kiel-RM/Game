using UnityEngine;
using System.Collections;

public class PlayerMovementBlock : MonoBehaviour
{
    public float blockDuration = 2f;
    private PlayerMovement playerMovement;
    private bool isBlocking = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isBlocking)
        {
            playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                isBlocking = true;
                playerMovement.BlockInputs();
                StartCoroutine(ReactivarMovimiento());
            }
        }
    }

    private IEnumerator ReactivarMovimiento()
    {
        yield return new WaitForSeconds(blockDuration);
        if (playerMovement != null)
        {
            playerMovement.UnblockInputs();
        }
        isBlocking = false;
    }
}

