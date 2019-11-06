using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    // This can be from gameController
    public float shootDelay = 2.0f;
    public float repeatRate = 2.0f;

    // this needs to be assigned in inspector
    public GameObject Bullet;
    public Transform BulletSpawn;

	// Use this for initialization
	void Start () {
        // This can be called from th GameController
        InvokeRepeating("Shoot", shootDelay, repeatRate);
	}
	
	public void Shoot()
    {
        Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }

}
