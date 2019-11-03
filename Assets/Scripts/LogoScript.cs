using UnityEngine;
using System.Collections;

public class LogoScript : MonoBehaviour {

    private float scale = 2.0f;

    public WaitForSeconds WaitForSeconds
    {
        get
        {
            return new WaitForSeconds(0.6f);
        }
    }

    private IEnumerator Start()
    { 
        // The second we start the menu script we start the menu music
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().PlayMusic();
        
        // I'm using LogoScript in Menu and Instruction Scenes, so I play the continue playing music in both of them
        while (true)
        {
            if (scale == 2.0f)
                scale = 1.6f;
            else if (scale == 1.6f)
                scale = 2.0f;
            transform.localScale = new Vector3(scale, scale, scale);

            yield return WaitForSeconds;
        }
    }
}
