﻿using System.Collections;
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

    private void Update()
    {
        
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
            Instantiate(explosion, transform.position, transform.rotation);
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
