using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    public Image Logo;
    public Image Background;
    public float fadetimeLogo;
    public float fadetimeBackground;

    // Use this for initialization
    void Start ()
    {
        Logo.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        Background.GetComponent<CanvasRenderer>().SetAlpha(1.0f);


    }

    // Update is called once per frame
    void Update ()
    {
        Logo.CrossFadeAlpha(0f, fadetimeLogo, false);
        Background.CrossFadeAlpha(0f, fadetimeBackground, false);

    }
}
