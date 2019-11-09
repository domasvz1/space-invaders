using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldCheckPoint : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If its not game object with the tag "Taxi" then do nothing
        if (!collision.CompareTag("Shield"))
            return;

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
