using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMenuItems : MonoBehaviour {

    // Add this to list and later laucn through loop
    public new Camera camera;
    public Transform transformPlay;
    public Transform transformBestScores;
    public Transform transformInstructions;
    public Transform transformQuit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // During the update we get transform component and set its position in the middle of the screen
        transformPlay.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 6);
        transformBestScores.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y-6, camera.transform.position.z + 6);
        transformInstructions.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y -12, camera.transform.position.z + 6);
        transformQuit.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y -18, camera.transform.position.z + 6);
    }
}
