using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickupCheckpoint : MonoBehaviour {

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("CustomEnemyBullet") || collision.CompareTag("CustomPlayerBullet"))
        {
            return;
        }

        if (transform == ShieldPickupHandler.checkpointA[ShieldPickupHandler.currentCheckpoint].transform)
        {
            //Check so we dont exceed our checkpoint quantity
            if (ShieldPickupHandler.currentCheckpoint + 1 < ShieldPickupHandler.checkpointA.Length)
            {
                ShieldPickupHandler.currentCheckpoint++;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                ShieldPickupHandler.currentCheckpoint = 0;
            }

            // Everytime we make sure that checkpints change too
            ShieldPickupPathStart.nextPickuptCheckpoint = ShieldPickupHandler.checkpointA[ShieldPickupHandler.currentCheckpoint];

        }
    }
}
