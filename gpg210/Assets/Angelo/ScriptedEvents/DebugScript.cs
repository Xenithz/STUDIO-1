using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public List<GameObject> test = new List<GameObject>();

    private void Awake()
    {
        test[0].GetComponent<MonoDoor>().CurrentBehavior();
        test[1].GetComponent<MonoDoor>().CurrentBehavior();
        test[2].GetComponent<MonoDoor>().CurrentBehavior();
        test[3].GetComponent<MonoDoor>().CurrentBehavior();
        Destroy(this.gameObject);
    }
}
