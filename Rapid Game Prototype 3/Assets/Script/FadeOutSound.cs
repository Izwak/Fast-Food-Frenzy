using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Solution found here: https://forum.unity.com/threads/fade-out-audio-source.335031/
//NOTE: THIS CODE IS CURRENTLY UNUSED

public class FadeOutSound
{
    public static IEnumerator FadeOut(AudioSource audio, float timeToFade, Vector3 worldPos, float skipToTime = 0)
    {
        audio.time = skipToTime;
        //AudioClip clip = audio.clip;
        //AudioSource.PlayClipAtPoint(clip, worldPos);
        audio.Play();
        float startVolume = audio.volume;
        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / timeToFade;
            yield return null;
        }
        audio.Stop();
    }
}
