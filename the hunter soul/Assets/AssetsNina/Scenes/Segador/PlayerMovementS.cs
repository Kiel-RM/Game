using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementS : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float jumpForce = 12f;
    public float dashForce = 10f;
    public float dashCooldown = 1f;
    public float jumpCooldown = 1.2f;
    

    private float lastDashTime;
    private float lastJumpTime;
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        lastDashTime = -dashCooldown;
        lastJumpTime = -jumpCooldown;
    }

    private void Update()
    {
        ApplyCustomGravity();
        HandleMovement();
        HandleJump();
        HandleDash();
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
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastJumpTime > jumpCooldown)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            lastJumpTime = Time.time;
        }
    }

    private void HandleDash()
    {
        if (Time.time - lastDashTime > dashCooldown && Input.GetKeyDown(KeyCode.LeftShift))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 dashDirection = new Vector3(horizontalInput, 0.0f, 0.0f).normalized;
            Vector3 dashVelocity = dashDirection * dashForce;
            rb.velocity = dashVelocity;
            lastDashTime = Time.time;
        }
    }

}
