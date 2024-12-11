using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float attackInterval = 30f;
    public float stuckTime = 5f;
    private Transform playerTransform;
    private Vector3 originalPosition;
    private bool hasAttacked = false;
    private bool isActive = false;
    private bool isStuck = false;
    private bool damageApplied = false;
    private float timeSinceLastAttack = 0f;
    private float stuckTimer = 0f;
    private Vector3 attackPosition;
    private Collider bossCollider;
    private Animator animator;
    private PlayerLife playerLife;

    void Start()
    {
        originalPosition = transform.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerLife = player.GetComponent<PlayerLife>();
        }
        bossCollider = GetComponent<Collider>();
        if (bossCollider != null)
        {
            bossCollider.enabled = false;
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= attackInterval)
        {
            SetAttackPosition();
            ActivateBossMovement(false, attackPosition);
        }

        if (isActive)
        {
            if (!hasAttacked)
            {
                MoveTowardsX(attackPosition.x);
                if (Mathf.Approximately(transform.position.x, attackPosition.x))
                {
                    hasAttacked = true;
                    isStuck = true;
                    stuckTimer = 0f;
                    if (bossCollider != null)
                    {
                        bossCollider.enabled = true;
                    }
                    if (animator != null)
                    {
                        animator.SetTrigger("NiñaAnimAtaque");
                        animator.SetBool("Walk", false); // Dejar de caminar cuando ataca
                    }
                }
            }
            else
            {
                if (isStuck)
                {
                    stuckTimer += Time.deltaTime;
                    DestroyBlockers();
                    if (stuckTimer >= stuckTime)
                    {
                        isStuck = false;
                    }
                }
                else
                {
                    MoveTowardsX(originalPosition.x);
                    if (Mathf.Approximately(transform.position.x, originalPosition.x))
                    {
                        hasAttacked = false;
                        isActive = false;
                        damageApplied = false;
                        if (bossCollider != null)
                        {
                            bossCollider.enabled = false;
                        }
                        if (animator != null)
                        {
                            animator.ResetTrigger("NiñaAnimAtaque");
                            animator.SetBool("Walk", false); // Dejar de caminar cuando regresa a la posición original
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !damageApplied)
        {
            PlayerLife playerLife = other.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.PerderVida();
                damageApplied = true; // Set damage flag
            }
        }
    }

    public void ActivateBossMovement(bool stuck, Vector3 targetPosition)
    {
        isActive = true;
        isStuck = false; // Asegúrate de que isStuck siempre sea false
        attackPosition = targetPosition;
        timeSinceLastAttack = 0f;
        if (animator != null)
        {
            animator.SetBool("Walk", true); // Comenzar a caminar cuando se activa el movimiento del jefe
        }
    }

    private void SetAttackPosition()
    {
        attackPosition = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
    }

    private void MoveTowardsX(float targetX)
    {
        if (!isStuck)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, targetX, moveSpeed * Time.deltaTime),
                transform.position.y,
                transform.position.z
            );
            if (animator != null)
            {
                animator.SetBool("Walk", true); // Asegurarse de que el jefe esté caminando mientras se mueve
            }
        }
    }

    private void DestroyBlockers()
    {
        StartCoroutine(DestroyBlockersWithDelay());
    }

    private IEnumerator DestroyBlockersWithDelay()
    {
        yield return new WaitForSeconds(1);
        GameObject[] blockers = GameObject.FindGameObjectsWithTag("Blocker");
        foreach (GameObject blocker in blockers)
        {
            Destroy(blocker);
        }
    }
}
