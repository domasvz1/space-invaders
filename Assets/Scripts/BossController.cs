using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    // This can be from gameController
    public float shootDelay = 1.0f;
    public float repeatRate;
    public int health = 15;

    // this needs to be assigned in inspector
    public GameObject Bullet;
    public Transform BulletSpawn;
    public GameObject bossExplosion;


    // Use this for initialization
    void Start()
    {
        const float lowerBound = 3.0f, higherBound = 6.0f;
        // Delay beetween the shooting rate are generated randomly
        // --- In this way I simple make that the generated spacships would shoot at different times and rates
        repeatRate = Random.Range(lowerBound, higherBound);
        shootDelay = Random.Range(lowerBound, higherBound);
        InvokeRepeating("Shoot", shootDelay, repeatRate);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Boundaries"))
        {
            return;
        }

        GameEventController gameController = GameObject.FindGameObjectWithTag("GameEvents").GetComponent<GameEventController>();

        // If Enemy Collide with player bullet we need to destroy the bullet
        if (collider.tag.Equals("CustomPlayerBullet"))
        {
            if(health > 1)
            {
                health -= 1;
            }
            else // If its bosses last health, a winning condition will be set to a player
            {
                Destroy(gameObject); // Destroy the Enemy object itself
                Instantiate(bossExplosion, transform.position, transform.rotation);
                gameController.score += 20; // add 20 points for killing boss
                gameController.Winning();
            }
            Destroy(collider.gameObject); // Destroy the Bullet object
        }
    }

    public void Shoot()
    {
        Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }
}
