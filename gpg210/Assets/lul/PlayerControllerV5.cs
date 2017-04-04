using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV5 : MonoBehaviour
{
    private GameObject gameManager;

    // Simple Walk
    public float speed;
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

    float presentStamina = 1;
    float maxStamina = 1;
    public bool isRunning;

    //Stamina Bar
    Rect staminaRect;
    Texture2D staminaTexture;

    public bool isDead;

    //public Handler handle;

    public GameObject head;

    // Use this for initialization
    void Start()
    {
        // Calling Component from the root of the object (Rigibody) 
        rb = GetComponent<Rigidbody>();


        // Setting Stamina Bar 

        // Stamina Bar (adjusting the size and shape of bar)
        staminaRect = new Rect(Screen.width / 10, Screen.height * 9 / 10, Screen.width / 3, Screen.height / 50);

        // 2D texture 
        staminaTexture = new Texture2D(1, 1);

        //setting pixel color black 
        staminaTexture.SetPixel(0, 0, Color.white);

        // Appling the color to the pixels 
        staminaTexture.Apply();

        isDead = false;

        gameManager = GameObject.Find("GameManager");

        //handle = GameObject.Find("AIFINAL").GetComponent<Handler>();

       // head = GameObject.Find("AIFINAL").transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       if(gameManager.GetComponent<GameManagerHandler>().gameManagerInstance.currentPauseState != GameManager.PauseState.paused)
        {
            if (isDead != true)
            {
                // Simple Walking

                // Setting simple walk system multiplying by speed per second 
                RightLeft = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                FrontBack = Input.GetAxis("Vertical") * speed * Time.deltaTime;

                transform.Translate(RightLeft * Time.deltaTime, 0, FrontBack * Time.deltaTime);







                // if and else for sprint 

                // left shift for sprint 
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // If pressed speed changes with isRunning set to true 
                    speed = 200.0f;
                    isRunning = true;
                }
                else
                {
                    // Else speed remains same and isRunning remains false
                    speed = 100.0f;
                    isRunning = false;
                }

                // If is running is ture 
                if (isRunning)
                {
                    // Subtratcing present stamina per second 
                    presentStamina -= Time.deltaTime;

                    // IF present stamina is less than 0
                    if (presentStamina < 0)
                    {
                        // Speed changes to the normal walk speed and isRunning goes to false
                        presentStamina = 0;
                        speed = 100.0f;
                        isRunning = false;
                    }
                }

                // Genrating stamina bar again 
                // IF present stamina is less than maxStamina anytime 
                else if (presentStamina < maxStamina)
                {
                    // increasing present stamina per second 
                    presentStamina += Time.deltaTime;
                }


                //Mouse Rotation 
                Xaxis = Input.GetAxis("Mouse X") * rotationSpeed;
                Yaxis -= Input.GetAxis("Mouse Y") * rotationSpeed;

                // Claping yaxis range between max and minimum range 
                Yaxis = Mathf.Clamp(Yaxis, -cameraRange, cameraRange);

                transform.Rotate(0, Xaxis, 0);

                // Rotating camera 
                cameraRotate.transform.localEulerAngles = new Vector3(Yaxis, 0, 0);
            }
        }
    }


    void OnGUI()
    {
        // ratio of how much stamina bar is filled/empty
        float ratio = presentStamina / maxStamina;
        //Calculating width of the texture taking the ratio of the stamina bar
        float rectWidth = ratio * Screen.width / 3;
        //Setting width of stamina bar which is calculated taking the ratio of the stamina bar
        staminaRect.width = rectWidth;

        // Drawing stamina bar in shape of rectangle 
        GUI.DrawTexture(staminaRect, staminaTexture);


    }


    // Setting jump if player is on ground or not
    void OnTriggerStay(Collider col)
    {
        // Player collides with floor 
        if (col.gameObject.tag == "floor")
        {
            // then grounded is true 
            grounded = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        // IF player is not colling with floor 
        if (col.gameObject.tag != "floor")
        {
            //then grounded is false
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "triggerEvent")
        {
            other.gameObject.GetComponent<TriggerZoneEvent>().EventTrigger();
        }
    }

    public void Die()
    {
        gameManager.GetComponent<GameManagerHandler>().gameManagerInstance.PlayerHasDied();
    }

    public void ForcedTurn()
    {
        Debug.Log("GO");
        Vector3 direc = Vector3.Normalize(head.transform.position - cameraRotate.transform.position);

        Quaternion lookAt = Quaternion.LookRotation(direc);

        transform.rotation = Quaternion.Lerp(cameraRotate.transform.rotation, lookAt, 2f * Time.deltaTime);
    }

}