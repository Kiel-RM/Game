using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public float dashForce = 10f;
    public float dashCooldown = 1f;
    public float jumpCooldown = 1.2f;
    public float inputBlockDuration = 2f;

    private float lastDashTime;
    private float lastJumpTime;
    private float inputBlockStartTime;
    private Rigidbody rb;
    private Animator animator;
    private PlayerAttack playerAttack; // Referencia al componente PlayerAttack
    private static bool inputsBlocked = false;

    public static bool InputsBlocked => inputsBlocked;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>(); // Obtener el componente PlayerAttack
        rb.useGravity = false;
        lastDashTime = -dashCooldown;
        lastJumpTime = -jumpCooldown;
    }

    private void Update()
    {
        ApplyCustomGravity();

        if (inputsBlocked)
        {
            if (Time.time - inputBlockStartTime >= inputBlockDuration)
            {
                UnblockInputs();
            }
            else
            {
                return;
            }
        }

        HandleMovement();
        HandleJump();
        HandleDash();
        HandleAnimations();
    }

    private void ApplyCustomGravity()
    {
        rb.velocity += Vector3.down * 20f * Time.deltaTime;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 2.5f * Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(4, 4, Mathf.Sign(horizontalInput) * 4);
            animator.SetBool("Correr", true);
            animator.SetBool("Standing", false);
        }
        else
        {
            animator.SetBool("Correr", false);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastJumpTime > jumpCooldown)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            lastJumpTime = Time.time;
            StartCoroutine(ActivateJumpStart());
        }
    }

    private IEnumerator ActivateJumpStart()
    {
        animator.SetBool("JumpStart", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("JumpStart", false);
    }

    private void HandleDash()
    {
        if (Time.time - lastDashTime > dashCooldown && Time.time - lastJumpTime > jumpCooldown && Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded())
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 dashDirection = new Vector3(horizontalInput, 0.0f, 0.0f).normalized;
            Vector3 dashVelocity = dashDirection * dashForce;
            rb.velocity = dashVelocity;
            lastDashTime = Time.time;
            animator.SetTrigger("Dash");
        }
    }

    private void HandleAnimations()
    {
        bool isGrounded = IsGrounded();
        bool isNearGround = IsNearGround();

        if (!isGrounded && !isNearGround)
        {
            animator.SetBool("JumpMiddle", true);
            animator.SetBool("JumpEnd", false);
            animator.SetBool("Standing", false);
        }
        else if (isNearGround && !isGrounded)
        {
            animator.SetBool("JumpMiddle", false);
            animator.SetBool("JumpEnd", true);
            animator.SetBool("Standing", false);
        }
        else if (isGrounded && !playerAttack.IsAttacking() && !animator.GetBool("Correr")) // Verificar si el personaje est� atacando y no corriendo
        {
            animator.SetBool("JumpMiddle", false);
            animator.SetBool("JumpEnd", false);
            animator.SetBool("Standing", true);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    private bool IsNearGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f);
    }

    public void BlockInputs()
    {
        inputsBlocked = true;
        inputBlockStartTime = Time.time;
        BulletLife.DestroyAllBullets();
    }

    public void UnblockInputs()
    {
        inputsBlocked = false;
    }
}