using UnityEngine;
using System.Collections;

public class EnemyLookAndRotate3Esp : MonoBehaviour
{
    public float rotationSpeed = 25f;    // Rotation speed (degrees per second)
    public float rotationAngle = 45f;    // Maximum rotation angle (in degrees)
    public float pauseDuration = 0.5f;   // Pause duration (seconds)
    public float tolerance = 0.08f;       // Tolerance for angle comparison

    private Quaternion initialRotation;  // Initial downward rotation
    private bool isPaused = false;       // Check if enemy is in paused state
    private float elapsedTime = 0f;      // Tracks manual time for rotation
    private float pauseEndTime = 0f;     // Time when the pause ends

    private EnemyShooterEsp shooter;        // Reference to the EnemyShooter component

    void Start()
    {
        // Set the enemy to look directly downward (90 degrees on X axis)
        initialRotation = Quaternion.Euler(90, 0, 0);
        transform.rotation = initialRotation;

        // Get the EnemyShooter component
        shooter = GetComponent<EnemyShooterEsp>();
    }

    void Update()
    {
        if (!isPaused)
        {
            // Increment custom time tracker for rotation
            elapsedTime += Time.deltaTime;

            // Use PingPong to calculate the rotation angle between -45 and 0
            float angle = Mathf.PingPong(elapsedTime * rotationSpeed, rotationAngle) - rotationAngle;

            // Apply rotation around Y-axis
            transform.rotation = initialRotation * Quaternion.Euler(0, angle, 0);

            // Check if the rotation is at the pause point within tolerance
            if (IsAtPauseAngle(angle))
            {
                // Start the pause coroutine if not already paused
                if (!isPaused)
                {
                    StartCoroutine(PauseRotation());
                }
            }
        }
    }

    bool IsAtPauseAngle(float angle)
    {
        // Check if angle is within tolerance of the key points
        return Mathf.Abs(angle - rotationAngle) < tolerance ||
               Mathf.Abs(angle + rotationAngle) < tolerance ||
               Mathf.Abs(angle) < tolerance;
    }

    IEnumerator PauseRotation()
    {
        isPaused = true;    // Pause rotation

        // Fire projectile
        if (shooter != null)
        {
            shooter.FireProjectile(); // Fire projectile each time the enemy pauses
        }

        yield return new WaitForSeconds(pauseDuration); // Wait for the specified pause duration
        isPaused = false;   // Resume rotation
    }
}