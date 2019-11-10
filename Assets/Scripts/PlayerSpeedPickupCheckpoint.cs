using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for a single checkpoint point, where a certain pickup travels
public class PlayerSpeedPickupCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        // If anything comes here besides PlayerSpeedPickup collider we ignore it
        if (!collider.CompareTag("PlayerSpeedPickup"))
        { 
            return;
        }

        if (transform == PlayerSpeedPickupHandler.checkpointA[PlayerSpeedPickupHandler.currentCheckpoint].transform)
        {
            //Check so we dont exceed our checkpoint quantity
            if (PlayerSpeedPickupHandler.currentCheckpoint + 1 < PlayerSpeedPickupHandler.checkpointA.Length)
            {
                PlayerSpeedPickupHandler.currentCheckpoint++;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                PlayerSpeedPickupHandler.currentCheckpoint = 0;
            }

            // Everytime we make sure that checkpints change too
            PlayerSpeedPickupPathStart.nextPickuptCheckpoint = PlayerSpeedPickupHandler.checkpointA[PlayerSpeedPickupHandler.currentCheckpoint];
        }
    }
}
