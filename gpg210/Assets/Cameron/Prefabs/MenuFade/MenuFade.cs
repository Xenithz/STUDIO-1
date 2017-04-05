using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuFade : MonoBehaviour {


    public Image BackGround;
    public Image ForeGround;

    public Text Text;
    public Button Button;

    public float ChangeScene;

    public float fadetimeBackground;
    public float fadetimeforeground;

    public float startdelay;
    public float delay2;
    public float delay3;
    public float delay4;

    // Use this for initialization
    void Start ()
    {
        ForeGround.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        BackGround.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        Debug.Log("test");
        StartCoroutine("StoryFade");
    }
	
	// Update is called once per frame
	void Update ()
    {

		
	}

    IEnumerator StoryFade()
    {
        yield return new WaitForSeconds(startdelay);

        BackGround.gameObject.SetActive(true);
        ForeGround.gameObject.SetActive(true);


        BackGround.CrossFadeAlpha(1f, fadetimeBackground, false);
        ForeGround.CrossFadeAlpha(1f, fadetimeforeground, false);

        yield return new WaitForSeconds(delay2);

        Text.gameObject.SetActive(true);
        Button.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay3);

        ForeGround.CrossFadeAlpha(0f, fadetimeforeground, false);

        yield return new WaitForSeconds(fadetimeforeground);

        ForeGround.gameObject.SetActive(false);








    }
}
