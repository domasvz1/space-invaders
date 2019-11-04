using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speedForce = 20f, torqueForce = -200f, driftChanceSticky = 0.9f, driftChanceSlippy = 1f, maxStickyVelocity = 2.5f;
    public AudioSource gassPedalSound;
    public GameObject ExhaustParticle;
    private PlayerController spaceShip;
    bool gotOutOfBounds = false,  finishedShrinking = false, slowDownThePlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        //If a player goes on trigger and sees the tag out of bounds, he just went out of bounds
        if (collision.CompareTag("OutsideBounds") )
            gotOutOfBounds = true;

        else if (collision.CompareTag("Slerp") )
            slowDownThePlayer = true;
            */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("OutsideBounds"))
            gotOutOfBounds = false;
        else if (collision.CompareTag("Slerp"))
            slowDownThePlayer = false;
    }

    // Use this for initialization
    private void Start()
    {
        spaceShip = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        Rigidbody2D player = GetComponent<Rigidbody2D>();
        float driftFactor = driftChanceSticky;

        if (RightVelocity().magnitude > maxStickyVelocity)
            driftFactor = driftChanceSlippy;

        player.velocity = ForwardVelocity() + RightVelocity() * driftFactor;

        if (gotOutOfBounds)
        {
            if (!finishedShrinking)
                StartCoroutine(Scale(player));

            if (finishedShrinking)
            {
                player.angularVelocity = 0;
                player.MovePosition(PlayerHandler.checkpointA[PlayerHandler.currentCheckpoint].position);
                finishedShrinking = false;
                gotOutOfBounds = false;
            }  
        }

        // The main spaceship moevement
        else if(!gotOutOfBounds)
        {
            // Everytime when we're not out of bounds,we make sure we dont shrink and we dont do the downsize
            finishedShrinking = false;

            // the not shrunk version of shpaship is 0.2  0.2 0.2
            player.transform.localScale = new Vector3(1,1,1);
            // The Accelaration the the car will be added, evey time i hold the button
            if (Input.GetButton("Horizontal"))
            {
                if (slowDownThePlayer) // If there's a collision to asteroid
                {
                    Instantiate(ExhaustParticle, spaceShip.transform.position, spaceShip.transform.rotation);
                    player.AddForce(transform.up * speedForce/3);
                }
                else
                {
                    Instantiate(ExhaustParticle, spaceShip.transform.position, spaceShip.transform.rotation);
                    player.AddForce(transform.up * speedForce);
                }
            }

            

            // The BEEP sound will online display when I Press the button
            if (Input.GetKeyDown(KeyCode.Z))
                PlayEngineSound(); // DO BEEP BEEP IN THE FUTURE

            float tf = Mathf.Lerp(0, torqueForce, player.velocity.magnitude / 2);
            player.angularVelocity = Input.GetAxis("Horizontal") * tf;
        }
    }

    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    void PlayEngineSound()
    {
        gassPedalSound.Play();
    }

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    private IEnumerator Scale(Rigidbody2D player)
    {
        float scaling = 0.01f;
        player.angularVelocity += 20;

        while (0 < transform.localScale.x)
        {
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime * scaling;
            yield return null;
        }
        finishedShrinking = true;
    }
}
