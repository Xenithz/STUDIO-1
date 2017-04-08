using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public Light light;

    private void Awake()
    {
        StartCoroutine(KitchenFlash());
    }

    public IEnumerator KitchenFlash()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            light.enabled = !light.enabled;

            yield return new WaitForSeconds(.15f);
            light.enabled = !light.enabled;
        }
    }
}
