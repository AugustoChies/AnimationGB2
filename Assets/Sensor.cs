using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
    public bool isInside;    
    public string myTrackedTag;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == myTrackedTag)
        {
            isInside = true;            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == myTrackedTag)
        {
            isInside = false;
        }
    }
}
