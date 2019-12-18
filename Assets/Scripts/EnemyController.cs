using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    // This can be from gameController
    public float shootDelay = 1.0f, repeatRate;

    // this needs to be assigned in the Inspector window
    public GameObject bullet;
    public Transform bulletSpawn;
    public GameObject explosion;
  

    // Use this for initialization
    void Start () {
        const float lowerBound = 3.0f, higherBound = 6.0f;
        repeatRate = Random.Range(lowerBound, higherBound);
        shootDelay = Random.Range(lowerBound, higherBound);
        InvokeRepeating("Shoot", shootDelay, repeatRate);
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Implementing collision logic
        
        // If its a boundary or other enemy spaceship, we immediately return from the collapse method

        if (collider.tag == "Boundaries" || collider.tag == "Enemy")
        {
            return;
        }

        GameEventController gameController = GameObject.FindGameObjectWithTag("GameEvents").GetComponent<GameEventController>();

        // If Enemy Collide with player bullet we need to destroy the bullet
        if (collider.tag == "CustomPlayerBullet")
        {
            Destroy(collider.gameObject); // Destroy the Bullet object
            Destroy(gameObject); // Destroy the Enemy object itself
            Instantiate(explosion, transform.position, transform.rotation);
            gameController.score += 1;
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }

}
