using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameObject gameManager;

    // Simple Walk
    public float speed;
    public float run;

    Rigidbody rb;

    float FrontBack;
    float RightLeft;

    // jump

    public float jumpspeed;
    public bool grounded = false;

    // Rotation
    // Mouse Movement
    float Xaxis;
    float Yaxis;

    public float rotationSpeed;
    public float cameraRange = 80.0f;
    public GameObject cameraRotate;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // Simple Walking
        RightLeft = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        FrontBack = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(RightLeft, 0, FrontBack);




        // Sprints 
        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * run * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * run * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * run * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * run * Time.deltaTime);
        }

        // Jump 
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rb.AddForce(0, jumpspeed, 0);
        }


        //Mouse Rotation 
        Xaxis = Input.GetAxis("Mouse X") * rotationSpeed;
        Yaxis -= Input.GetAxis("Mouse Y") * rotationSpeed;

        Yaxis = Mathf.Clamp(Yaxis, -cameraRange, cameraRange);

        transform.Rotate(0, Xaxis, 0);
        cameraRotate.transform.localEulerAngles = new Vector3(Yaxis, 0, 0);
    }

    // Setting jump if player is on ground or not
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "floor")
        {
            grounded = true;
        }
    }

    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "floor")
        {

            grounded = false;
        }
    }

    public void Die()
    {
        gameManager.GetComponent<GameManagerHandler>().gameManagerInstance.PlayerHasDied();
    }
}
