﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    private void OnTriggerExit(Collider collider)
    {
        Destroy(collider.gameObject);
    }
}
