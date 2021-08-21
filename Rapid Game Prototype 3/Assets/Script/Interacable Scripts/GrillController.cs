using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject emptySlot;
    public GameObject pattyParticles;
    GameObject clone;

    AudioSource audioController;
    AudioClip sizzle;
    bool playingAudio = false;
    bool fading = false;
    float startVolume;

    // Start is called before the first frame update
    void Start()
    {
        clone = Instantiate(pattyParticles, emptySlot.transform.position, Quaternion.identity);
        clone.transform.SetParent(this.transform);
        clone.transform.Rotate(new Vector3(-90, 0, 0));
        clone.SetActive(false);
        audioController = GetComponent<AudioSource>();
        startVolume = audioController.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (emptySlot.transform.childCount > 0)
        {
            GameObject paddiies = emptySlot.transform.GetChild(0).gameObject;


            if (paddiies != null)
            {
                clone.SetActive(true);
                if (!playingAudio)
                {
                    audioController.volume = startVolume;
                    audioController.Play();
                    playingAudio = true;
                }
                if(paddiies.tag == "Fire Paddies") {
                    GetComponent<FireParticleController>().onfire = true;
                }

                Cooking cooking = paddiies.GetComponent<Cooking>();

                if (cooking != null && !paddiies.CompareTag("Raw Paddies"))
                {
                    cooking.duration = gameManager.burnTime;
                }
            }
        }
        if (emptySlot.transform.childCount == 0 && playingAudio == true)
        {
            clone.SetActive(false);
            fading = true;
            if (fading)
            {
                audioController.volume -= startVolume * Time.deltaTime * 2;
                if (audioController.volume <= 0)
                {
                    audioController.Stop();
                    fading = false;
                    playingAudio = false;
                    GetComponent<FireParticleController>().onfire = false;
                }
            }
            //audioController.Stop();
            //StartCoroutine(FadeOutSound.FadeOut(audioController, 1, transform.position, audioController.time));
            //playingAudio = false;
        }
    }
}
