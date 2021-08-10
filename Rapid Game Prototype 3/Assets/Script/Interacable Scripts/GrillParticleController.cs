using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillParticleController : MonoBehaviour
{
    public GameObject emptySlot;
    public GameObject pattyParticles;
    GameObject clone;

    AudioSource audioController;
    AudioClip sizzle;
    bool playingAudio = false;

    // Start is called before the first frame update
    void Start()
    {
        clone = Instantiate(pattyParticles, emptySlot.transform.position, Quaternion.identity);
        clone.transform.SetParent(this.transform);
        clone.transform.Rotate(new Vector3(-90, 0, 0));
        clone.SetActive(false);
        audioController = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (emptySlot.transform.childCount > 0)
        {
            if (emptySlot.transform.GetChild(0) != null)
            {
                clone.SetActive(true);
                if (!playingAudio)
                {
                    audioController.Play();
                    playingAudio = true;
                }
            }
        }
        if (emptySlot.transform.childCount <= 0)
        {
            clone.SetActive(false);
            audioController.Stop();
            playingAudio = false;
        }
    }
}
