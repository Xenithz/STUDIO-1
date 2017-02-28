using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    float presentStamina = 5;
    float maxStamina = 5;
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
	
    // Update is called once per frame
    void Update()
    {
		
        // Simple Walking
        RightLeft = Input.GetAxis("Horizontal") * speed * Time.deltaTime; 
        FrontBack = Input.GetAxis("Vertical") * speed * Time.deltaTime; 

        transform.Translate(RightLeft, 0, FrontBack); 


        if (Input.GetKeyDown(KeyCode.Q))
        {
            isRunning = true; 

        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            isRunning = false; 

        }


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



        if (isRunning)
        {
            presentStamina -= Time.deltaTime; 
            if (presentStamina < 0)
            {
                presentStamina = 0; 
                isRunning = false; 
            }
        }
        else if (presentStamina < maxStamina)
        {
            presentStamina += Time.deltaTime;  
        }


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
