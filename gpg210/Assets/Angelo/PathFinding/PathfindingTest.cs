using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{
    public PathfindingHandler path;
    public Transform target;
    public float speed;

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

    public Transform road;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        speed = 2 * Time.deltaTime;
        road = path.CreateAPath(transform, target);
        Move();
        Rotate();
        Debug.DrawLine(transform.position, road.position, Color.red);
        //transform.Translate((road.position - transform.position).normalized * 0.1f);
    }

    private void Move()
    {
        velocity = rb.velocity;
        desiredVelocity = Vector3.Normalize(road.position - transform.position) * maxVelocity;
        steeringForce = desiredVelocity - velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
        steeringForce = steeringForce / rb.mass;
        velocity = Vector3.ClampMagnitude(velocity + steeringForce, maxSpeed);
        transform.position += velocity;
    }

    private void Rotate()
    {
        direction = road.position - transform.position;
        rotationSpeed = 2.5f * Time.deltaTime;
        directionToSet = Vector3.RotateTowards(transform.forward, direction, rotationSpeed, 0);
        Quaternion lookAt = Quaternion.LookRotation(directionToSet);
        transform.rotation = lookAt;
    }
}
