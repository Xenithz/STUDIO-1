using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleTest : MonoBehaviour
{
    public int candleCount = 0;

    public List<GameObject> test = new List<GameObject>();

    private void Update()
    {
        if(candleCount >= 5)
        {
            foreach(GameObject v in test)
            {
                v.SetActive(true);
            }
        }
    }
}
