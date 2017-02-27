using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    public bool CandleCanStart;
    public Transform TargetFlame;
    public bool IsCandleOn;



    void OnTriggerStay(Collider other)
    {
        TargetFlame = transform.FindChild("Flame");
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
                    TargetFlame.gameObject.SetActive(false);
                    IsCandleOn = false;
                }
            }

            if (IsCandleOn == false)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    TargetFlame.gameObject.SetActive(true);
                    IsCandleOn = true;
                }
            }
        }
    }
}