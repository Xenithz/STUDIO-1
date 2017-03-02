using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTest : MonoBehaviour
{
    public Transform targetTransform;
    public List<Transform> wayPoints = new List<Transform>();
    public float distanceFromAgentToWaypoint;
    public float rotationSpeed;
    public Vector3 directionToWaypoint;
    public int i = 0;

    public Vector3 desiredVelocity;
    public Vector3 steering;
    public float maxVelocity = 5;
    public Rigidbody rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotationSpeed = 3f * Time.deltaTime;

        targetTransform = wayPoints[i];

        distanceFromAgentToWaypoint = Vector3.Distance(this.transform.position, targetTransform.position);

        Debug.Log(directionToWaypoint);

        directionToWaypoint = Vector3.Normalize(targetTransform.position - transform.position);

        Quaternion lookAt = Quaternion.LookRotation(directionToWaypoint);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, rotationSpeed);

        desiredVelocity = Vector3.Normalize(targetTransform.position - transform.position) * maxVelocity;
        steering = desiredVelocity - rb.velocity;
        rb.AddForce(steering);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);


        if (Input.GetKeyDown(KeyCode.I))
        {
            i++;
        }

        if(i == wayPoints.Count)
        {
            i = 0;
        }
    }
}
