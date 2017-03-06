using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	
	void Update ()
    {
		if(GameManager.GameManagerInstance.isPlayerAlive == false)
        {
            SceneManager.LoadScene("GameOver");
        }
	}
}
