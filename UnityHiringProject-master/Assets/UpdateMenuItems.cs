using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMenuItems : MonoBehaviour {

    // Add this to list and later laucn through loop
    public new Camera camera;
    public Transform transLogo;
    public Transform transintroText;
    public Transform transPlay;
    public Transform transHowTo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // During the update we get transform component and set its position in the middle of the screen
        Transform trans = GetComponent<Transform>();
        transLogo.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 18, camera.transform.position.z + 6);
        transintroText.transform.position = new Vector3(camera.transform.position.x + 3, camera.transform.position.y - 8, camera.transform.position.z + 6);
        // Last two objects oin the menu
        transPlay.transform.position = new Vector3(camera.transform.position.x - 10, camera.transform.position.y - 20, camera.transform.position.z + 6);
        transHowTo.transform.position = new Vector3(camera.transform.position.x + 10, camera.transform.position.y - 20, camera.transform.position.z + 6);
    }
}
