using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCoroutines : MonoBehaviour
{
    public Light lightMenu;

    public GameObject model1;

    public GameObject model2;

    public GameObject model3;

    private void Awake()
    {
        StartCoroutine(KitchenFlash());
    }

    public IEnumerator KitchenFlash()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            lightMenu.enabled = !lightMenu.enabled;
            model2.SetActive(false);
            model1.SetActive(false);

            yield return new WaitForSeconds(.15f);
            lightMenu.enabled = !lightMenu.enabled;
            model2.SetActive(false);
            model1.SetActive(true);

            yield return new WaitForSeconds(1f);
            lightMenu.enabled = !lightMenu.enabled;
            model2.SetActive(false);
            model1.SetActive(true);

            yield return new WaitForSeconds(.15f);
            lightMenu.enabled = !lightMenu.enabled;
            model2.SetActive(true);
            model1.SetActive(false);
        }
    }
}
