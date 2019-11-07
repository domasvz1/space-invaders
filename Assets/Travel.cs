using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour {

    private readonly float travelSpeed = 25.0f;

	void Start ()
    {
        GetComponent<Rigidbody>().velocity = transform.up * travelSpeed;
    }
	
}
