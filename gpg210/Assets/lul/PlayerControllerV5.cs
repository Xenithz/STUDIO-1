using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV5 : MonoBehaviour
{
    private GameObject gameManager;

    // Simple Walk
    public float speed;
    Rigidbody rb;
    public float FrontBack;
    public float RightLeft;

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
    bool showing = true;

    public bool isDead;

    public HeadBob headBob;

    public GameObject aIHead;

    public bool isAllowedToStartMoving;

    public float timerForStartingToMove;

    public AudioSource breathing;

    public bool breathingAudioCue;

    // Use this for initialization
    void Awake()
    {
        // Calling Component from the root of the object (Rigibody) 
        rb = GetComponent<Rigidbody>();


        // Setting Stamina Bar 

        // Stamina Bar (adjusting the size and shape of bar)
        staminaRect = new Rect(Screen.width / 10, Screen.height * 9 / 10, Screen.width / 3, Screen.height / 50);

        // 2D texture 
        staminaTexture = new Texture2D(1, 1);

        //setting pixel color  
        staminaTexture.SetPixel(0, 0, Color.white);

        // Appling the color to the pixels 
        staminaTexture.Apply();

        isDead = false;

        gameManager = GameObject.Find("GameManager");

        headBob = GetComponentInChildren<HeadBob>();

        isRunning = true;

        isAllowedToStartMoving = false;

        timerForStartingToMove = 0f;

        presentStamina = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAllowedToStartMoving == false)
        {
            timerForStartingToMove += Time.deltaTime;
        }

        if(timerForStartingToMove >= 1)
        {

            if (gameManager.GetComponent<GameManagerHandler>().gameManagerInstance.currentPauseState != GameManager.PauseState.paused)
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
                        // bar is displayed
                        showing = true;


                    }
                    //if maxStamina is equal to present stamina
                    else if (maxStamina == presentStamina)
                    {
                        //bar will dispaear
                        showing = false;
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
                        SprintBreathing();

                        // Subtratcing present stamina per second 
                        presentStamina -= Time.deltaTime;

                        // IF present stamina is less than 0
                        if (presentStamina < 0)
                        {
                            // Speed changes to the normal walk speed and isRunning goes to false
                            presentStamina = 0;
                            speed = 100.0f;
                            isRunning = false;
                            breathing.Stop();
                        }
                    }

                    // Genrating stamina bar again 
                    // IF present stamina is less than maxStamina anytime 
                    else if (presentStamina < maxStamina)
                    {
                        // increasing present stamina per 10 seconds
                        presentStamina += Time.deltaTime / 2.0f;
                        //Claminping the value of present stamina 
                        presentStamina = Mathf.Clamp(presentStamina, 0, maxStamina);
                    }

                    else if (!isRunning)
                    {
                        breathing.Stop();
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
    }


    void OnGUI()
    {
        if (showing == true)
        {
            //setting pixel color white
            staminaTexture.SetPixel(0, 0, Color.white);

            // Appling the color to the pixels 
            staminaTexture.Apply();

            // ratio of how much stamina bar is filled/empty
            float ratio = presentStamina / maxStamina;

            //Calculating width of the texture taking the ratio of the stamina bar
            float rectWidth = ratio * Screen.width / 3;

            //Setting width of stamina bar which is calculated taking the ratio of the stamina bar
            staminaRect.width = rectWidth;

            // Drawing stamina bar in shape of rectangle 
            GUI.DrawTexture(staminaRect, staminaTexture);


        }


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


    //Angelo's functions

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
        Vector3 direc = Vector3.Normalize(aIHead.transform.position - cameraRotate.transform.position);

        Quaternion lookAt = Quaternion.LookRotation(direc);

        transform.rotation = Quaternion.Lerp(cameraRotate.transform.rotation, lookAt, 2f * Time.deltaTime);
    }

    public void SprintBreathing()
    {
        if (breathing.isPlaying != true)
        {
            breathingAudioCue = true;
        }
        if (breathingAudioCue == true)
        {
            breathing.PlayOneShot(breathing.clip, 2);
            breathingAudioCue = false;
        }
    }

}