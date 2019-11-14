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
    public Boundaries Boundaries;
    public Transform ShotSpawnCoords;
    public GameObject BulletShell, explosion, pickupExplosion;
    public AudioSource ShootingSound;
    private GameEventController gameEventController;
    private ParticleSystem.MainModule main;

    private float shootingIntensity = 0.6f;
    private float nextBulletTime, playerSpaceshipSpeed = 4.0f;
    public int timeLeftForShootingBoost = 10, timeLeftForSpeedBoost = 10, playersHealth = 1;

    public bool hasShield = false, hasSpeedBoost = false, hasShootingBoost = false, blinking = true;

    public const string speedPickupTag = "PlayerSpeedPickup", shootingPicktupTag = "ShootingSpeedPickup", shieldPickupTag = "ShieldPickup";

    public void Start()
    {
        main = pickupExplosion.GetComponent<ParticleSystem>().main;
        gameEventController = GameObject.FindGameObjectWithTag("GameEvents").GetComponent<GameEventController>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        // If a Player collides with enemy or enemy bullet spaceship,  the logic remains the same
        if (collider.tag == "Enemy" || collider.tag == "CustomEnemyBullet")
        {
            // It a player has a full shield he loses one health and nothing happens to him
            if (playersHealth > 2)
            {
                playersHealth -= 1;
                // TO DO: Shield lose partical explosion
                Destroy(collider.gameObject);
            }
            // If he loses another health, his shield stops working
            else if (playersHealth == 2)
            {
                playersHealth -= 1;
                StopCoroutine("PulsingIcon");
                hasShield = false; // Setting health icon to false
                Destroy(collider.gameObject); // Destroy enemy bullet or spaceship
                gameEventController.shieldImage.SetActive(false);
                // TO DO LOSE LAST SHIELD
            }
            // If the player loses his last health
            else
            {
                Destroy(gameObject);
                Destroy(collider.gameObject); // Destroy enemy bullet or spaceship
                Instantiate(explosion, collider.transform.position, collider.transform.rotation);
                gameEventController.GameOver();
            }
        }

        switch (collider.tag)
        {
            case speedPickupTag:
                if (!hasSpeedBoost)
                {
                    gameEventController.playerspeedImage.SetActive(true);
                    Destroy(collider.gameObject);
                    hasSpeedBoost = true;
                    main.startColor = Color.red;
                    Instantiate(pickupExplosion, collider.transform.position, collider.transform.rotation);
                }
                break;
            case shootingPicktupTag:
                if (!hasShootingBoost)
                {
                    gameEventController.shootingspeedImage.SetActive(true);
                    Destroy(collider.gameObject);
                    hasShootingBoost = true;
                    main.startColor = Color.yellow;
                    Instantiate(pickupExplosion, collider.transform.position, collider.transform.rotation);
                }
                break;
            case shieldPickupTag:
                if (!hasShield)
                {
                    gameEventController.shieldImage.SetActive(true);
                    Destroy(collider.gameObject);
                    hasShield = true;
                    main.startColor = Color.green;
                    Instantiate(pickupExplosion, collider.transform.position, collider.transform.rotation);
                }
                break;
            default:
                break;
        }
    }

    public void Update()
    {
        // When you shoot, it checks if you can shoot another bullet and plays the sound of the shot
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextBulletTime)
        {
            nextBulletTime = Time.time + shootingIntensity;
            Instantiate(BulletShell, ShotSpawnCoords.position, ShotSpawnCoords.rotation);
            ShootingSound.Play();
        }

        // Use Shooting Boost
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (hasShootingBoost)
            {
                StartCoroutine("UseShootingBoost");
                StartCoroutine(PulsingIcon(gameEventController.shootingspeedImage));
            }
        }

        // When the time for Player Shooting Boost is over, we shut down Coruotine and reset variables
        if (timeLeftForShootingBoost <=  0)
        {
            StopCoroutine("UseShootingBoost");
            shootingIntensity = 0.6f;
            hasShootingBoost = false;
            timeLeftForShootingBoost = 10;
            StopCoroutine("PulsingIcon");
            gameEventController.shootingspeedImage.SetActive(false);
        }

        // Using Players Ship Speed Boost
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (hasSpeedBoost)
            {
                StartCoroutine("UseSpeedBoost");
                StartCoroutine(PulsingIcon(gameEventController.playerspeedImage));
            }
        }

        // When the time for Player Speed Boost is over, we shut down Coruotine and reset variables
        if (timeLeftForSpeedBoost <= 0)
        {
            StopCoroutine("UseSpeedBoost");
            // Reset Player Spaceship speed
            playerSpaceshipSpeed = 4.0f;
            hasSpeedBoost = false;
            timeLeftForSpeedBoost = 10;
            StopCoroutine("PulsingIcon");
            gameEventController.playerspeedImage.SetActive(false);
        }

        // Using shield
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(playersHealth == 1)
            {
                StartCoroutine(PulsingIcon(gameEventController.shieldImage));
                playersHealth += 2;
            }
        }
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
        if (playerSpaceshipSpeed == 0)
            playerSpaceshipSpeed = 10;

        player.velocity = spaceshipPosition * playerSpaceshipSpeed;


        // For now x,y min boundaries are:
        // X  "min -6.45f" and "max 1.6f"
        // Y  "min 1.29f" and "max 7"
        // This can be tweaked into method that would catch camera position and determine boundaries for object
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

 
    private IEnumerator UseShootingBoost()
    {
        shootingIntensity = 0.2f;
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeftForShootingBoost -= 1;
        }
    }

    private IEnumerator UseSpeedBoost()
    {
        playerSpaceshipSpeed = 12.0f;
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeftForSpeedBoost -= 1;
        }
    }

    private IEnumerator PulsingIcon(GameObject goToPulse)
    {
        float scale = 1.0f;
        // I'm using LogoScript in Menu and Instruction Scenes, so I play the continue playing music in both of them
        while (true)
        {
            if (scale == 1.0f)
                scale = 0.5f;
            else if (scale == 0.5f)
                scale = 1.0f;
            goToPulse.transform.localScale
                = new Vector3(scale, scale, scale);

            yield return new WaitForSeconds(0.7f);
        }
    }
}
