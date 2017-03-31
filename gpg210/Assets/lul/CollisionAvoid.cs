using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoid : MonoBehaviour
{
    public Transform target;
    Rigidbody rb;

    float rayRange = 5.0f;

    public Vector3 steeringForce;
    public Vector3 desiredVelocity;
    public Vector3 velocity;
    public float maxVelocity;
    public float maxForce;
    public float maxSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {   

        RaycastHit hit;


        // Setting direction and distance

        Vector3 direction = (target.position - transform.position).normalized;





        RaycastHit hit2;
        RaycastHit hit3;

        // To create two raycast left and rigth (to avoid corners
        Vector3 left = transform.position;
        Vector3 right = transform.position;




        if (Physics.Raycast(right + (transform.right), transform.forward, out hit2, rayRange))
        {
            // if ray not hitting this object and hitting the gameobjects with tag 
            if (hit2.transform != transform && hit2.collider.gameObject.CompareTag("obb"))
            {
                // startRay += Time.time * 1.0f; 
                Debug.DrawRay(transform.position, hit2.point, Color.red);

                Vector3 hitNormal = hit2.normal;
                //adding normalized hit direction with some pulling away type force
                direction += transform.forward + hitNormal * 100.0f;



            }
        }

        if (Physics.Raycast(left - (transform.right), transform.forward, out hit3, rayRange))
        {
            // if ray not hitting this object and hitting the gameobjects with tag 
            if (hit2.transform != transform && hit3.collider.gameObject.CompareTag("obb"))
            {
                // startRay += Time.time * 1.0f; 
                Debug.DrawRay(transform.position, hit3.point, Color.red);
                Vector3 hitNormal = hit3.normal;


                //adding normalized hit direction with some pulling away type force
                direction += transform.forward + hitNormal * 100.0f;

            }
        }



        Move();
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
