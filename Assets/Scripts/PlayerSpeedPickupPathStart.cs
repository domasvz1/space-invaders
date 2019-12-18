using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedPickupPathStart : MonoBehaviour {
    public Transform firstCheckpoint;
    public static Transform nextPickuptCheckpoint;

    void Start()
    {
        nextPickuptCheckpoint = firstCheckpoint;
    }
}
