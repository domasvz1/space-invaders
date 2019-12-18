using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedPickupHandler : MonoBehaviour {

    public Transform[] checkPointArray;
    public static Transform[] checkpointA;
    public static int currentCheckpoint = 0;

    private void Start()
    {
        currentCheckpoint = 0;
    }

    private void Update()
    {
        checkpointA = checkPointArray;
    }
}