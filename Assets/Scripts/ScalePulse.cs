using UnityEngine;
using System.Collections;

public class ScalePulse : MonoBehaviour {

    private float scale = 2.0f;
    private const float maxScale = 2.0f, minScale = 1.6f, pulsePause = 0.5f;

    public WaitForSeconds WaitForSeconds
    {
        get
        {
            return new WaitForSeconds(pulsePause);
        }
    }

    private IEnumerator Start()
    {
        // I'm using LogoScript in Menu and Instruction Scenes, so I play the continue playing music in both of them
        while (true)
        {
            if (scale == maxScale)
                scale = minScale;
            else if (scale == minScale)
                scale = maxScale;
            transform.localScale = new Vector3(scale, scale, scale);

            yield return WaitForSeconds;
        }
    }
}