using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV5 : MonoBehaviour
{
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

    //Stamina

    float presentStamina = 2;
    float maxStamina = 2;
    bool isRunning;

    Rect staminaRect;
    Texture2D staminaTexture;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();


        staminaRect = new Rect(Screen.width / 10, Screen.height * 9 / 10, Screen.width / 3, Screen.height / 50);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.black);

        staminaTexture.Apply();

    }


    public float rotationspeed = 10.0f;


    // Update is called once per frame
    void Update()
    {

        if (grounded)
        {
            // Simple Walking
            RightLeft = Input.GetAxis("Horizontal");
            FrontBack = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(RightLeft, 0.0f, FrontBack);
            rb.velocity = movement *= speed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 200.0f;
            isRunning = true;
        }
        else
        {
            speed = 100.0f;
            isRunning = false;
        }

        if (isRunning)
        {
            presentStamina -= Time.deltaTime;
            if (presentStamina < 0)
            {
                presentStamina = 0;
                speed = 100.0f;
                isRunning = false;
            }
        }
        else if (presentStamina < maxStamina)
        {
            presentStamina += Time.deltaTime;
        }




        // Jump 
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rb.AddForce(0, jumpspeed, 0);
        }


        //Mouse Rotation 
        Xaxis = Input.GetAxis("Mouse X") * rotationspeed * Time.deltaTime;
        Yaxis -= Input.GetAxis("Mouse Y") * rotationSpeed;

        Yaxis = Mathf.Clamp(Yaxis, -cameraRange, cameraRange);

        transform.Rotate(0, Xaxis, 0);
        cameraRotate.transform.localEulerAngles = new Vector3(Yaxis, 0, 0);


    }


    void OnGUI()
    {
        float ratio = presentStamina / maxStamina;
        float rectWidth = ratio * Screen.width / 3;
        staminaRect.width = rectWidth;

        GUI.DrawTexture(staminaRect, staminaTexture);


    }

    void SetRunning(bool isRunning)
    {
        this.isRunning = isRunning;

        //isRunning ? runSpeed : walkSpeed;  

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
}
