using UnityEngine;
using System.Collections;

public class PlayerSpeedPickupMovement : MonoBehaviour
{
    public float inicialSpeed;
    public float turningSpeed;
    private Transform target;

    void FixedUpdate()
    {
        target = PlayerSpeedPickupPathStart.nextPickuptCheckpoint; // Marking the target, a.k.a the next checkpoint

        if (PlayerSpeedPickupPathStart.nextPickuptCheckpoint != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = -1 * Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turningSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.position, inicialSpeed * Time.deltaTime);
        }
    }
}