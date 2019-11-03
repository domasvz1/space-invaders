using UnityEngine;
using System.Collections;

public class LogoScript : MonoBehaviour {

    private float scale = 2.3f;

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
            if (scale == 2.3f)
                scale = 1.7f;
            else if (scale == 1.7f)
                scale = 2.3f;
            transform.localScale = new Vector3(scale, scale, scale);

            yield return WaitForSeconds;
        }
    }
}
