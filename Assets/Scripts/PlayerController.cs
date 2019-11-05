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

    private readonly float shootingIntensity = 0.4f;
    private float nextBulletTime;


    public void Start()
    {
        Rigidbody player = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        /*
        //If a player goes on trigger and sees the tag out of bounds, he just went out of bounds
        if (collision.CompareTag("OutsideBounds") )
            gotOutOfBounds = true;

        else if (collision.CompareTag("Slerp") )
            slowDownThePlayer = true;
            */
    }

    private void OnTriggerExit(Collider collision)
    { 
        // For now no more triggers
        /*
        if (collision.CompareTag("OutsideBounds"))
            gotOutOfBounds = false;
        else if (collision.CompareTag("Slerp"))
            slowDownThePlayer = false;
            */
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

        // TO DO other controls here
        // Input Pause option
        // Input restart option
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

    /*
    void PlayEngineSound()
    {
        shootingSound.Play();
    }
    */

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
