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
    public GameObject explosion;
  

    // Use this for initialization
    void Start () {
        // We need to implement score system here and call the script or object where the data could be passed

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

        // If its player or a bullet shooting we need to istanciate the collision
        if (collider.tag == "Player" )
        {
            
            Instantiate(explosion, collider.transform.position, collider.transform.rotation);

            // Ending the game after collision
            Destroy(gameObject); // Destroy the Enemy object itself
            GameObject.FindGameObjectWithTag("GameEvents").GetComponent<GameEventController>().GameOver();
        }

        // If Enemy Collide with player bullet we need to destroy the bullet
        if (collider.tag == "CustomPlayerBullet")
        {
            Destroy(collider.gameObject); // Destroy the Bullet object
            Destroy(gameObject); // Destroy the Enemy object itself
        }


        // And the end we add the score, destroy Enemy object and the bullet
        // We add the score 

        //--> TO DO ADD THE SCORE
        // gameController.AddScore(scoreValue);


    }

    public void Shoot()
    {
        Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }

}
