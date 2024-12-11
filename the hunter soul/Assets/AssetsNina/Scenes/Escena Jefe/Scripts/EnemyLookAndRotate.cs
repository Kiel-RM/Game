using UnityEngine;
using System.Collections;

public class EnemyLookAndRotate : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float rotationAngle = 30f;
    public float pauseDuration = 0.5f;
    public float tolerance = 0.046f;

    private Quaternion initialRotation;
    private bool isPaused = false;
    private float elapsedTime = 0f;

    private EnemyShooter shooter;
    private Animator animator;

    void Start()
    {
        initialRotation = Quaternion.Euler(90, 180, 0);
        transform.rotation = initialRotation;
        shooter = GetComponent<EnemyShooter>();
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.Play("static");
        }
    }

    void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.PingPong(elapsedTime * rotationSpeed, rotationAngle * 2) - rotationAngle;
            transform.rotation = initialRotation * Quaternion.Euler(0, angle, 0);

            if (IsAtPauseAngle(angle))
            {
                StartCoroutine(PauseRotation());
            }
        }
    }

    bool IsAtPauseAngle(float angle)
    {
        return Mathf.Abs(angle - rotationAngle) < tolerance ||
               Mathf.Abs(angle + rotationAngle) < tolerance ||
               Mathf.Abs(angle) < tolerance;
    }

    IEnumerator PauseRotation()
    {
        isPaused = true;

        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        yield return new WaitForSeconds(0.3f);

        if (shooter != null)
        {
            shooter.FireProjectile();
        }

        yield return new WaitForSeconds(pauseDuration);
        isPaused = false;

        if (animator != null)
        {
            animator.SetTrigger("Static");
        }
    }
}
