using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterantionScript : MonoBehaviour {
    public Transform[] spots;
    public GameObject target, character;
		
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            target.transform.position = spots[0].position;
            character.GetComponent<Unit>().RequestPath();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            target.transform.position = spots[1].position;
            character.GetComponent<Unit>().RequestPath();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            target.transform.position = spots[2].position;
            character.GetComponent<Unit>().RequestPath();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            target.transform.position = spots[3].position;
            character.GetComponent<Unit>().RequestPath();
        }
    }
}
