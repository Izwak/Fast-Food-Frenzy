using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }


    }

    public Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Bro watch out: " + name + " isnt a real sound");
            return null;
        }

        return sound;
    }

    public void Play(string name)
    {
        Sound sound = GetSound(name);

        if (sound != null)
            sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = GetSound(name);

        if (sound != null)
            sound.source.Stop();
    }

    public bool IsPlaying(string name)
    {
        Sound sound = GetSound(name);

        if (sound != null)
            return sound.source.isPlaying;

        Debug.LogWarning("Return False because audio no exist");
        return false;
    }

    public void FadeOut(string name, float time)
    {
        Sound sound = GetSound(name);


        if (sound != null)
        {
            StartCoroutine(FadeToEnumerator(sound, sound.source.volume, 0, time));
        }
        else
        {
            Debug.LogWarning("No fade out as there was no audio to fade");
        }
    }

    public void FadeIn(string name, float time)
    {
        Sound sound = GetSound(name);

        // Make sure the audio is playing to fade in
        if (!sound.source.isPlaying)
            sound.source.Play();

        if (sound != null)
        {
            StartCoroutine(FadeToEnumerator(sound, 0, sound.source.volume, time));
        }
        else
        {
            Debug.LogWarning("No fade out as there was no audio to fade");
        }
    }

    public IEnumerator FadeToEnumerator(Sound sound, float startVolume, float endVolume, float fadeTime)
    {
        float timeElapsed = 0;

        while (timeElapsed < fadeTime)
        {
            //print("I want 2 c how long it takes");
            sound.source.volume = Mathf.Lerp(startVolume, endVolume, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (endVolume == 0)
            sound.source.Stop();

        //print("How many times does it reach here");
    }
}
