using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image Image;
    public float fadetime;
    public bool fade = false;

    // Use this for initialization
    void Start()
    {
        Image.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fade = !fade;
        }
        if (fade == false)
        {

            Image.CrossFadeAlpha(1f, fadetime, false);
        }

        if (fade == true)
        {
            // Image.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            Image.CrossFadeAlpha(0f, fadetime, false);
        }

    }
}
