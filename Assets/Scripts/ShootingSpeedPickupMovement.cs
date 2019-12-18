﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedPickupMovement : MonoBehaviour {
    public float inicialSpeed;
    public float turningSpeed;
    private Transform target;

    void FixedUpdate()
    {
        target = ShootingSpeedPickupPathStart.nextPickuptCheckpoint; // Marking the target, a.k.a the next checkpoint

        if (ShootingSpeedPickupPathStart.nextPickuptCheckpoint != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = -1 * Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turningSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.position, inicialSpeed * Time.deltaTime);
        }
    }
}