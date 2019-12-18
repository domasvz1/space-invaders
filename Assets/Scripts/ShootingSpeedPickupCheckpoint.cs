using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedPickupCheckpoint : MonoBehaviour {

    private void OnTriggerEnter(Collider collider)
    {
        // If anything comes here besides ShootingSpeedPickup collider we ignore it
        if (!collider.CompareTag("ShootingSpeedPickup") )
        {
            return;
        }

        if (transform == ShootingSpeedPickupHandler.checkpointA[ShootingSpeedPickupHandler.currentCheckpoint].transform)
        {
            //Check so we dont exceed our checkpoint quantity
            if (ShootingSpeedPickupHandler.currentCheckpoint + 1 < ShootingSpeedPickupHandler.checkpointA.Length)
            {
                ShootingSpeedPickupHandler.currentCheckpoint++;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                ShootingSpeedPickupHandler.currentCheckpoint = 0;
            }

            // Everytime we make sure that checkpints change too
            ShootingSpeedPickupPathStart.nextPickuptCheckpoint = ShootingSpeedPickupHandler.checkpointA[ShootingSpeedPickupHandler.currentCheckpoint];

        }
    }
}
