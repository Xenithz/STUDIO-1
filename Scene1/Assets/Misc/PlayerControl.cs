using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayerControl : MonoBehaviour
{
    private enum SoundType
    {
        gravel,
        metal
    }

    private PlayerControl.SoundType soundType;
    public AudioClip[] footstepAudioClips;
    private AudioSource audio;

    private bool[] keys;
    bool keyIsDown;
    bool keyAlreadyDown;
    int audioIndex;


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
        rb = GetComponent<Rigidbody>();

        this.soundType = PlayerControl.SoundType.gravel;
        if (this.soundType == PlayerControl.SoundType.gravel)
        {
            this.audioIndex = 0;
        }
        else if (this.soundType == PlayerControl.SoundType.metal)
        {
            this.audioIndex = 4;
        }
        this.audio = base.GetComponent<AudioSource>();
        this.audio.clip = this.footstepAudioClips[this.audioIndex];
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

        tempAudio();
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

    void tempAudio()
    {
        this.keys = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.D)
            };
        if (this.keys[0] || this.keys[1] || this.keys[2] || this.keys[3])
        {
            this.keyIsDown = true;
            if (!this.audio.isPlaying)
            {
                if (this.soundType == PlayerControl.SoundType.gravel)
                {
                    if (this.audioIndex > 3)
                    {
                        this.audioIndex = 0;
                    }
                }
                else if (this.soundType == PlayerControl.SoundType.metal && this.audioIndex > 7)
                {
                    this.audioIndex = 4;
                }
                this.audio.clip = this.footstepAudioClips[this.audioIndex];
                this.audio.Play();
                this.audioIndex++;
            }
        }
        if (this.keyIsDown && !this.keyAlreadyDown)
        {
            this.keyAlreadyDown = true;
        }
        if (!this.keys[0] && !this.keys[1] && !this.keys[2] && !this.keys[3] && this.keyAlreadyDown)
        {
            this.audio.Stop();
            if (this.soundType == PlayerControl.SoundType.gravel)
            {
                this.audioIndex = 0;
            }
            else if (this.soundType == PlayerControl.SoundType.metal)
            {
                this.audioIndex = 4;
            }
            this.audio.clip = this.footstepAudioClips[this.audioIndex];
            this.keyIsDown = false;
            this.keyAlreadyDown = false;
        }
    }
}
