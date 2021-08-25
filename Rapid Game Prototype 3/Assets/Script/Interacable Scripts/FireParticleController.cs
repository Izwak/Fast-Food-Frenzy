using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticleController : MonoBehaviour
{
    public GameObject emptySlot;
    public GameObject fireParticles;
    GameObject clone;

    AudioSource audioController;
    AudioClip sizzle;
    bool playingAudio = false;

    public bool onfire = false;
    bool wasonfire = false;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        clone = Instantiate(fireParticles, emptySlot.transform.position, Quaternion.identity);
        clone.transform.SetParent(this.transform);
        clone.transform.Rotate(new Vector3(-90, 0, 0));
        clone.SetActive(false);
        audioController = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onfire) {
            timer += Time.deltaTime;
            if (timer > 3) {
                timer = 0;
                GameManager.Instance.score--;
            }
            if (!wasonfire) {
                wasonfire = true;
                clone.SetActive(true);
                if (!playingAudio) {
                    audioController.Play();
                    playingAudio = true;
                }
            }
        } else {
            if (wasonfire) {
                Debug.Log("awd");
                wasonfire = false;
                clone.SetActive(false);
                audioController.Stop();
                playingAudio = false;
            }
        }
    }
}
