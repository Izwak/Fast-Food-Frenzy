using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Solution found here: https://forum.unity.com/threads/fade-out-audio-source.335031/

public class FadeOutSound
{
    public static IEnumerator FadeOut(AudioSource audio, float timeToFade, float skipToTime = 0)
    {
        audio.Play();
        audio.time = skipToTime;
        float startVolume = audio.volume;
        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / timeToFade;
            yield return null;
        }
    }
}
