using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedPickupPathStart : MonoBehaviour {

    public Transform firstCheckpoint;
    public static Transform nextPickuptCheckpoint;

    void Start()
    {
        nextPickuptCheckpoint = firstCheckpoint;
    }
}
