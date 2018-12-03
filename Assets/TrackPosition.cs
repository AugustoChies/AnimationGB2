using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour {
    public GameObject tracked;
    public bool pos, rot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (pos)
            this.transform.position = tracked.transform.position;
        if (rot)
            this.transform.eulerAngles = new Vector3(0,tracked.transform.rotation.eulerAngles.y,0);
    }
}
