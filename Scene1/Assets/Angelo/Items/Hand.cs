using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Camera mainCamera;

    private bool targettingAnItem;

    private float distanceToItem;

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Vector3 rayStartingLocation = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit rayHit;

            if (Physics.Raycast(rayStartingLocation, mainCamera.transform.forward, out rayHit))
            {
                if (rayHit.transform.tag == "item")
                {
                    targettingAnItem = true;
                    distanceToItem = Vector3.Distance(this.gameObject.transform.position, rayHit.transform.position);
                    Item temporaryItem;
                }
                else
                {
                    targettingAnItem = false;
                }

                if (targettingAnItem == true && distanceToItem <= 3f)
                {
                    if (Input.GetMouseButton(1))
                    {

                    }
                }
            }
        }
    }
}
