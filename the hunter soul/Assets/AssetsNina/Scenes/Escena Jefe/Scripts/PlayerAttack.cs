using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2.3f;
    public int attackDamage = 1;
    public string enemyTag = "Enemy";
    public float attackAngle = 45f;

    private int currentAttack = 0;
    private Animator animator;
    private Dictionary<int, float> attackDurations = new Dictionary<int, float>();
    private bool isAttacking = false;
    private bool isCooldown = false;
    private bool damageApplied = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackDurations[1] = 0.5f;
        attackDurations[2] = 0.7f;
        attackDurations[3] = 0.9f;
    }

    private void Update()
    {
        HandleAttack();
        HandleReturnToStanding();
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isCooldown)
        {
            currentAttack = (currentAttack % 3) + 1;
            animator.SetBool("Standing", false);
            animator.SetTrigger($"Attack{currentAttack}");
            PerformAttack();
            isAttacking = true;
            StartCoroutine(ReturnToStandingAfterDelay(attackDurations[currentAttack]));
        }
    }

    private IEnumerator ReturnToStandingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("Standing", true);
        isAttacking = false;
    }

    private void HandleReturnToStanding()
    {
        if (!isAttacking && currentAttack > 0)
        {
            currentAttack = 0;
            animator.SetBool("Standing", true);
            StartCoroutine(CooldownCoroutine());
            damageApplied = false; // Restablecer damageApplied aquí
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(1f);
        isCooldown = false;
    }

    private void PerformAttack()
    {
        if (currentAttack == 3)
        {
            Vector3 startPosition = transform.position + Vector3.up;
            Vector3 direction = transform.forward;
            float extendedRange = attackRange;
            Debug.DrawRay(startPosition, direction * extendedRange, Color.red, 1f);

            RaycastHit[] hits = Physics.RaycastAll(startPosition, direction, extendedRange);

            foreach (RaycastHit hit in hits)
            {
                Collider enemy = hit.collider;
                if (enemy == null) continue;

                if (enemy.CompareTag(enemyTag))
                {
                    Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
                    float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

                    if (angleToEnemy <= attackAngle / 2)
                    {
                        Debug.Log($"Hit {enemy.name} with attack {currentAttack}, dealing {attackDamage} damage.");
                        BossLife bossLife = enemy.GetComponent<BossLife>();
                        if (bossLife != null && !damageApplied)
                        {
                            bossLife.PerderVida();
                            damageApplied = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !damageApplied)
        {
            BossLife bossLife = other.GetComponent<BossLife>();
            if (bossLife != null)
            {
                bossLife.PerderVida();
                damageApplied = true;
            }
        }
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
