using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSoundTimer : MonoBehaviour {

    public AudioSource Audio;

    // Use this for initialization
    private IEnumerator Start () {

        float fadeTime = 3.5f;
        float startVolume = Audio.volume;
        Audio.Play();

        while (Audio.volume > 0)
        {
            Audio.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        Audio.Stop();
    }
}
