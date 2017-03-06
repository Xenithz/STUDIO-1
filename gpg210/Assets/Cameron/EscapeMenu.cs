using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour {

    public GameObject GameUI;
    bool GameUIActive;
	

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameUIActive == true)
            {
                GameUI.gameObject.SetActive(false);
                GameUIActive = false;
                Time.timeScale = 1f;
            }
            else if (GameUIActive == false)
            {
                GameUI.gameObject.SetActive(true);
                GameUIActive = true;
                Time.timeScale = 0f;
            }

        }


		
	}
}
