using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTravel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y +0.05f, gameObject.transform.position.z);
    }
}
