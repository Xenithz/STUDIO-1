using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private GameManager()
    {

    }

    private static GameManager gameManagerInstance;

    public static GameManager GameManagerInstance
    {
        get
        {
            if (gameManagerInstance == null)
            {
                gameManagerInstance = new GameManager();
            }

            return gameManagerInstance;
        }
    }


}
