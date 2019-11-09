using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldCheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("BulletInc"))
        {
            return;
        }

        Debug.Log(transform);
        if (transform == PickupHandler.checkpointA[PickupHandler.currentCheckpoint].transform)
        {
            //Check so we dont exceed our checkpoint quantity
            if (PickupHandler.currentCheckpoint + 1 < PickupHandler.checkpointA.Length)
            {
                PickupHandler.currentCheckpoint++;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                PickupHandler.currentCheckpoint = 0;
            }

            // Everytime we make sure that checkpints change too
            PickupTravelPath.nextPickuptCheckpoint = PickupHandler.checkpointA[PickupHandler.currentCheckpoint];

        }
    }
}
