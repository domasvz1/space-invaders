using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct Boundaries
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
    //public AudioSource shootingSound;
    //public GameObject ExhaustParticle;
    public Boundaries Boundaries;
    public float spaceshipSpeed;

    public Transform ShotSpawnCoords;
    public GameObject BulletShell;
    public AudioSource ShootingSound;
    public GameObject explosion;

    private readonly float shootingIntensity = 0.4f;
    private float nextBulletTime;

    public void Start()
    {
        //Rigidbody player = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        // If a Player collides with enemies bullet we need to destroy the bullet and the player
        if (collider.tag == "CustomEnemyBullet")
        {
            Destroy(gameObject);
            Instantiate(explosion, collider.transform.position, collider.transform.rotation);
            GameObject.FindGameObjectWithTag("GameEvents").GetComponent<GameEventController>().GameOver();
        }

        // If a Player collides with enemy spaceship, it would make sence for player to be destroyed
        if (collider.tag == "Enemy")
        {
            //Destroy both Player and Enemy Spaceships
            Destroy(gameObject);
            Destroy(collider.gameObject);
            Instantiate(explosion, collider.transform.position, collider.transform.rotation);
            GameObject.FindGameObjectWithTag("GameEvents").GetComponent<GameEventController>().GameOver();
        }

        // Implement Pickups collison here
        // If a Player collides with enemies bullet we need to destroy the bullet and the player
        if (collider.tag == "Shield")
        {


        }

        if (collider.tag == "BulletInc")
        {

        }


    }

    private void Update()
    {
        // When you shoot, it checks if you can shoot another bullet and plays the sound of the shot
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextBulletTime)
        {
            nextBulletTime = Time.time + shootingIntensity;
            Instantiate(BulletShell, ShotSpawnCoords.position, ShotSpawnCoords.rotation);
            ShootingSound.Play();
        }

        // TO DO Insert Pause option here
    }

    private void FixedUpdate()
    {
        // Getting Player's Rigidbody component

        Rigidbody player = GetComponent<Rigidbody>();
        // Need write the moving logic here
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");


        // Get Rigidbody component and chanhe its velocity to vector coords multiplied by speed var
        Vector3 spaceshipPosition = new Vector3(horizontalMovement, verticalMovement, 0.0f);


        // If someone forgets to set speed in inspector or it resets, this will be changed to default:
        if (spaceshipSpeed == 0)
            spaceshipSpeed = 10;

        player.velocity = spaceshipPosition * spaceshipSpeed;

        player.transform.position = new Vector3(
            Mathf.Clamp(player.position.x, Boundaries.xMin, Boundaries.xMax),
            Mathf.Clamp(player.position.y, Boundaries.yMin, Boundaries.yMax),
            player.transform.position.z);
        
        player.rotation = Quaternion.Euler(0.0f, 0.0f, player.velocity.x * -1);


    }


    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    private IEnumerator Scale(Rigidbody player)
    {
        /*
        float scaling = 0.01f;
        player.angularVelocity.z += 20;

        while (0 < transform.localScale.x)
        {
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime * scaling;
            yield return null;
        }
        finishedShrinking = true;
        */
        yield return null;
    }
}
