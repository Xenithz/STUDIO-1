using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHand : MonoBehaviour
{
    /*
        The MonoHand class will be used to access the item's current behaviors. The hand will first add all the items
        in the scene to a list of gameobjects by the tag. It will then sort the list according to the distance from the player
        to the item.  The first index of the list will be the targetted item. After the player gets within a certain distance of the item
        and is facing the item, the player can use it by using the left click
   */

    //targetTransform stores the transform of the item that the hand is currently targetting
    public Transform targetTransform;

    //directionFromPlayerToItem stores the direction to the item
    public Vector3 directionFromPlayerToItem;

    //angle will store between the direction and the forward direction of the transform of the player
    public float angle;

    //distanceFromPlayerToItem will store the distance from the player to the item as a flaot
    public float distanceFromPlayerToItem;

    //itemList will store all the items inside the game scene
    public List<GameObject> itemList;

    private void Awake()
    {
        //On awake instantiate the itemList as a list of GameObjects
        itemList = new List<GameObject>();

        //Add all the GameObjects with the tag item to the itemList
        itemList.AddRange(GameObject.FindGameObjectsWithTag("item"));
    }

    private void Update()
    {
        //Pass in a custom sort function as the argument for the list sort
        itemList.Sort(delegate (GameObject a, GameObject b)
        {
            //Compare the distance of object A (relative to the transform of the hand) to the distance of object b (relative to the distance of the transform of the hand)
            return Vector3.Distance(this.transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(this.transform.position, b.transform.position));
        });

        //Set the targetTransform to the transform of the first item in the list
        targetTransform = itemList[0].transform;

        //Get the direction from the player to the item by normalizing the magnitude
        directionFromPlayerToItem = Vector3.Normalize(targetTransform.position - transform.position);
        
        //Get the distance from the player to the item 
        distanceFromPlayerToItem = Vector3.Distance(targetTransform.transform.position, transform.position);

        //Get the angle between the directionToTheItem and the forward direction of the transform
        angle = Vector3.Angle(directionFromPlayerToItem, transform.forward);

        //Debug for line
        Debug.DrawLine(transform.position, transform.position + directionFromPlayerToItem * 5, Color.red, 0.5f);

        //Check if the distance is smaller than 4, and the angle is smaller than 40
        if(distanceFromPlayerToItem < 4f && angle <= 40f)
        {
            //CHeck if the player presses the left click
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.I))
            {
                //Call the currentBehavior of the item
                targetTransform.gameObject.GetComponent<MonoItem>().CurrentBehavior();
            }
        }
    }
}
