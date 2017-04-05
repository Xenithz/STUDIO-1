using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public Image Logo;
    public Image Background;
    public float fadetimeLogo;
    public float fadetimeBackground;
    public float fadedelay;
    public float fadedelay2;
    public float ChangeScene;
    public float startdelay;

    // Use this for initialization
    void Start ()
    {
       // Logo.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        Background.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        //Fade();
        StartCoroutine("Fade");

    }

    // Update is called once per frame
    void Update ()
    {
        //Logo.CrossFadeAlpha(0f, fadetimeLogo, false);
       // Background.CrossFadeAlpha(0f, fadetimeBackground, false);
        //Background.color = Color.red;
        if(ChangeScene == 1)
        {
            SceneManager.LoadScene("Menu");
        }

    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(startdelay);

        Background.CrossFadeAlpha(0f, fadetimeBackground, false);

        yield return new WaitForSeconds(fadedelay);

        Background.CrossFadeAlpha(1f, fadetimeBackground, false);

        yield return new WaitForSeconds(fadedelay2);

        ChangeScene++;




    }
}
