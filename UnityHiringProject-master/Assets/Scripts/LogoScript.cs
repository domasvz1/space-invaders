using UnityEngine;
using System.Collections;

public class LogoScript : MonoBehaviour {

    float scale = 2.3f;
    readonly WaitForSeconds waitForSeconds = new WaitForSeconds(0.6f);

    
    IEnumerator Start()
    { 
        // The second we start the menu script we start the menu music
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().PlayMusic();

        // I'm using LogoScript in Menu and HowToPlay Scenes, so I play the continue playing music in both of them
        while (true)
        {
            if (scale == 2.3f)
                scale = 1.7f;
            else if (scale == 1.7f)
                scale = 2.3f;
            transform.localScale = new Vector2(scale, scale);

            yield return waitForSeconds;
        }
    }
}
