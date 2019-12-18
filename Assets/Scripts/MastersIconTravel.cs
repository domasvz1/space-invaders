using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastersIconTravel : MonoBehaviour
{ 

    WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
    float IVelocity = 20;
    const float lowerPulsingBound = 1.2f, higherPulsingBound = 1.3f;
    private float objectsScale = lowerPulsingBound; // Start by assigning the lower scale value

    IEnumerator Start()
    {
        // Icon Transformation needs to start only when player sees it
        while (true)
        {
            if (transform.position.x < 2.1 && transform.position.y < 0.36)
            {
                if (objectsScale.Equals(higherPulsingBound))
                    objectsScale = lowerPulsingBound;
                else if (objectsScale.Equals(lowerPulsingBound))
                    objectsScale = higherPulsingBound;

                if (IVelocity == 20)
                    IVelocity = -20;
                else if (IVelocity == -20)
                    IVelocity = 20;

                transform.localScale = new Vector2(objectsScale, objectsScale);
            }
            yield return waitForSeconds;
        }
    }
}
