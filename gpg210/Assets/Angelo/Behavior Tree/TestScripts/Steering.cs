using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public Transform player;
    public Vector3 steeringForce;
    public Vector3 desiredVelocity;
    public Vector3 velocity;
    public float maxVelocity;
    public float maxForce;
    public float maxSpeed;
    public Rigidbody rb;

    public Vector3 direction;
    public Vector3 directionToSet;
    public float rotationSpeed;

    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        velocity = rb.velocity;
        desiredVelocity = Vector3.Normalize(player.position - transform.position) * maxVelocity;
        steeringForce = desiredVelocity - velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
        steeringForce = steeringForce / rb.mass;
        velocity = Vector3.ClampMagnitude(velocity + steeringForce, maxSpeed);
        transform.position += velocity;
    }

    private void Rotate()
    {
        direction = player.position - transform.position;
        rotationSpeed = 2 * Time.deltaTime;
        directionToSet = Vector3.RotateTowards(transform.forward, direction, rotationSpeed, 0);
        Quaternion lookAt = Quaternion.LookRotation(directionToSet);
        transform.rotation = lookAt;
    }
}
