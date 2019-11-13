using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    // This can be from gameController
    public float shootDelay = 1.0f;
    public float repeatRate;

    // this needs to be assigned in inspector
    public GameObject Bullet;
    public Transform BulletSpawn;
    public GameObject explosion;
  

    // Use this for initialization
    void Start () {
        // No the delay and shooting rate are generated randomly 
        repeatRate = Random.Range(3.0f, 6.0f);
        shootDelay = Random.Range(3.0f, 6.0f);
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
        Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }

}
