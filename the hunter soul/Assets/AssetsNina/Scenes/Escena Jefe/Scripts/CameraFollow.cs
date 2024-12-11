using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float damping;
    [SerializeField] private Vector3 offset;
    public Transform target;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;

        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);
        transform.position = newPosition;
    }
}
