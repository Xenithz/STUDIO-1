using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public Rigidbody myRigidBody;

    public Vector3 ahead;
    public Vector3 ahead2;
    public Vector3 objectVector;

    public float maxAhead;
    public float sphereRadius = 5f;

    public Transform nearestObstacle;

    public LayerMask layerMask;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ahead = transform.position + Vector3.Normalize(myRigidBody.velocity) * maxAhead;
        ahead2 = transform.position + Vector3.Normalize(myRigidBody.velocity) * maxAhead * 0.5f;

        Collider[] obstacles = Physics.OverlapSphere(transform.position, 5f, layerMask);

        for (int i = 1; i < obstacles.Length - 1; i++)
        {
            for (int j = 0; i < obstacles.Length - 1; j++)
            {
                if (Vector3.Distance(transform.position, obstacles[j + 1].transform.position) > Vector3.Distance(transform.position, obstacles[j].transform.position))
                {
                    Collider temp = obstacles[j];
                    obstacles[j] = obstacles[j + 1];
                    obstacles[j + 1] = temp;
                }
            }
        }

        nearestObstacle = obstacles[0].transform;

        objectVector = transform.position - nearestObstacle.position;
        Debug.DrawLine(nearestObstacle.position, objectVector, Color.red);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
