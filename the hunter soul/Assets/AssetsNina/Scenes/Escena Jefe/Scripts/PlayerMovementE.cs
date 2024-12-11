using UnityEngine;

public class PlayerMovementEsp : MonoBehaviour
{
    public float moveSpeed = 5f;
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
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Flip character's direction based on movement
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
        }

        rb.velocity += Vector3.down * 20f * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastJumpTime > jumpCooldown)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            lastJumpTime = Time.time;
        }

        if (Time.time - lastDashTime > 1.0f && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 dashDirection = new Vector3(horizontalInput, 0.0f, 0.0f).normalized;
            Vector3 dashVelocity = dashDirection * dashForce;
            rb.velocity = dashVelocity;
            lastDashTime = Time.time;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 2.5f * Time.deltaTime;
        }
    }
}