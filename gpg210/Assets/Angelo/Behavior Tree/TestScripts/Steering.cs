using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public Transform currentTarget;
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

    public List<Transform> wayPoints = new List<Transform>();
    public int i = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        currentTarget = wayPoints[i];
        Move();
        Rotate();

        if (Input.GetKeyDown(KeyCode.I))
        {
            i++;
        }

        if (i == wayPoints.Count)
        {
            i = 0;
        }
    }

    private void Move()
    {
        velocity = rb.velocity;
        desiredVelocity = Vector3.Normalize(currentTarget.position - transform.position) * maxVelocity;
        steeringForce = desiredVelocity - velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
        steeringForce = steeringForce / rb.mass;
        velocity = Vector3.ClampMagnitude(velocity + steeringForce, maxSpeed);
        transform.position += velocity;
    }

    private void Rotate()
    {
        direction = currentTarget.position - transform.position;
        rotationSpeed = 2 * Time.deltaTime ;
        directionToSet = Vector3.RotateTowards(transform.forward, direction, rotationSpeed, 0);
        Quaternion lookAt = Quaternion.LookRotation(directionToSet);
        transform.rotation = lookAt;
    }
}
