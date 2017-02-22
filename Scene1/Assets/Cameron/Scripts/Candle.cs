using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    public bool CandleCanStart;
    public Transform Flame1;
    public bool IsCandleOn;



    void OnTriggerStay(Collider other)
    {
        Flame1 = transform.FindChild("Flame");
        if (other.tag == "Player")
        {
            CandleCanStart = true;
        }

        else

        {
            CandleCanStart = false;
        }

        if (CandleCanStart == true)
        {
            if (IsCandleOn == true)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Flame1.gameObject.SetActive(false);
                    IsCandleOn = false;
                }
            }

            if (IsCandleOn == false)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Flame1.gameObject.SetActive(true);
                    IsCandleOn = true;
                }
            }
        }
    }
}