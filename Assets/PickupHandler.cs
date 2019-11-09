using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour {

    // These Static Variables are accessed in "checkpoint" Script
    public Transform[] checkPointArray;
    public static Transform[] checkpointA;
    public static int currentCheckpoint = 0;
    public Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        currentCheckpoint = 0;
    }

    private void Update()
    {
        checkpointA = checkPointArray;
    }
}