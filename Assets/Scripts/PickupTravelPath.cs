using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickupTravelPath : MonoBehaviour
{
    public Transform firstCheckpoint;
    public static Transform nextPickuptCheckpoint;

    void Start()
    {
        nextPickuptCheckpoint = firstCheckpoint;
    }
}