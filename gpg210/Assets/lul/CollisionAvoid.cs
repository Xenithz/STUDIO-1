using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoid : MonoBehaviour
{
    public Transform target;
    Rigidbody rb;

    Vector3 direction;

    public float force;

    public Vector3 velocity;
    public Vector3 desiredVelocity;
    public Vector3 steeringForce;
    public float maxSpeed;
    public float maxForce;
    public float maxVelocity;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {

        // Declaring velocity of the object's rigidbody
        velocity = rb.velocity;

        // setting desired velocity by normalizing the distance between target postion and this object postion 
        desiredVelocity = Vector3.Normalize(target.position - transform.position) * maxVelocity;

        RaycastHit hit;

        float maxDis = 10.0f;

        // declaring vector position of forward position where the AI would be 
        Vector3 ahead = transform.position + velocity.normalized * maxDis;

        Debug.DrawLine(transform.position, transform.position + transform.forward * maxDis, Color.red);

        // pointing at the ahead postion of AI
        transform.LookAt(ahead);
        //  if shooting a raycats from all the three side of this object with distance to travel

        // Shotting a raycast   
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDis))
        {
            // If hitting any object with tag "obb"
            if (hit.collider.gameObject.CompareTag("obb"))
            {
                //then
                // setting aviodence which is normiling the distance between forward postion of this object and positon of object which we are about to collide
                Vector3 aviodence = (ahead - hit.transform.position).normalized;

                //adding two vectors and multipling it with force 
                desiredVelocity += aviodence * 500.0f * Time.deltaTime;
            }
        }
        // To create two raycast left and rigth (to avoid corners
        Vector3 left = transform.position;

        Vector3 right = transform.position;

        // Shotting a raycast   
        if (Physics.Raycast(right + (transform.right * 5f), transform.forward, out hit, maxDis))
        {
            //hitting the gameobjects with tag 
            if (hit.collider.gameObject.CompareTag("obb"))
            {
                //then
                // setting aviodence which is normiling the distance between forward postion of this object and positon of object which we are about to collide
                Vector3 aviodence = (ahead - hit.transform.position).normalized;
                // Debug.DrawLine(hit2.transform.position, ahead, Color.white);

                //adding two vectors and multipling it with force 
                desiredVelocity += aviodence * 500.0f * Time.deltaTime;
            }
        }

        // Shotting a raycast   
        if (Physics.Raycast(left - (transform.right * 5f), transform.forward, out hit, maxDis))
        {
            //hitting the gameobjects with tag 
            if (hit.collider.gameObject.CompareTag("obb"))
            {
                //then
                // setting aviodence which is normiling the distance between forward postion of this object and positon of object which we are about to collide
                Vector3 aviodence = (ahead - hit.transform.position).normalized;

                //adding two vectors and multipling it with force 
                desiredVelocity += aviodence * 500.0f * Time.deltaTime;

            }
        }

        steeringForce = desiredVelocity - velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
        rb.AddForce(steeringForce);
        rb.velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }


    private void Move()
    {
        velocity = rb.velocity;
        desiredVelocity = Vector3.Normalize(target.position - transform.position) * maxVelocity;
        steeringForce = desiredVelocity - velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
        steeringForce = steeringForce / rb.mass;
        velocity = Vector3.ClampMagnitude(velocity + steeringForce, maxSpeed);
        transform.position += velocity;
    }
}
